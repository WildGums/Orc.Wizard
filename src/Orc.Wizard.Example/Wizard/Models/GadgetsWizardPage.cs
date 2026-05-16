namespace Orc.Wizard.Example.Wizard;

using System.Collections.ObjectModel;
using System.Text;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public class GadgetsWizardPage : WizardPageBase
{
    private readonly ILanguageService _languageService;

    public GadgetsWizardPage()
        : this(IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public GadgetsWizardPage(ILanguageService languageService)
    {
        _languageService = languageService;
        Title = languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_Description");
        IsOptional = true;

        Gadgets = new ObservableCollection<Gadget>(new[]
        {
            new Gadget { Name = "Lumia 950" },
            new Gadget { Name = "Lumia 950 XL" },
            new Gadget { Name = "Surface Pro 3" },
            new Gadget { Name = "Surface Pro 4" },
            new Gadget { Name = "Surface Book" }
        });
    }

    public ObservableCollection<Gadget> Gadgets { get; private set; }

    public override ISummaryItem GetSummary()
    {
        var summary = new StringBuilder();

        foreach (var gadget in Gadgets)
        {
            if (gadget.IsSelected)
            {
                summary.AppendLine(gadget.Name);
            }
        }

        return new SummaryItem
        {
            Title = _languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_SummaryTitle"),
            Summary = summary.ToString()
        };
    }
}
