using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace skillerator.Shared
{
    /// <summary>
    /// A base component to perform common functionalities.
    /// </summary>
    public class BaseComponent: ComponentBase
    {
        [Inject] protected SkilleratorAppService Service { get; set; }

        protected  override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            Service.Spinner?.Hide();
            Service.Spinner?.ShowModalSpinner(false);
        }
    }
}