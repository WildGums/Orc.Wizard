namespace Orc.Wizard.Example.Wizard;

using System;
using Catel.Services;

public class Long1WizardPage : WizardPageBase
{
    public Long1WizardPage(ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(languageService);

        Title = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Description");
        IsOptional = true;
    }
}
