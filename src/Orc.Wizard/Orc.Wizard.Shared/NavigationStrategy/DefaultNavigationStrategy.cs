// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System.Collections.Generic;
    using System.Linq;
    using Catel;

    public class DefaultNavigationStrategy : INavigationStrategy
    {
        #region Methods
        public int GetIndexOfNextPage(IWizard wizard)
        {
            Argument.IsNotNull(() => wizard);

            IEnumerable<IWizardPage> pages = wizard.Pages;
            IWizardPage currentPage = wizard.CurrentPage;
            if (currentPage == null)
            {
                IWizardPage firstPage = pages.FirstOrDefault();
                if (firstPage == null)
                {
                    return WizardConfiguration.CannotNavigate;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                IWizardPage lastPage = pages.LastOrDefault();
                if (currentPage == lastPage)
                {
                    return WizardConfiguration.CannotNavigate;
                }
                else
                {
                    int indexOfCurrentPage = pages.ToList().IndexOf(currentPage);
                    int indexOfNextPage = indexOfCurrentPage + 1;
                    return indexOfNextPage;
                }
            }
        }

        public int GetIndexOfPreviousPage(IWizard wizard)
        {
            Argument.IsNotNull(() => wizard);

            IEnumerable<IWizardPage> pages = wizard.Pages;
            IWizardPage currentPage = wizard.CurrentPage;
            if (currentPage == null)
            {
                IWizardPage lastPage = pages.LastOrDefault();
                if (lastPage == null)
                {
                    return WizardConfiguration.CannotNavigate;
                }
                else
                {
                    int indexOfLastPage = pages.Count() - 1;
                    return indexOfLastPage;
                }
            }
            else
            {
                IWizardPage firstPage = pages.FirstOrDefault();
                if (currentPage == firstPage)
                {
                    return WizardConfiguration.CannotNavigate;
                }
                else
                {
                    int indexOfCurrentPage = pages.ToList().IndexOf(currentPage);
                    int indexOfPreviousPage = indexOfCurrentPage - 1;
                    return indexOfPreviousPage;
                }
            }
        }
        #endregion
    }
}