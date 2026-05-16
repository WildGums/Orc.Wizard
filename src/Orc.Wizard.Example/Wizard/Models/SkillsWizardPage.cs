namespace Orc.Wizard.Example.Wizard;

using System.Collections.ObjectModel;
using System.Text;
using Catel.Services;

public class SkillsWizardPage : WizardPageBase
{
    private readonly string _summaryTitle;

    public SkillsWizardPage()
    {
        Title = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Title");
        Description = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Description");
        _summaryTitle = ExampleResourceHelper.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_SummaryTitle");
    }

    public SkillsWizardPage(ILanguageService languageService)
    {
        Title = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Description");
        _summaryTitle = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_SummaryTitle");

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
            Title = _summaryTitle,
            Summary = summary.ToString()
        };
    }
}
