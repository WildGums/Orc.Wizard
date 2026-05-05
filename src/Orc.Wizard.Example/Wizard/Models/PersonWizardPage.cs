namespace Orc.Wizard.Example.Wizard;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class PersonWizardPage : WizardPageBase
{
    private readonly ILogger<PersonWizardPage> _logger;

    public PersonWizardPage(ILogger<PersonWizardPage> logger)
    {
        Title = "Person";
        Description = "Enter the details of the person";
        _logger = logger;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = "Person",
            Summary = string.Format("{0} {1}", FirstName, LastName)
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
