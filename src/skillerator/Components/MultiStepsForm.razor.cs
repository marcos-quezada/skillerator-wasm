using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using skillerator.Models;
using Blazored.FluentValidation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Linq;
using System.Text;

namespace skillerator.Components{
    public partial class MultiStepsForm{
        protected internal List<MultiStepsFormCard> Cards = new List<MultiStepsFormCard>();

        [Parameter]
        public string Id{get;set;}

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public MultiStepsFormCard ActiveStep { get; set; }

        [Parameter]
        public int ActiveStepIx { get; set; }

        public bool IsLastStep { get; set; }

        public BrexitInfo userInfo {get; set;}= new BrexitInfo();
        [Inject] protected HttpClient Http {get; set;}
        [Inject] protected NavigationManager NavigationManager {get; set;}
        private FluentValidationValidator fluentValidationValidator;

        private AuslaenderbehoerdeData AuslaenderbehoerdeItem {get; set;}
        private const string GEO_DATA_SERVICE_ENDPOINT = "https://overpass-api.de/api/interpreter";
        private const string EMAIL_SERVICE_ENDPOINT = "https://proxy.skillerator.de/api/send-email";
        private const string BEHOERDEN_SERVICE_ENDPOINT = "https://bamf-navi.bamf.de/atlas-backend/behoerden/zustaendigkeiten";
        private const string DOWNLOAD_LINK_TEMPLATE = "http://api.skillerator.de/project/{0}/build/{1}/output/output.pdf";

        [CascadingParameter] protected internal Dictionary<long, AuslaenderbehoerdeData> AuslaenderbehoerdeDictionary{get; set;}
        bool PartialValidate(string RuleSetsName)
        {
            Console.WriteLine($"userInfo is elegible value : {userInfo.isElegible}");
            Console.WriteLine($"Rules Set Name : {RuleSetsName}");
            Console.WriteLine($"Test Comparison : {userInfo.isElegible.CompareTo(true)}");
            bool isValidData = fluentValidationValidator.Validate(options => options.IncludeRuleSets(RuleSetsName));
            Console.WriteLine($"Partial validation result : {isValidData}");
            return isValidData;
        }

        protected internal void GoBack()
        {
            if (ActiveStepIx > 0)
                SetActive(Cards[ActiveStepIx - 1]);
        }

        protected internal void GoNext()
        {
            //fluentValidationValidator.Validate(options => options.IncludeRuleSets(Cards[ActiveStepIx].ValidationGroup))
            if (ActiveStepIx < Cards.Count - 1 && PartialValidate(Cards[ActiveStepIx].ValidationGroup))
                SetActive(Cards[(Cards.IndexOf(ActiveStep) + 1)]);
        }

        async Task SubmitValidForm()
        {   
            await SendEmail();
            Console.WriteLine("Form Submitted Successfully!");
            NavigationManager.NavigateTo($"/brexit/sucess");
        }

