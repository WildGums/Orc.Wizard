namespace Orc.Wizard.Example.Wizard;

using Catel.Services;

public class Long1WizardPage : WizardPageBase
{
    public Long1WizardPage()
    {
        Title = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Title");
        Description = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Description");
        IsOptional = true;
    }

    public Long1WizardPage(ILanguageService languageService)
    {
        Title = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Description");
        IsOptional = true;
    }
}
