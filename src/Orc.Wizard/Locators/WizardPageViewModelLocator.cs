namespace Orc.Wizard;

using Catel.MVVM;

public class WizardPageViewModelLocator : ViewModelLocator, IWizardPageViewModelLocator
{
    public WizardPageViewModelLocator()
    {
        NamingConventions.Add("[CURRENT].ViewModels.[VW]PageViewModel");
        NamingConventions.Add("[CURRENT].ViewModels.[VW]ViewModel");
    }
}