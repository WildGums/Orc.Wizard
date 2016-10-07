// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
        public static IWizardPage AddPage(this IWizard wizard, IWizardPage page)
        {
            Argument.IsNotNull(() => wizard);
            Argument.IsNotNull(() => page);

            wizard.InsertPage(wizard.Pages.Count(), page);

            return page;
        }

        public static TWizardPage AddPage<TWizardPage>(this IWizard wizard)
            where TWizardPage : IWizardPage
        {
            Argument.IsNotNull(() => wizard);

            return wizard.InsertPage<TWizardPage>(wizard.Pages.Count());
        }

        public static TWizardPage InsertPage<TWizardPage>(this IWizard wizard, int index)
            where TWizardPage : IWizardPage
        {
            Argument.IsNotNull(() => wizard);

            var typeFactory = wizard.GetTypeFactory();
            var page = typeFactory.CreateInstance<TWizardPage>();

            wizard.InsertPage(index, page);

            return page;
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