using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace skillerator.Components{
    public partial class BrexitToolComponent{
        [CascadingParameter] protected internal MultiStepsForm ParentForm { get; set; }

        protected internal string[] BritishNationalitiesList = new string[] {"British citizen", "British National (Overseas)", "British Protected Person"};
    }
}

