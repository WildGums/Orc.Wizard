namespace Orc.Wizard.Example.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.IoC;
    using Catel.MVVM;
    using Orc.Wizard.Example.Wizard;

    public class MainViewModel : ViewModelBase
    {
        private readonly IWizardService _wizardService;
        private readonly ITypeFactory _typeFactory;

        public MainViewModel(IWizardService wizardService, ITypeFactory typeFactory)
        {
            Argument.IsNotNull(() => wizardService);
            Argument.IsNotNull(() => typeFactory);

            _wizardService = wizardService;
            _typeFactory = typeFactory;

            ShowWizard = new TaskCommand<Type>(OnShowWizardExecuteAsync);
            UseFastForwardNavigationController = true;
            AllowQuickNavigation = true;
            ShowSummaryPage = true;
            ShowHelp = true;
            HandleNavigationStates = true;
            MarkAllPagesAsVisited = false;
            CacheViews = true;

            Title = "Orc.Wizard example";
        }

        #region Properties
        public bool ShowInTaskbar { get; set; }

        public bool ShowHelp { get; set; }

        public bool AllowQuickNavigation { get; set; }

        public bool UseFastForwardNavigationController { get; set; }

        public bool ShowSummaryPage { get; set; }

        public bool HandleNavigationStates { get; set; }

        public bool MarkAllPagesAsVisited { get; set; }

        public bool CacheViews { get; set; }
        #endregion

        #region Commands
        public TaskCommand<Type> ShowWizard { get; private set; }

        private Task OnShowWizardExecuteAsync(Type wizardType)
        {
            var wizard = _typeFactory.CreateInstance(wizardType) as IExampleWizard;

            wizard.ShowInTaskbarWrapper = ShowInTaskbar;
            wizard.ShowHelpWrapper = ShowHelp;
            wizard.AllowQuickNavigationWrapper = AllowQuickNavigation;
            wizard.HandleNavigationStatesWrapper = HandleNavigationStates;
            wizard.CacheViewsWrapper = CacheViews;

            if (UseFastForwardNavigationController)
            {
                wizard.NavigationControllerWrapper = _typeFactory.CreateInstanceWithParametersAndAutoCompletion<FastForwardNavigationController>(wizard);
            }

            if (!ShowSummaryPage)
            {
                var lastPage = wizard.Pages.Last();
                wizard.RemovePage(lastPage);
            }

            if (MarkAllPagesAsVisited)
            {
                wizard.Pages.ForEach(x => x.IsVisited = true);
            }

            return _wizardService.ShowWizardAsync(wizard);
        }
        #endregion
    }
}
