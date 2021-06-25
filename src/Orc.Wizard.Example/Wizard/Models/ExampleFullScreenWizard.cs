// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleWizard.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Services;

    public class ExampleFullScreenWizard : FullScreenWizardBase, IExampleWizard
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IMessageService _messageService;

        public ExampleFullScreenWizard(ITypeFactory typeFactory, IMessageService messageService)
            : base(typeFactory)
        {
            Argument.IsNotNull(() => messageService);

            _messageService = messageService;

            Title = "Orc.Wizard example";

            this.AddPage<PersonWizardPage>();
            this.AddPage<AgeWizardPage>();
            this.AddPage<Long1WizardPage>();
            this.AddPage<Long2WizardPage>();
            this.AddPage<SkillsWizardPage>();
            this.AddPage<ComponentsWizardPage>();
            this.AddPage<SummaryWizardPage>();

            // Test for numbers being updated correctly
            this.InsertPage<GadgetsWizardPage>(4);

            MinSize = new System.Windows.Size(800, 600);
            MaxSize = new System.Windows.Size(1000, 800);
            ResizeMode = System.Windows.ResizeMode.CanResize;
        }

        public INavigationController NavigationControllerWrapper
        {
            get { return NavigationController; }
            set { NavigationController = value; }
        }

        public bool ShowInTaskbarWrapper
        {
            get { return ShowInTaskbar; }
            set { ShowInTaskbar = value; }
        }

        public bool ShowHelpWrapper
        {
            get { return IsHelpVisible; }
            set { IsHelpVisible = value; }
        }

        public bool AllowQuickNavigationWrapper
        {
            get { return AllowQuickNavigation; }
            set { AllowQuickNavigation = value; }
        }

        public bool HandleNavigationStatesWrapper
        {
            get { return HandleNavigationStates; }
            set { HandleNavigationStates = value; }
        }

        public bool CacheViewsWrapper
        {
            get { return CacheViews; }
            set { CacheViews = value; }
        }

        public override Task ShowHelpAsync()
        {
            return _messageService.ShowAsync("HELP HANDLER");
        }

        public override async Task ResumeAsync()
        {
            Log.Info("Resuming wizard");

            await base.ResumeAsync();
        }
    }
}
