namespace Orc.Wizard.Example.Wizard;

using System;
using System.Collections.ObjectModel;
using System.Text;
using Catel.Services;

public class GadgetsWizardPage : WizardPageBase
{
    private readonly string _summaryTitle;

    public GadgetsWizardPage(ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(languageService);

        Title = languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_Description");
        _summaryTitle = languageService.GetRequiredString("Orc_Wizard_Example_GadgetsWizardPage_SummaryTitle");
        IsOptional = true;
        Gadgets = CreateGadgets();
    }

    public ObservableCollection<Gadget> Gadgets { get; private set; }

    private static ObservableCollection<Gadget> CreateGadgets()
    {
        return new ObservableCollection<Gadget>(new[]
        {
            new Gadget { Name = "Lumia 950" },
            new Gadget { Name = "Lumia 950 XL" },
            new Gadget { Name = "Surface Pro 3" },
            new Gadget { Name = "Surface Pro 4" },
            new Gadget { Name = "Surface Book" }
        });
    }

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
            Title = _summaryTitle,
            Summary = summary.ToString()
        };
    }
}
