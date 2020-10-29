// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GadgetsWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    using System.Collections.ObjectModel;
    using System.Text;

    public class GadgetsWizardPage : WizardPageBase
    {
        public GadgetsWizardPage()
        {
            Title = "Gadgets";
            Description = "Select the gadgets";
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
                Title = "Gadgets",
                Summary = summary.ToString()
            };
        }
    }
}
