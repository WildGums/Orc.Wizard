// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageViewModelLocator.cs" company="WildGums">
//   Copyright (c) 2013 - 2015 WildGums. All rights reserved.
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