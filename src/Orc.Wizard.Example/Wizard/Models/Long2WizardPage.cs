namespace Orc.Wizard.Example.Wizard;

using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public class Long2WizardPage : WizardPageBase
{
    public Long2WizardPage()
        : this(IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public Long2WizardPage(ILanguageService languageService)
    {
        Title = languageService.GetRequiredString("Orc_Wizard_Example_Long2WizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_Long2WizardPage_Description");
        IsOptional = true;
    }
}
