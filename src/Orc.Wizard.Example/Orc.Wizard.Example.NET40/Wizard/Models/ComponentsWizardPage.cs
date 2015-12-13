// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentsWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.Models
{
    using System.Collections.ObjectModel;
    using System.Text;
    using Example.Models;

    public class ComponentsWizardPage : WizardPageBase
    {
        public ComponentsWizardPage()
        {
            Title = "Components";
            Description = "Select the components";

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
                Title = "Components",
                Summary = summary.ToString()
            };
        }
    }
}