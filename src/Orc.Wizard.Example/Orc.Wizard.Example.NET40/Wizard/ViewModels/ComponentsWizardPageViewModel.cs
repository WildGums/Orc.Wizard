// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentsWizardPageViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Catel.Collections;
    using Catel.Data;
    using Catel.MVVM;
    using Example.Models;
    using Models;

    public class ComponentsWizardPageViewModel : WizardPageViewModelBase<ComponentsWizardPage>
    {
        public ComponentsWizardPageViewModel(ComponentsWizardPage wizardPage)
            : base(wizardPage)
        {
            SelectAll = new Command(OnSelectAllExecute);
        }

        [ViewModelToModel]
        public ObservableCollection<Component> Components { get; private set; }

        #region Commands
        public Command SelectAll { get; private set; }

        private void OnSelectAllExecute()
        {
            Components.ForEach(x => x.IsSelected = true);
        }
        #endregion

        protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
        {
            base.ValidateBusinessRules(validationResults);

            var components = Components;
            if (components != null)
            {
                if (!Components.Any(x => x.IsSelected))
                {
                    validationResults.Add(BusinessRuleValidationResult.CreateError("Select at least 1 component"));
                }
            }
        }
    }
}