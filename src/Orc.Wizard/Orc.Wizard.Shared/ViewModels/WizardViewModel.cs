// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardViewModel.cs" company="WildGums">
//   Copyright (c) 2013 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Fody;
    using Catel.MVVM;
    using Catel.Services;
    using Catel.Windows;

    public class WizardViewModel : ViewModelBase
    {
        private readonly IMessageService _messageService;
        private readonly ILanguageService _languageService;

        #region Constructors
        public WizardViewModel(IWizard wizard, IMessageService messageService, ILanguageService languageService)
        {
            Argument.IsNotNull(() => wizard);
            Argument.IsNotNull(() => messageService);
            Argument.IsNotNull(() => languageService);

            DeferValidationUntilFirstSaveCall = true;

            Wizard = wizard;
            WizardPages = new List<IWizardPage>(wizard.Pages);
            _messageService = messageService;
            _languageService = languageService;

            Finish = new TaskCommand(OnFinishExecuteAsync, OnFinishCanExecute);
            Cancel = new TaskCommand(OnCancelExecuteAsync, OnCancelCanExecuteAsync);
            GoToNext = new TaskCommand(OnGoToNextExecuteAsync, OnGoToNextCanExecute);
            GoToPrevious = new TaskCommand(OnGoToPreviousExecuteAsync, OnGoToPreviousCanExecute);
        }
        #endregion

        #region Properties
        [Model(SupportIEditableObject = false)]
        [Expose("CurrentPage")]
        public IWizard Wizard { get; set; }

        public IEnumerable<IWizardPage> WizardPages { get; private set; } 

        public string PageTitle { get; private set; }

        public string PageDescription { get; private set; }

        public bool IsPageOptional { get; private set; }

        public bool IsFirstPage { get; private set; }

        public bool IsLastPage { get; private set; }
        #endregion

        #region Commands
        public TaskCommand GoToPrevious { get; set; }

        private async Task OnGoToPreviousExecuteAsync()
        {
            await Wizard.MoveBackAsync();

            UpdateState();
        }

        private bool OnGoToPreviousCanExecute()
        {
            return Wizard.CanMoveBack;
        }

        public TaskCommand GoToNext { get; set; }

        private async Task OnGoToNextExecuteAsync()
        {
            await Wizard.MoveForwardAsync();

            UpdateState();
        }

        private bool OnGoToNextCanExecute()
        {
            return Wizard.CanMoveForward;
        }

        public TaskCommand Finish { get; set; }

        private async Task OnFinishExecuteAsync()
        {
            if (await SaveAsync())
            {
                await Wizard.SaveAsync();
                await CloseViewModelAsync(true);
            }
        }

        private bool OnFinishCanExecute()
        {
            var validationSummary = this.GetValidationSummary(true);
            return !validationSummary.HasErrors && !validationSummary.HasWarnings && Wizard.CanResume;
        }

        public new TaskCommand Cancel { get; set; }

        private async Task OnCancelExecuteAsync()
        {
            if (await _messageService.ShowAsync(_languageService.GetString("Wizard_AreYouSureYouWantToCancelWizard"), button: MessageButton.YesNo) == MessageResult.No)
            {
                return;
            }

            if (await CancelAsync())
            {
                await Wizard.CancelAsync();
                await CloseViewModelAsync(false);
            }
        }

        private bool OnCancelCanExecuteAsync()
        {
            return Wizard.CanCancel;
        }
        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            UpdateState();
        }

        private void UpdateState()
        {
            IsFirstPage = Wizard.IsFirstPage();
            IsLastPage = Wizard.IsLastPage();

            var page = Wizard.CurrentPage;

            PageTitle = (page != null) ? page.Title : string.Empty;
            PageDescription = (page != null) ? page.Description : string.Empty;
            IsPageOptional = (page != null) ? page.IsOptional : false;

            var currentIndex = Wizard.Pages.TakeWhile(wizardPage => !ReferenceEquals(wizardPage, page)).Count() + 1;
            var totalPages = Wizard.Pages.Count();

            var title = Wizard.Title;
            if (!string.IsNullOrEmpty(title))
            {
                title += " - ";
            }

            title += string.Format(_languageService.GetString("Wizard_XofY"), currentIndex, totalPages);
            Title = title;
        }
        #endregion
    }
}