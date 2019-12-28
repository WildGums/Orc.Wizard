// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageViewModelBase.cs" company="WildGums">
//   Copyright (c) 2013 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using Catel;
    using Catel.MVVM;

    public class WizardPageViewModelBase<TWizardPage> : ViewModelBase
        where TWizardPage : class, IWizardPage
    {
        #region Constructors
        public WizardPageViewModelBase(TWizardPage wizardPage)
        {
            Argument.IsNotNull(() => wizardPage);

            DeferValidationUntilFirstSaveCall = true;

            WizardPage = wizardPage;
        }
        #endregion

        #region Properties

        [Model]
        public TWizardPage WizardPage { get; private set; }

        public IWizard Wizard
        {
            get
            {
                var wizardPage = WizardPage;
                if (wizardPage is null)
                {
                    return null;
                }

                return wizardPage.Wizard;
            }
        }
        #endregion
    }
}
