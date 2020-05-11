// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkillsWizardPageViewModel.cs" company="WildGums">
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

    public class SkillsWizardPageViewModel : WizardPageViewModelBase<SkillsWizardPage>
    {
        public SkillsWizardPageViewModel(SkillsWizardPage wizardPage)
            : base(wizardPage)
        {
            SelectAll = new Command(OnSelectAllExecute);
        }

        [ViewModelToModel]
        public ObservableCollection<Skill> Skills { get; private set; }

        #region Commands
        public Command SelectAll { get; private set; }

        private void OnSelectAllExecute()
        {
            Skills.ForEach(x => x.IsSelected = true);
        }
        #endregion

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Skills.ForEach(x => x.PropertyChanged += OnSkillPropertyChanged);
        }

        protected override async Task CloseAsync()
        {
            Skills.ForEach(x => x.PropertyChanged -= OnSkillPropertyChanged);

            await base.CloseAsync();
        }

        private void OnSkillPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Validate(true);
        }

        protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
        {
            base.ValidateBusinessRules(validationResults);

            var skills = Skills;
            if (skills != null)
            {
                if (!skills.Any(x => x.IsSelected))
                {
                    validationResults.Add(BusinessRuleValidationResult.CreateError("Select at least 1 skill"));
                }
            }
        }
    }
}
