namespace Orc.Wizard.Example.Wizard;

using System.Collections.ObjectModel;
using System.Text;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public class SkillsWizardPage : WizardPageBase
{
    private readonly ILanguageService _languageService;

    public SkillsWizardPage()
        : this(IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public SkillsWizardPage(ILanguageService languageService)
    {
        _languageService = languageService;
        Title = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Description");

        Skills = new ObservableCollection<Skill>(new[]
        {
            new Skill { Name = "C#" },
            new Skill { Name = "Catel" },
            new Skill { Name = "MVVM" },
            new Skill { Name = "WPF" },
        });
    }

    public ObservableCollection<Skill> Skills { get; private set; }

    public override ISummaryItem GetSummary()
    {
        var summary = new StringBuilder();

        foreach (var skill in Skills)
        {
            if (skill.IsSelected)
            {
                summary.AppendLine(skill.Name);
            }
        }

        return new SummaryItem
        {
            Title = _languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_SummaryTitle"),
            Summary = summary.ToString()
        };
    }
}
