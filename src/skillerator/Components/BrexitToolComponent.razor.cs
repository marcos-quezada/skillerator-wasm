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
    }
}