        protected internal async Task SendEmail(){
            string PDFCreationResult = await GeneratePDF();
            
            JsonDocument PDFCreationResultJsonDocument = JsonDocument.Parse(PDFCreationResult);
            JsonElement Root = PDFCreationResultJsonDocument.RootElement;
            Root.TryGetProperty("compile", out JsonElement CompileElement);
            CompileElement.TryGetProperty("outputFiles", out JsonElement OuputFilesArray);
            OuputFilesArray.EnumerateArray().FirstOrDefault().TryGetProperty("build", out JsonElement BuildElement);
            
            string DownloadLink = string.Format(DOWNLOAD_LINK_TEMPLATE, userInfo.ProjectUUID, BuildElement.GetString());

            string MainEmailTemplate = await Http.GetStringAsync("templates/brexit_email_template.html");
            EmailContentData EmailData = new EmailContentData(userInfo.Email, "Document generated", string.Format(MainEmailTemplate, DownloadLink, DownloadLink));

            var json = JsonSerializer.Serialize(EmailData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");   

            var response = await Http.PostAsync(EMAIL_SERVICE_ENDPOINT, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        } 

        protected internal string CreateVarsLatexString(){
            return "\\newcommand{\\AuslanderbehoerdeDescription}{" + AuslaenderbehoerdeItem.Amtsbezeichnung + " " + AuslaenderbehoerdeItem.Bezeichnung 
                + "}\\newcommand{\\AuslanderbehoerdeAddressLineOne}{" + AuslaenderbehoerdeItem.StrasseHsNr + "}\\newcommand{\\AuslanderbehoerdeAddressLineTwo}{"
                + AuslaenderbehoerdeItem.Plz + " " + AuslaenderbehoerdeItem.Ort + "}\\newcommand{\\Surname}{" + userInfo.LastName + "}\\newcommand{\\FirstName}{"
                + userInfo.FirstName + "}\\newcommand{\\Gender}{" + userInfo.Gender + "}\\newcommand{\\CurrentAddressLineOne}{" + userInfo.StreetAddress + " "
                + userInfo.Number + "}\\newcommand{\\CurrentAddressLineTwo}{" + userInfo.ZipCode + " " + userInfo.City + "}\\setboolean{IsRegistration}{"
                + userInfo.isRegistration + "}\\setboolean{IsApplication}{" + userInfo.isCertificate + "}\\newcommand{\\FormerNames}{" + userInfo.FormerNames
                + "}\\newcommand{\\Email}{" + userInfo.Email + "}";
        }
        protected internal async Task<string> GeneratePDF(){
            await GetAuslanderbehoerdeId();
            string MainLatexTemplate = await Http.GetStringAsync("templates/brexit_template.tex");
            Resource[] resources = new Resource[]{
                new Resource("vars.tex", CreateVarsLatexString()),
                new Resource("main.tex", MainLatexTemplate)
            };
            Options options = new Options("pdflatex", 40);
            CompileElement compile = new CompileElement( options, resources);
            BrexitLatexTemplate template = new BrexitLatexTemplate(compile);

            var json = JsonSerializer.Serialize(template);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://api.skillerator.de/project/{userInfo.ProjectUUID}/compile";
            
            var response = await Http.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }

        protected internal async Task<string> GetAuslanderbehoerdeId(){
            UriBuilder builder = new UriBuilder(BEHOERDEN_SERVICE_ENDPOINT);
            string AmtlicherGemeindeSchluessel = await GetAmtlicherGemeindeSchluessel();
            Console.WriteLine($"Amtlicher Gemeinde Schlüssel : {AmtlicherGemeindeSchluessel}");
            builder.Query = $"ags={AmtlicherGemeindeSchluessel}&isVwe=0";

            var requestMessage = new HttpRequestMessage(){
                Method = new HttpMethod("GET"),
                RequestUri = builder.Uri
            };

            Console.WriteLine($"Auslanderbehoerde Message Request : {builder.Uri.ToString()}");

            var response = await Http.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;

            var result = await response.Content.ReadAsStringAsync();

            JsonDocument BehoerdeIdDocument = JsonDocument.Parse(result);
            Console.WriteLine($"JsonDocument : {BehoerdeIdDocument.ToString()}");
            JsonElement Root = BehoerdeIdDocument.RootElement;
            Console.WriteLine($"Root Element : {Root.ToString()}");
            Console.WriteLine($"First Element Root Array : {Root.EnumerateArray().FirstOrDefault().ToString()}");
            Console.WriteLine($"First Nested Array Element : {Root.EnumerateArray().FirstOrDefault().EnumerateArray().FirstOrDefault().ToString()}");
            string BehoerdeId = Root.EnumerateArray().FirstOrDefault().EnumerateArray().FirstOrDefault().ToString();
            Console.WriteLine($"BehoerdeId : {BehoerdeId}");
            AuslaenderbehoerdeItem = AuslaenderbehoerdeDictionary.GetValueOrDefault(Int64.Parse(BehoerdeId));            
            Console.WriteLine($"Auslanderbehörde address : {AuslaenderbehoerdeItem.StrasseHsNr}");

            return BehoerdeId;

        }
        protected internal async Task<string> GetAmtlicherGemeindeSchluessel(){
            UriBuilder builder = new UriBuilder(GEO_DATA_SERVICE_ENDPOINT);
            string DecodedQuery = $"[out:json];area[\"ISO3166-1\"=\"DE\"][admin_level=2];node[\"openGeoDB:community_identification_number\"][\"name\"=\"{userInfo.City}\"](area);out;";
            string EncodedQuery = System.Web.HttpUtility.UrlEncode(DecodedQuery);
            
            builder.Query = $"data={EncodedQuery}";
 
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("GET"),
                RequestUri = builder.Uri
            };

            var response = await Http.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            
            var result =  await response.Content.ReadAsStringAsync();

            JsonDocument OpenStreetMapDocument = JsonDocument.Parse(result);

            JsonElement Root = OpenStreetMapDocument.RootElement;
            JsonElement ElementsList = Root.GetProperty("elements");
            JsonElement MatchedElement = ElementsList.EnumerateArray().FirstOrDefault();
            MatchedElement.TryGetProperty("tags", out JsonElement TagsElement);
            TagsElement.TryGetProperty("openGeoDB:community_identification_number", out JsonElement AmtlicherGemeindeSchluesselElement);
            return AmtlicherGemeindeSchluesselElement.GetString();

        }



        protected internal void SetActive(MultiStepsFormCard card)
        {
            ActiveStep = card ?? throw new ArgumentNullException(nameof(card));

            ActiveStepIx = StepsIndex(card);
            if (ActiveStepIx == Cards.Count - 1)
                IsLastStep = true;
            else
                IsLastStep = false;
        }

        public int StepsIndex(MultiStepsFormCard card) => StepsIndexInternal(card);
        protected int StepsIndexInternal(MultiStepsFormCard card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            return Cards.IndexOf(card);
        }
        
        protected internal void AddStep(MultiStepsFormCard card)
        {
            Cards.Add(card);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SetActive(Cards[0]);
                StateHasChanged();
            }
        }

    }

    
}