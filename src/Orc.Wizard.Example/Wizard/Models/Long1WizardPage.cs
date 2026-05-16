namespace Orc.Wizard.Example.Wizard;

using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public class Long1WizardPage : WizardPageBase
{
    public Long1WizardPage()
        : this(IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public Long1WizardPage(ILanguageService languageService)
    {
        Title = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_Long1WizardPage_Description");
        IsOptional = true;
    }
}
