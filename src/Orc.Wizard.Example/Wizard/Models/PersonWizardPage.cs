namespace Orc.Wizard.Example.Wizard;

using System.Threading.Tasks;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class PersonWizardPage : WizardPageBase
{
    private readonly ILogger<PersonWizardPage> _logger;
    private readonly ILanguageService _languageService;

    public PersonWizardPage(ILogger<PersonWizardPage> logger)
        : this(logger, IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public PersonWizardPage(ILogger<PersonWizardPage> logger, ILanguageService languageService)
    {
        _languageService = languageService;
        Title = languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Description");
        _logger = logger;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = _languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_SummaryTitle"),
            Summary = string.Format("{0} {1}", FirstName, LastName)
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
