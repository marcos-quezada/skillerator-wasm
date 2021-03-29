using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Linq;
using skillerator.Models;

namespace skillerator.Components{
    public partial class BrexitToolComponent{
        [CascadingParameter] protected internal MultiStepsForm ParentForm { get; set; }
        [CascadingParameter(Name = "CountriesList")] protected internal List<GeonameItem> CountriesList {get; set;}

        protected internal string[] BritishNationalitiesList = new string[] {"British Citizen", "British National (Overseas)", "British Protected Person", "British Subject (right of abode)",
            "British Subject (without right of abode)", "British Overseas Citizen", "British Overseas Territories Citizen (Gibraltar)", "British Overseas Territories Citizen (not Gibraltar)"};
        
        protected internal List<(string, string)> MaritalStatusList = new List<(string, string)>(){
            ("single", "ledig"),
            ("married/ registered partnership", "verheiratet/ eingetragene Partnerschaft"),
            ("separated", "getrenntlebend"),
            ("applied for divorce", "Scheidung eingereicht"),
            ("widowed", "verwitwet"),
            ("divorced/ partnership annulled", "geschieden/ Partnerschaft aufgehoben")
        };

        protected internal List<(string, string)> EyeColourList = new List<(string, string)>(){
            ("brown", "braun"),
            ("blue", "blau"),
            ("grey", "grau"),
            ("green", "grün"),
            ("blue-green", "blaugrün"),
            ("blue-grey", "blaugrau"),
            ("grey-green", "graugrün"),
            ("green-brown", "graübraun")
        };

        protected internal void AddSixMonthsPlusOutStay(){
            ParentForm.userInfo.SixMonthsPlusOutStayList.Add(new SixMonthsPlusOutStay());
        }

        protected internal void RemoveSixMonthsPlusOutStay(int index){
            ParentForm.userInfo.SixMonthsPlusOutStayList.RemoveAt(index);
        }
    }
}

