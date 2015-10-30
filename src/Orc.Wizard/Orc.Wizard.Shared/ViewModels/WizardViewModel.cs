// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardViewModel.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Windows;

    public class WizardViewModel : ViewModelBase
    {
        #region Constructors
        public WizardViewModel(IWizard wizard)
        {
            Argument.IsNotNull(() => wizard);

            Wizard = wizard;

            Buttons = new List<DataWindowButton>();

            Ok = new TaskCommand(OnOkExecuteAsync, OnOkCanExecute);
            Cancel = new TaskCommand(OnCancelExecuteAsync, OnCancelCanExecuteAsync);
            Next = new TaskCommand(OnNextExecuteAsync, OnNextCanExecute);
            Back = new TaskCommand(OnBackExecuteAsync, OnBackCanExecute);

            Buttons.Add(new DataWindowButton("Back", Back));
            Buttons.Add(new DataWindowButton("Next", Next));
            Buttons.Add(new DataWindowButton("OK", Ok));
            Buttons.Add(new DataWindowButton("Cancel", Cancel));
        }
        #endregion

        #region Properties
        public override string Title
        {
            get { return Wizard.Title; }
        }

        [Model]
        public IWizard Wizard { get; set; }

        // TODO: No buttons inside vm
        public IList<DataWindowButton> Buttons { get; private set; }
        
        // TODO: take the value from Wizard.CurrentPage.Header
        public string PageHeader { get; private set; }
        #endregion

        #region Commands
        public TaskCommand Back { get; set; }

        private Task OnBackExecuteAsync()
        {
            return Wizard.MoveBackAsync();
        }

        private bool OnBackCanExecute()
        {
            return Wizard.CanMoveBack;
        }

        public TaskCommand Next { get; set; }

        private Task OnNextExecuteAsync()
        {
            return Wizard.MoveForwardAsync();
        }

        private bool OnNextCanExecute()
        {
            return Wizard.CanMoveForward;
        }

        public TaskCommand Ok { get; set; }

        private async Task OnOkExecuteAsync()
        {
            if (await SaveAsync())
            {
                await Wizard.ResumeAsync();
                await CloseViewModelAsync(true);
            }
        }

        private bool OnOkCanExecute()
        {
            var validationSummary = this.GetValidationSummary(true);
            return !validationSummary.HasErrors && !validationSummary.HasWarnings && Wizard.CanResume;
        }

        public new TaskCommand Cancel { get; set; }

        private async Task OnCancelExecuteAsync()
        {
            if (await CancelAsync())
            {
                await Wizard.CancelAsync();
                await CloseViewModelAsync(true);
            }
        }

        private bool OnCancelCanExecuteAsync()
        {
            return Wizard.CanCancel;
        }
        #endregion
    }
}