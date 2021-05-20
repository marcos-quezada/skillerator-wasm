using Microsoft.AspNetCore.Components;

namespace skillerator.Components{
    public partial class MultiStepsFormCard{
        [CascadingParameter] protected internal MultiStepsForm Parent { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Name { get; set; }

        [Parameter] public string ValidationGroup{get; set;}

        protected override void OnInitialized()
        {
            Parent.AddStep(this);
        }
    }
}