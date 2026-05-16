namespace Orc.Wizard.Example.Wizard;

using System;
using System.Collections.ObjectModel;
using System.Text;
using Catel.Services;

public class SkillsWizardPage : WizardPageBase
{
    private readonly string _summaryTitle;

    public SkillsWizardPage(ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(languageService);

        Title = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Title");
        Description = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_Description");
        _summaryTitle = languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPage_SummaryTitle");
        Skills = CreateSkills();
    }

    public ObservableCollection<Skill> Skills { get; private set; }

    private static ObservableCollection<Skill> CreateSkills()
    {
        return new ObservableCollection<Skill>(new[]
        {
            new Skill { Name = "C#" },
            new Skill { Name = "Catel" },
            new Skill { Name = "MVVM" },
            new Skill { Name = "WPF" },
        });
    }

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
