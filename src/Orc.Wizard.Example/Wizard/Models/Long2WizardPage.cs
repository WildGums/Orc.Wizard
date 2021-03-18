// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GadgetsWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    using System.Collections.ObjectModel;
    using System.Text;

    public class Long2WizardPage : WizardPageBase
    {
        public Long2WizardPage()
        {
            Title = "Long 2";
            Description = "Very long page 2";
            IsOptional = true;
        }
    }
}
