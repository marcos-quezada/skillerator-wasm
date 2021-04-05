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
        
        protected internal List<(string, string)> MaritalStatusList = new(){
            ("single", "ledig"),
            ("married/ registered partnership", "verheiratet/ eingetragene Partnerschaft"),
            ("separated", "getrenntlebend"),
            ("applied for divorce", "Scheidung eingereicht"),
            ("widowed", "verwitwet"),
            ("divorced/ partnership annulled", "geschieden/ Partnerschaft aufgehoben")
        };

        protected internal List<(string, string)> EyeColourList = new(){
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
            ParentForm.UserInfo.SixMonthsPlusOutStayList.Add(new SixMonthsPlusOutStay());
        }

        protected internal void RemoveSixMonthsPlusOutStay(int index){
            ParentForm.UserInfo.SixMonthsPlusOutStayList.RemoveAt(index);
        }

        protected internal void AddAdditionalFamilyMemberData(){
            ParentForm.UserInfo.AdditionalFamilyMemberDataList.Add(new AdditionalFamilyMemberData());
        }

        protected internal void RemoveAdditionalFamilyMemberData(int index){
            ParentForm.UserInfo.AdditionalFamilyMemberDataList.RemoveAt(index);
        }
    }
}

