// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkillsWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    using System.Collections.ObjectModel;
    using System.Text;

    public class SkillsWizardPage : WizardPageBase
    {
        public SkillsWizardPage()
        {
            Title = "Skills";
            Description = "Select the skills";

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
                Title = "Skills",
                Summary = summary.ToString()
            };
        }
    }
}
