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
            ("ledig","single"),
            ("verheiratet/ eingetragene Partnerschaft", "married/ registered partnership"),
            ("getrenntlebend", "separated"),
            ("Scheidung eingereicht", "applied for divorce"),
            ("verwitwet", "widowed"),
            ("geschieden/ Partnerschaft aufgehoben", "divorced/ partnership annulled")
        };

        protected internal List<(string, string)> EyeColourList = new List<(string, string)>(){
            ("braun", "brown"),
            ("blau", "blue"),
            ("grau", "grey"),
            ("grün", "green"),
            ("blaugrün", "blue-green"),
            ("blaugrau", "blue-grey"),
            ("graugrün", "grey-green"),
            ("graübraun", "green-brown")
        };

        protected internal void AddSixMonthsPlusOutStay(){
            ParentForm.userInfo.SixMonthsPlusOutStayList.Add(new SixMonthsPlusOutStay());
        }

        protected internal void RemoveSixMonthsPlusOutStay(int index){
            ParentForm.userInfo.SixMonthsPlusOutStayList.RemoveAt(index);
        }
    }
}

