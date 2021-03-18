// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GadgetsWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    using System.Collections.ObjectModel;
    using System.Text;

    public class Long1WizardPage : WizardPageBase
    {
        public Long1WizardPage()
        {
            Title = "Long 1";
            Description = "Very long page 1";
            IsOptional = true;
        }
    }
}
