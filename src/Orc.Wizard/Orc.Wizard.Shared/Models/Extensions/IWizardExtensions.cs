// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;

    public static class IWizardExtensions
    {
        public static bool IsFirstPage(this IWizard wizard, IWizardPage wizardPage = null)
        {
            return IsPage(wizard, wizardPage, x => x.First());
        }

        public static bool IsLastPage(this IWizard wizard, IWizardPage wizardPage = null)
        {
            return IsPage(wizard, wizardPage, x => x.Last());
        }

        private static bool IsPage(this IWizard wizard, IWizardPage wizardPage, Func<List<IWizardPage>, IWizardPage> selector)
        {
            Argument.IsNotNull(() => wizard);

            if (wizardPage == null)
            {
                wizardPage = wizard.CurrentPage;
            }

            var allPages = wizard.Pages.ToList();
            if (allPages.Count == 0)
            {
                return false;
            }

            var isLastPage = ReferenceEquals(selector(allPages), wizardPage);
            return isLastPage;
        }
    }
}