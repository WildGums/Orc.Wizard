// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentsWizardPageViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.Collections;
    using Catel.Data;
    using Catel.MVVM;
    using Component = Example.Component;

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

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Components.ForEach(x => x.PropertyChanged += OnComponentPropertyChanged);
        }

        protected override async Task CloseAsync()
        {
            Components.ForEach(x => x.PropertyChanged -= OnComponentPropertyChanged);

            await base.CloseAsync();
        }

        private void OnComponentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Validate(true);
        }

        protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
        {
            base.ValidateBusinessRules(validationResults);

            var components = Components;
            if (components != null)
            {
                if (!components.Any(x => x.IsSelected))
                {
                    validationResults.Add(BusinessRuleValidationResult.CreateError("Select at least 1 component"));
                }
            }
        }
    }
}
