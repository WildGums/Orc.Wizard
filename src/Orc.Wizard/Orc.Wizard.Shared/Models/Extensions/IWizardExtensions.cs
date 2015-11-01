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
    using Catel.IoC;
    using Catel.Reflection;

    public static class IWizardExtensions
    {
        public static void AddPage(this IWizard wizard, IWizardPage page)
        {
            Argument.IsNotNull(() => wizard);
            Argument.IsNotNull(() => page);

            wizard.InsertPage(wizard.Pages.Count(), page);
        }

        public static void AddPage<TWizardPage>(this IWizard wizard)
            where TWizardPage : IWizardPage
        {
            Argument.IsNotNull(() => wizard);

            var typeFactory = wizard.GetTypeFactory();
            var page = typeFactory.CreateInstance<TWizardPage>();

            wizard.AddPage(page);
        }

        public static TWizardPage FindPageByType<TWizardPage>(this IWizard wizard)
            where TWizardPage : IWizardPage
        {
            return (TWizardPage)FindPage(wizard, x => typeof(TWizardPage).IsAssignableFromEx(x.GetType()));
        }

        public static IWizardPage FindPage(this IWizard wizard, Func<IWizardPage, bool> predicate)
        {
            Argument.IsNotNull(() => wizard);
            Argument.IsNotNull(() => predicate);

            var allPages = wizard.Pages.ToList();
            if (allPages.Count == 0)
            {
                return null;
            }

            return allPages.FirstOrDefault(predicate);
        }

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