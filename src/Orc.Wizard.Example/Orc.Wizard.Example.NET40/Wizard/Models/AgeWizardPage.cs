// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgeWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.Models
{
    public class AgeWizardPage : WizardPageBase
    {
        public AgeWizardPage()
        {
            Title = "Age";
            Description = "Specify the age of the person";
            IsOptional = true;
        }

        public string Age { get; set; }
    }
}