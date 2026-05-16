namespace Orc.Wizard.Example.Wizard;

using System.Threading.Tasks;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class AgeWizardPage : WizardPageBase
{
    private readonly ILogger<AgeWizardPage> _logger;
    private readonly ILanguageService _languageService;

    public AgeWizardPage(ILogger<AgeWizardPage> logger)
        : this(logger, IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public AgeWizardPage(ILogger<AgeWizardPage> logger, ILanguageService languageService)
    {
        _languageService = languageService;
        Title = languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_Description");
        IsOptional = true;
        _logger = logger;
    }

    public string Age { get; set; }

    public override ISummaryItem GetSummary()
    {
        return new SummaryItem
        {
            Title = _languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPage_SummaryTitle"),
            Summary = Age
        };
    }

    public override Task CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page");

        return base.CancelAsync();
    }
}
