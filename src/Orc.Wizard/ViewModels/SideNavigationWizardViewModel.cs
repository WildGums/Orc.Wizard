namespace Orc.Wizard.ViewModels;

using System;
using Catel.Services;
using Orc.Wizard;

public class SideNavigationWizardViewModel : WizardViewModel
{
    public SideNavigationWizardViewModel(IWizard wizard, IServiceProvider serviceProvider, 
        IMessageService messageService, ILanguageService languageService) 
        : base(wizard, serviceProvider, messageService, languageService)
    {
    }
}
