namespace Orc.Wizard.Example.ViewModels
{
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;
    using Catel;
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

            ShowWizard = new TaskCommand(OnShowWizardExecuteAsync);
            ShowSummaryPage = true;
            HandleNavigationStates = true;

            Title = "Orc.Wizard example";
        }

        #region Properties
        public bool ShowInTaskbar { get; set; }

        public bool ShowSummaryPage { get; set; }

        public bool HandleNavigationStates { get; set; }
        #endregion

        #region Commands
        public TaskCommand ShowWizard { get; private set; }

        private Task OnShowWizardExecuteAsync()
        {
            var wizard = _typeFactory.CreateInstance<ExampleWizard>();
            wizard.ShowInTaskbarWrapper = ShowInTaskbar;
            wizard.HandleNavigationStatesWrapper = HandleNavigationStates;

            if (!ShowSummaryPage)
            {
                var lastPage = wizard.Pages.Last();
                wizard.RemovePage(lastPage);
            }

            return _wizardService.ShowWizardAsync(wizard);
        }
        #endregion
    }
}
