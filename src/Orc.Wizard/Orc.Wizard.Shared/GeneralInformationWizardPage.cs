// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneralInformationWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Globalization;

    public class GeneralInformationWizardPage : WizardPageBase
    {
        public GeneralInformationWizardPage()
        {
            
        }

        public string Name { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public DayOfWeek FirstDayOfWeek { get; set; }
        public string ShortTimeFormat { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}