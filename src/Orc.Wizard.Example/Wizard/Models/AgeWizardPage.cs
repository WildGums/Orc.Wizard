namespace Orc.Wizard.Example.Wizard;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class AgeWizardPage : WizardPageBase
{
    private readonly ILogger<AgeWizardPage> _logger;

    public AgeWizardPage(ILogger<AgeWizardPage> logger)
    {
        Title = "Age";
        Description = "Specify the age of the person";
        IsOptional = true;
        _logger = logger;
    }

    public string Age { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = "Age",
            Summary = Age
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
