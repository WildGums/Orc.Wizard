// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageViewModelLocator.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Collections.Generic;

    public class WizardPageViewModelLocator : IWizardPageViewModelLocator
    {
        #region Fields
        private readonly IDictionary<Type, Type> _cache = new Dictionary<Type, Type>();
        #endregion

        #region Methods
        // TODO: support naming conventions like Catel does (re-use the components)
        public void RegisterWizardPageViewModel(Type wizardPageType, Type viewModelType)
        {
            _cache[wizardPageType] = viewModelType;
        }

        public Type ResolveWizardPageViewModel(Type wizardPageType)
        {
            return _cache[wizardPageType];
        }
        #endregion
    }
}