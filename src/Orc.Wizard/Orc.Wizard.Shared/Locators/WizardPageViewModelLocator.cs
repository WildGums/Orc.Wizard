// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageViewModelLocator.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Collections.Generic;
    using Catel.MVVM;

    public class WizardPageViewModelLocator : ViewModelLocator, IWizardPageViewModelLocator
    {
        #region Fields
        private readonly IDictionary<Type, Type> _cache = new Dictionary<Type, Type>();
        #endregion

        public WizardPageViewModelLocator()
        {
            
        }

        #region Methods
        #endregion
    }
}