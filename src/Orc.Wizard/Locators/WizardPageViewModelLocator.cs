namespace Orc.Wizard;

using Catel.MVVM;
using Microsoft.Extensions.Logging;

public class WizardPageViewModelLocator : ViewModelLocator, IWizardPageViewModelLocator
{
    public WizardPageViewModelLocator(ILogger<ViewModelLocator> logger)
        : base(logger)
    {
        NamingConventions.Add("[CURRENT].ViewModels.[VW]PageViewModel");
        NamingConventions.Add("[CURRENT].ViewModels.[VW]ViewModel");
    }
}
