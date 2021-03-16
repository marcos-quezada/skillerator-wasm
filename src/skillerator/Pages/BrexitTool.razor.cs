using Microsoft.AspNetCore.Components;
using skillerator.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace skillerator.Pages{
    public partial class BrexitTool{
        [Parameter] public string Action{get; set;}

        [Inject] protected NavigationManager NavigationManager {get; set;}
        [Inject] protected HttpClient Http {get; set;}
        private const string SERVICE_ENDPOINT = "https://proxy.skillerator.de/api/bamf-abh";

        public Dictionary<long, AuslaenderbehoerdeData> AuslaenderbehoerdeDictionary = new Dictionary<long, AuslaenderbehoerdeData>();

        protected internal void GoToAction(string Action){
            NavigationManager.NavigateTo($"/brexit/{Action}");
        }

        protected override async Task OnInitializedAsync()
        {
            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(SERVICE_ENDPOINT)
            };

            requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            requestMessage.Headers.Add("Connection", "keep-alive");
            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/534.34 (KHTML, like Gecko) Qt/4.8.2");

            var response = await Http.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            
            AuslaenderbehoerdeData[] AuslaenderbehoerdeList = await response.Content.ReadFromJsonAsync<AuslaenderbehoerdeData[]>();
             
            foreach(var item in AuslaenderbehoerdeList){
                if(!AuslaenderbehoerdeDictionary.ContainsKey(item.Id))
                    AuslaenderbehoerdeDictionary.Add(item.Id, item);
            }
        }
    }
}