// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewProjectWizardWindow.xaml.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Views
{
    using System.Linq;
    using Catel.Windows;
    using ViewModels;

    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow
    {
        #region Constructors
        public WizardWindow()
            : this(null)
        {
        }

        public WizardWindow(WizardViewModel viewModel)
            : base(viewModel, DataWindowMode.Custom)
        {
            InitializeButtons(viewModel);

            InitializeComponent();
        }
        #endregion

        protected override void OnViewModelChanged()
        {
            base.OnViewModelChanged();

            var vm = ViewModel as WizardViewModel;
            if (vm == null)
            {
                return;
            }

            InitializeButtons(vm);
        }

        private void InitializeButtons(WizardViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            // TODO: clear previous buttons

            if (!viewModel.Buttons.Any())
            {
                AddCustomButton(new DataWindowButton("OK", OnOkExecute, OnOkCanExecute));
            }
            else
            {
                foreach (var button in viewModel.Buttons)
                {
                    AddCustomButton(button);
                }
            }
        }
    }
}