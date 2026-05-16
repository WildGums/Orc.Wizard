namespace Orc.Wizard.Example.Wizard;

using System;
using System.Threading.Tasks;
using Catel.Services;
using Microsoft.Extensions.Logging;

public class AgeWizardPage : WizardPageBase
{
    private readonly ILogger<AgeWizardPage> _logger;
    private readonly string _summaryTitle;

    public AgeWizardPage(ILogger<AgeWizardPage> logger, ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(languageService);

        Title = languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_Description");
        _summaryTitle = languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_SummaryTitle");
        IsOptional = true;
        _logger = logger;
    }

    public string Age { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = _summaryTitle,
            Summary = Age
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
