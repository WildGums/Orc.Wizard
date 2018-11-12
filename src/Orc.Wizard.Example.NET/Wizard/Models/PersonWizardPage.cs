// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    public class PersonWizardPage : WizardPageBase
    {
        public PersonWizardPage()
        {
            Title = "Person";
            Description = "Enter the details of the person";
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
