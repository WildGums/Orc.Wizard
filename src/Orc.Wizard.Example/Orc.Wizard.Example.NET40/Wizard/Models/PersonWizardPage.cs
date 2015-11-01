// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.Models
{
    public class PersonWizardPage : WizardPageBase
    {
        public PersonWizardPage()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override ISummaryItem GetSummary()
        {
            return new SummaryItem
            {
                Title = "Person",
                Summary = string.Format("{0} {1}", FirstName, LastName)
            };
        }
    }
}