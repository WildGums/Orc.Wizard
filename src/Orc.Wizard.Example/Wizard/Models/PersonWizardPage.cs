namespace Orc.Wizard.Example.Wizard;

using System.Threading.Tasks;
using Catel.Services;
using Microsoft.Extensions.Logging;

public class PersonWizardPage : WizardPageBase
{
    private readonly ILogger<PersonWizardPage> _logger;
    private readonly string _summaryTitle;

    public PersonWizardPage(ILogger<PersonWizardPage> logger)
    {
        Title = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Title");
        Description = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Description");
        _summaryTitle = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_SummaryTitle");
        _logger = logger;
    }

    public PersonWizardPage(ILogger<PersonWizardPage> logger, ILanguageService languageService)
    {
        Title = languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_Description");
        _summaryTitle = languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPage_SummaryTitle");
        _logger = logger;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = _summaryTitle,
            Summary = string.Format("{0} {1}", FirstName, LastName)
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
