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
            MoveBackViaCode = new TaskCommand(OnMoveBackViaCodeExecuteAsync);
            MoveForwardViaCode = new TaskCommand(OnMoveForwardViaCodeExecuteAsync);
            CancelViaCode = new TaskCommand(OnCancelViaCodeExecuteAsync);
            ResumeViaCode = new TaskCommand(OnResumeViaCodeExecuteAsync);
        }

        [ViewModelToModel]
        public ObservableCollection<Component> Components { get; private set; }

        #region Commands
        public Command SelectAll { get; private set; }

        private void OnSelectAllExecute()
        {
            Components.ForEach(x => x.IsSelected = true);
        }

        public TaskCommand MoveBackViaCode { get; private set; }

        private async Task OnMoveBackViaCodeExecuteAsync()
        {
            await Wizard.MoveBackAsync();
        }

        public TaskCommand MoveForwardViaCode { get; private set; }

        private async Task OnMoveForwardViaCodeExecuteAsync()
        {
            await Wizard.MoveForwardAsync();
        }

        public TaskCommand CancelViaCode { get; private set; }

        private async Task OnCancelViaCodeExecuteAsync()
        {
            await Wizard.CancelAsync();
        }

        public TaskCommand ResumeViaCode { get; private set; }

        private async Task OnResumeViaCodeExecuteAsync()
        {
            await Wizard.MoveForwardOrResumeAsync();
        }
        #endregion

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Components.ForEach(x => x.PropertyChanged += OnComponentPropertyChanged);
        }

        protected override Task<bool> SaveAsync()
        {
            return base.SaveAsync();
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
