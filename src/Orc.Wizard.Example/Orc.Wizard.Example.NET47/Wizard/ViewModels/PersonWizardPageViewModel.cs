﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstWizardPageViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using System.Collections.Generic;
    using Catel.Data;
    using Catel.MVVM;
    using Models;

    public class PersonWizardPageViewModel : WizardPageViewModelBase<PersonWizardPage>
    {
        public PersonWizardPageViewModel(PersonWizardPage wizardPage)
            : base(wizardPage)
        {
        }

        [ViewModelToModel]
        public string FirstName { get; set; }

        [ViewModelToModel]
        public string LastName { get; set; }

        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            base.ValidateFields(validationResults);

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                validationResults.Add(FieldValidationResult.CreateError("FirstName", "First name is required"));
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                validationResults.Add(FieldValidationResult.CreateError("LastName", "Last name is required"));
            }
        }
    }
}