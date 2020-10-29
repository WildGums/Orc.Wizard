// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GadgetsWizardPageViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
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

    public class GadgetsWizardPageViewModel : WizardPageViewModelBase<GadgetsWizardPage>
    {
        public GadgetsWizardPageViewModel(GadgetsWizardPage wizardPage)
            : base(wizardPage)
        {
            SelectAll = new Command(OnSelectAllExecute);
        }

        [ViewModelToModel]
        public ObservableCollection<Gadget> Gadgets { get; private set; }

        //protected override async Task InitializeAsync()
        //{
        //    await base.InitializeAsync();

        //    Gadgets.ForEach(x => x.PropertyChanged += OnGadgetPropertyChanged);
        //}

        //protected override async Task CloseAsync()
        //{
        //    Gadgets.ForEach(x => x.PropertyChanged -= OnGadgetPropertyChanged);

        //    await base.CloseAsync();
        //}

        //private void OnGadgetPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    Validate(true);
        //}


        //protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
        //{
        //    base.ValidateBusinessRules(validationResults);

        //    var gadgets = Gadgets;
        //    if (gadgets != null)
        //    {
        //        if (!gadgets.Any(x => x.IsSelected))
        //        {
        //            validationResults.Add(BusinessRuleValidationResult.CreateError("Select at least 1 component"));
        //        }
        //    }
        //}

        #region Commands
        public Command SelectAll { get; private set; }

        private void OnSelectAllExecute()
        {
            Gadgets.ForEach(x => x.IsSelected = true);
        }
        #endregion
    }
}
