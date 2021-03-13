using System.Linq;
using Microsoft.AspNetCore.Components;

namespace skillerator.Shared
{
    /// <summary>
    /// The injectable service class used to handle common functionalities all over the application.
    /// </summary>
    public class SkilleratorAppService
    {
        /// <summary>
        /// Specifies spinner component reference.
        /// </summary>
        public SpinnerComponent Spinner { get; set; }
    }
}