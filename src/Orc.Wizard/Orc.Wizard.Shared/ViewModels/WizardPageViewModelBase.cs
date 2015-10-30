// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageViewModelBase.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.ViewModels
{
    using Catel;
    using Catel.MVVM;

    public class WizardPageViewModelBase<TPage> : ViewModelBase
        where TPage : class, IWizardPage
    {
        #region Constructors
        public WizardPageViewModelBase(TPage wizardPage)
        {
            Argument.IsNotNull(() => wizardPage);

            WizardPage = wizardPage;
        }
        #endregion

        #region Properties

        [Model]
        public TPage WizardPage { get; private set; }
        #endregion
    }
}