namespace Orc.Wizard.Example.Wizard;

using System.Collections.ObjectModel;
using System.Text;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public class ComponentsWizardPage : WizardPageBase
{
    private readonly ILanguageService _languageService;

    public ComponentsWizardPage()
        : this(IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public ComponentsWizardPage(ILanguageService languageService)
    {
        _languageService = languageService;
        Title = languageService.GetRequiredString("Orc_Wizard_Example_ComponentsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_ComponentsWizardPage_Description");
        IsOptional = true;

        Components = new ObservableCollection<Component>(new []
        {
            new Component { Name = "Orc.Analytics" },
            new Component { Name = "Orc.CommandLine" },
            new Component { Name = "Orc.Controls" },
            new Component { Name = "Orc.FilterBuilder" },
            new Component { Name = "Orc.FileAssociation" },
            new Component { Name = "Orc.LicenseManager" },
            new Component { Name = "Orc.LogViewer" },
            new Component { Name = "Orc.Notifications" },
            new Component { Name = "Orc.NuGetExplorer" },
            new Component { Name = "Orc.ProjectManagement" },
            new Component { Name = "Orc.Search" },
            new Component { Name = "Orc.SystemInfo" },
            new Component { Name = "Orc.WorkspaceManagement" },
            new Component { Name = "Orc.Wizard" },
            new Component { Name = "Orchestra" },
        });
    }

    public ObservableCollection<Component> Components { get; private set; }

    public override ISummaryItem GetSummary()
    {
        var summary = new StringBuilder();

        foreach (var component in Components)
        {
            if (component.IsSelected)
            {
                summary.AppendLine(component.Name);
            }
        }

        return new SummaryItem
        {
            Title = _languageService.GetRequiredString("Orc_Wizard_Example_ComponentsWizardPage_SummaryTitle"),
            Summary = summary.ToString()
        };
    }
}
