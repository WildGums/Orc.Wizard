// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkillsWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.Models
{
    using System.Collections.ObjectModel;
    using System.Text;
    using Example.Models;

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