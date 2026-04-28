namespace Orc.Wizard.ViewModels;

using System;
using Catel.Services;
using Orc.Wizard;

public class FullScreenWizardViewModel : WizardViewModel
{
    public FullScreenWizardViewModel(IWizard wizard, IServiceProvider serviceProvider, 
        IMessageService messageService, ILanguageService languageService) 
        : base(wizard, serviceProvider, messageService, languageService)
    {
    }
}
