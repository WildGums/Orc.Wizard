namespace Orc.Wizard.Example.Wizard;

using System;
using Catel.Services;

public class Long2WizardPage : WizardPageBase
{
    public Long2WizardPage(ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(languageService);

        Title = languageService.GetRequiredString("Orc_Wizard_Example_Long2WizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_Long2WizardPage_Description");
        IsOptional = true;
    }
}
