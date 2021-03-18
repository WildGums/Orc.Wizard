[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v5.0", FrameworkDisplayName="")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/wizard", "Orc.Wizard")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/wizard", "Orc.Wizard.Controls")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/wizard", "Orc.Wizard.Converters")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/wizard", "Orc.Wizard.Views")]
[assembly: System.Windows.Markup.XmlnsPrefix("http://schemas.wildgums.com/orc/wizard", "orcwizard")]
[assembly: System.Windows.ThemeInfo(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
namespace Orc.Wizard.Controls
{
    public sealed class BreadcrumbItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public static readonly System.Windows.DependencyProperty CurrentPageProperty;
        public static readonly System.Windows.DependencyProperty DescriptionProperty;
        public static readonly System.Windows.DependencyProperty NumberProperty;
        public static readonly System.Windows.DependencyProperty PageProperty;
        public static readonly System.Windows.DependencyProperty TitleProperty;
        public BreadcrumbItem() { }
        public Orc.Wizard.IWizardPage CurrentPage { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public Orc.Wizard.IWizardPage Page { get; set; }
        public string Title { get; set; }
        public void InitializeComponent() { }
    }
    public class SideNavigationBreadcrumbItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public static readonly System.Windows.DependencyProperty CurrentPageProperty;
        public static readonly System.Windows.DependencyProperty DescriptionProperty;
        public static readonly System.Windows.DependencyProperty NumberProperty;
        public static readonly System.Windows.DependencyProperty PageProperty;
        public static readonly System.Windows.DependencyProperty TitleProperty;
        public SideNavigationBreadcrumbItem() { }
        public Orc.Wizard.IWizardPage CurrentPage { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public Orc.Wizard.IWizardPage Page { get; set; }
        public string Title { get; set; }
        public void InitializeComponent() { }
    }
    public class WizardPageHeader : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public static readonly System.Windows.DependencyProperty DescriptionProperty;
        public static readonly System.Windows.DependencyProperty TextAlignmentProperty;
        public static readonly System.Windows.DependencyProperty TitleProperty;
        public WizardPageHeader() { }
        public string Description { get; set; }
        public System.Windows.TextAlignment TextAlignment { get; set; }
        public string Title { get; set; }
        public void InitializeComponent() { }
    }
}
namespace Orc.Wizard.Converters
{
    public class BreadcrumbTitleConverter : Catel.MVVM.Converters.ValueConverterBase<Orc.Wizard.IWizardPage>
    {
        public BreadcrumbTitleConverter() { }
        protected override object Convert(Orc.Wizard.IWizardPage value, System.Type targetType, object parameter) { }
    }
    public class IsSelectedToBrushConverter : Catel.MVVM.Converters.ValueConverterBase<bool>
    {
        public IsSelectedToBrushConverter() { }
        protected override object Convert(bool value, System.Type targetType, object parameter) { }
    }
    public class IsSelectedToForegroundBrushConverter : Catel.MVVM.Converters.ValueConverterBase<bool>
    {
        public IsSelectedToForegroundBrushConverter() { }
        protected override object Convert(bool value, System.Type targetType, object parameter) { }
    }
    public class WizardPageToIsSelectedConverter : Catel.MVVM.Converters.ValueConverterBase<Orc.Wizard.IWizardPage>
    {
        public WizardPageToIsSelectedConverter() { }
        protected override object Convert(Orc.Wizard.IWizardPage value, System.Type targetType, object parameter) { }
    }
}
namespace Orc.Wizard
{
    public class DefaultNavigationController : Orc.Wizard.INavigationController
    {
        protected readonly Catel.Services.ILanguageService _languageService;
        protected readonly Catel.Services.IMessageService _messageService;
        public DefaultNavigationController(Orc.Wizard.IWizard wizard, Catel.Services.ILanguageService languageService, Catel.Services.IMessageService messageService) { }
        public Orc.Wizard.IWizard Wizard { get; }
        protected virtual Orc.Wizard.WizardNavigationButton CreateBackButton(Orc.Wizard.IWizard wizard) { }
        protected virtual Orc.Wizard.WizardNavigationButton CreateCancelButton(Orc.Wizard.IWizard wizard) { }
        protected virtual Orc.Wizard.WizardNavigationButton CreateFinishButton(Orc.Wizard.IWizard wizard) { }
        protected virtual Orc.Wizard.WizardNavigationButton CreateForwardButton(Orc.Wizard.IWizard wizard) { }
        protected virtual System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardNavigationButton> CreateNavigationButtons(Orc.Wizard.IWizard wizard) { }
        public void EvaluateNavigationCommands() { }
        public System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardNavigationButton> GetNavigationButtons() { }
    }
    public class DefaultNavigationStrategy : Orc.Wizard.INavigationStrategy
    {
        public DefaultNavigationStrategy() { }
        public int GetIndexOfNextPage(Orc.Wizard.IWizard wizard) { }
        public int GetIndexOfPreviousPage(Orc.Wizard.IWizard wizard) { }
    }
    public class FastForwardNavigationController : Orc.Wizard.DefaultNavigationController
    {
        public FastForwardNavigationController(Orc.Wizard.IWizard wizard, Catel.Services.ILanguageService languageService, Catel.Services.IMessageService messageService) { }
        protected override Orc.Wizard.WizardNavigationButton CreateFinishButton(Orc.Wizard.IWizard wizard) { }
        protected override System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardNavigationButton> CreateNavigationButtons(Orc.Wizard.IWizard wizard) { }
    }
    public abstract class FullScreenWizardBase : Orc.Wizard.WizardBase
    {
        public static readonly Catel.Data.PropertyData HideNavigationSystemProperty;
        protected FullScreenWizardBase(Catel.IoC.ITypeFactory typeFactory) { }
        public bool HideNavigationSystem { get; set; }
    }
    public class GeneralInformationWizardPage : Orc.Wizard.WizardPageBase
    {
        public static readonly Catel.Data.PropertyData CultureInfoProperty;
        public static readonly Catel.Data.PropertyData EndDateProperty;
        public static readonly Catel.Data.PropertyData FirstDayOfWeekProperty;
        public static readonly Catel.Data.PropertyData NameProperty;
        public static readonly Catel.Data.PropertyData ShortTimeFormatProperty;
        public static readonly Catel.Data.PropertyData StartDateProperty;
        public GeneralInformationWizardPage() { }
        public System.Globalization.CultureInfo CultureInfo { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DayOfWeek FirstDayOfWeek { get; set; }
        public string Name { get; set; }
        public string ShortTimeFormat { get; set; }
        public System.DateTime StartDate { get; set; }
    }
    public interface INavigationController
    {
        void EvaluateNavigationCommands();
        System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardNavigationButton> GetNavigationButtons();
    }
    public interface INavigationStrategy
    {
        int GetIndexOfNextPage(Orc.Wizard.IWizard wizard);
        int GetIndexOfPreviousPage(Orc.Wizard.IWizard wizard);
    }
    public interface ISummaryItem
    {
        Orc.Wizard.IWizardPage Page { get; set; }
        string Summary { get; set; }
        string Title { get; set; }
    }
    public interface IWizard
    {
        bool AllowQuickNavigation { get; }
        bool CanCancel { get; }
        bool CanMoveBack { get; }
        bool CanMoveForward { get; }
        bool CanResume { get; }
        bool CanShowHelp { get; }
        Orc.Wizard.IWizardPage CurrentPage { get; }
        bool HandleNavigationStates { get; }
        bool IsHelpVisible { get; }
        System.Windows.Size MaxSize { get; }
        System.Windows.Size MinSize { get; }
        Orc.Wizard.INavigationController NavigationController { get; }
        Orc.Wizard.INavigationStrategy NavigationStrategy { get; }
        System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardPage> Pages { get; }
        System.Windows.ResizeMode ResizeMode { get; }
        bool RestoreScrollPositionPerPage { get; }
        bool ShowInTaskbar { get; }
        string Title { get; }
        event System.EventHandler<System.EventArgs> Canceled;
        event System.EventHandler<System.EventArgs> CurrentPageChanged;
        event System.EventHandler<System.EventArgs> HelpShown;
        event System.EventHandler<System.EventArgs> MovedBack;
        event System.EventHandler<System.EventArgs> MovedForward;
        event System.EventHandler<System.EventArgs> Resumed;
        System.Threading.Tasks.Task CancelAsync();
        System.Threading.Tasks.Task CloseAsync();
        Catel.Data.IValidationContext GetValidationContextForCurrentPage(bool validate = true);
        System.Threading.Tasks.Task InitializeAsync();
        void InsertPage(int index, Orc.Wizard.IWizardPage page);
        System.Threading.Tasks.Task MoveBackAsync();
        System.Threading.Tasks.Task MoveForwardAsync();
        System.Threading.Tasks.Task MoveToPageAsync(int indexOfNextPage);
        void RemovePage(Orc.Wizard.IWizardPage page);
        System.Threading.Tasks.Task ResumeAsync();
        [ObsoleteEx(RemoveInVersion="4.0", ReplacementTypeOrMember="ResumeAsync", TreatAsErrorFromVersion="3.0")]
        System.Threading.Tasks.Task SaveAsync();
        System.Threading.Tasks.Task ShowHelpAsync();
    }
    public static class IWizardExtensions
    {
        public static Orc.Wizard.IWizardPage AddPage(this Orc.Wizard.IWizard wizard, Orc.Wizard.IWizardPage page) { }
        public static TWizardPage AddPage<TWizardPage>(this Orc.Wizard.IWizard wizard)
            where TWizardPage : Orc.Wizard.IWizardPage { }
        public static TWizardPage AddPage<TWizardPage>(this Orc.Wizard.IWizard wizard, object model)
            where TWizardPage : Orc.Wizard.IWizardPage { }
        public static Orc.Wizard.IWizardPage FindPage(this Orc.Wizard.IWizard wizard, System.Func<Orc.Wizard.IWizardPage, bool> predicate) { }
        public static TWizardPage FindPageByType<TWizardPage>(this Orc.Wizard.IWizard wizard)
            where TWizardPage : Orc.Wizard.IWizardPage { }
        public static TWizardPage InsertPage<TWizardPage>(this Orc.Wizard.IWizard wizard, int index)
            where TWizardPage : Orc.Wizard.IWizardPage { }
        public static TWizardPage InsertPage<TWizardPage>(this Orc.Wizard.IWizard wizard, int index, object model)
            where TWizardPage : Orc.Wizard.IWizardPage { }
        public static bool IsFirstPage(this Orc.Wizard.IWizard wizard, Orc.Wizard.IWizardPage wizardPage = null) { }
        public static bool IsLastPage(this Orc.Wizard.IWizard wizard, Orc.Wizard.IWizardPage wizardPage = null) { }
        public static System.Threading.Tasks.Task MoveForwardOrResumeAsync(this Orc.Wizard.IWizard wizard) { }
        public static System.Threading.Tasks.Task MoveToPageAsync(this Orc.Wizard.IWizard wizard, Orc.Wizard.IWizardPage wizardPage) { }
    }
    public interface IWizardNavigationButton
    {
        System.Windows.Input.ICommand Command { get; }
        string Content { get; }
        bool IsVisible { get; }
        void Update();
    }
    public interface IWizardPage
    {
        string BreadcrumbTitle { get; set; }
        string Description { get; set; }
        bool IsOptional { get; }
        bool IsVisited { get; set; }
        int Number { get; set; }
        string Title { get; set; }
        Catel.MVVM.IViewModel ViewModel { get; set; }
        Orc.Wizard.IWizard Wizard { get; set; }
        event System.EventHandler<Orc.Wizard.ViewModelChangedEventArgs> ViewModelChanged;
        System.Threading.Tasks.Task AfterWizardPagesSavedAsync();
        System.Threading.Tasks.Task CancelAsync();
        Orc.Wizard.ISummaryItem GetSummary();
        System.Threading.Tasks.Task SaveAsync();
    }
    public static class IWizardPageExtensions
    {
        public static System.Threading.Tasks.Task MoveForwardOrResumeAsync(this Orc.Wizard.IWizardPage wizardPage) { }
    }
    public interface IWizardPageViewModel : Catel.Data.IValidatable, Catel.MVVM.IViewModel, System.ComponentModel.IDataErrorInfo, System.ComponentModel.IDataWarningInfo, System.ComponentModel.INotifyDataErrorInfo, System.ComponentModel.INotifyDataWarningInfo, System.ComponentModel.INotifyPropertyChanged
    {
        void EnableValidationExposure();
    }
    public interface IWizardPageViewModelLocator : Catel.MVVM.ILocator, Catel.MVVM.IViewModelLocator { }
    public interface IWizardService
    {
        System.Threading.Tasks.Task<bool?> ShowWizardAsync(Orc.Wizard.IWizard wizard);
    }
    public static class IWizardServiceExtensions
    {
        public static System.Threading.Tasks.Task<bool?> ShowWizardAsync<TWizard>(this Orc.Wizard.IWizardService wizardService, object model = null)
            where TWizard : Orc.Wizard.IWizard { }
    }
    public class LibraryThemeProvider : ControlzEx.Theming.LibraryThemeProvider
    {
        public LibraryThemeProvider() { }
        public override void FillColorSchemeValues(System.Collections.Generic.Dictionary<string, string> values, ControlzEx.Theming.RuntimeThemeColorValues colorValues) { }
    }
    public static class ListBoxExtensions
    {
        public static readonly System.Windows.DependencyProperty HorizontalOffsetProperty;
        public static void CenterSelectedItem(this System.Windows.Controls.ListBox listBox) { }
        public static double GetHorizontalOffset(System.Windows.FrameworkElement target) { }
        public static void SetHorizontalOffset(System.Windows.FrameworkElement target, double value) { }
    }
    public static class ModuleInitializer
    {
        public static void Initialize() { }
    }
    public static class ProgressBarExtensions
    {
        public static readonly System.Windows.DependencyProperty SmoothProgressProperty;
        public static double GetSmoothProgress(System.Windows.FrameworkElement target) { }
        public static void SetSmoothProgress(System.Windows.FrameworkElement target, double value) { }
        public static void UpdateProgress(this System.Windows.Controls.ProgressBar progressBar, int currentItem, int totalItems) { }
    }
    public abstract class SideNavigationWizardBase : Orc.Wizard.WizardBase
    {
        public static readonly Catel.Data.PropertyData ShowFullScreenProperty;
        protected SideNavigationWizardBase(Catel.IoC.ITypeFactory typeFactory) { }
        public bool ShowFullScreen { get; set; }
    }
    public class SummaryItem : Orc.Wizard.ISummaryItem
    {
        public SummaryItem() { }
        public Orc.Wizard.IWizardPage Page { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
    }
    public class SummaryWizardPage : Orc.Wizard.WizardPageBase
    {
        public SummaryWizardPage(Catel.Services.ILanguageService languageService) { }
    }
    public static class ThemingKeys
    {
        public const string AccentColor = "Orc.Colors.AccentColor";
        public const string AccentColor40 = "Orc.Colors.AccentColor40";
        public const string AccentColorBrush = "Orc.Brushes.AccentColorBrush";
        public const string AccentColorBrush40 = "Orc.Brushes.AccentColorBrush40";
    }
    public class ViewModelChangedEventArgs : System.EventArgs
    {
        public ViewModelChangedEventArgs(Catel.MVVM.IViewModel oldViewModel, Catel.MVVM.IViewModel newViewModel) { }
        public Catel.MVVM.IViewModel NewViewModel { get; }
        public Catel.MVVM.IViewModel OldViewModel { get; }
    }
    public abstract class WizardBase : Catel.Data.ModelBase, Orc.Wizard.IWizard
    {
        protected readonly Catel.IoC.ITypeFactory _typeFactory;
        public static readonly Catel.Data.PropertyData AllowQuickNavigationProperty;
        public static readonly Catel.Data.PropertyData CanShowHelpProperty;
        public static readonly Catel.Data.PropertyData HandleNavigationStatesProperty;
        public static readonly Catel.Data.PropertyData HorizontalScrollbarVisibilityProperty;
        public static readonly Catel.Data.PropertyData IsHelpVisibleProperty;
        public static readonly Catel.Data.PropertyData MaxSizeProperty;
        public static readonly Catel.Data.PropertyData MinSizeProperty;
        public static readonly Catel.Data.PropertyData ResizeModeProperty;
        public static readonly Catel.Data.PropertyData RestoreScrollPositionPerPageProperty;
        public static readonly Catel.Data.PropertyData ShowInTaskbarProperty;
        public static readonly Catel.Data.PropertyData TitleProperty;
        public static readonly Catel.Data.PropertyData VerticalScrollbarVisibilityProperty;
        protected WizardBase(Catel.IoC.ITypeFactory typeFactory) { }
        public bool AllowQuickNavigation { get; set; }
        public virtual bool CanCancel { get; }
        public virtual bool CanMoveBack { get; }
        public virtual bool CanMoveForward { get; }
        public virtual bool CanResume { get; }
        public bool CanShowHelp { get; set; }
        public Orc.Wizard.IWizardPage CurrentPage { get; }
        public virtual bool HandleNavigationStates { get; set; }
        public virtual System.Windows.Controls.ScrollBarVisibility HorizontalScrollbarVisibility { get; set; }
        public bool IsHelpVisible { get; set; }
        public virtual System.Windows.Size MaxSize { get; set; }
        public virtual System.Windows.Size MinSize { get; set; }
        public Orc.Wizard.INavigationController NavigationController { get; set; }
        public Orc.Wizard.INavigationStrategy NavigationStrategy { get; set; }
        public System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardPage> Pages { get; }
        public virtual System.Windows.ResizeMode ResizeMode { get; set; }
        public virtual bool RestoreScrollPositionPerPage { get; set; }
        public bool ShowInTaskbar { get; set; }
        public string Title { get; set; }
        public virtual System.Windows.Controls.ScrollBarVisibility VerticalScrollbarVisibility { get; set; }
        public event System.EventHandler<System.EventArgs> Canceled;
        public event System.EventHandler<System.EventArgs> CurrentPageChanged;
        public event System.EventHandler<System.EventArgs> HelpShown;
        public event System.EventHandler<System.EventArgs> MovedBack;
        public event System.EventHandler<System.EventArgs> MovedForward;
        public event System.EventHandler<System.EventArgs> Resumed;
        public virtual System.Threading.Tasks.Task CancelAsync() { }
        public virtual System.Threading.Tasks.Task CloseAsync() { }
        public virtual Catel.Data.IValidationContext GetValidationContext(Orc.Wizard.IWizardPage wizardPage, bool validate = true) { }
        public virtual Catel.Data.IValidationContext GetValidationContextForCurrentPage(bool validate = true) { }
        public virtual System.Threading.Tasks.Task InitializeAsync() { }
        public void InsertPage(int index, Orc.Wizard.IWizardPage page) { }
        public virtual System.Threading.Tasks.Task MoveBackAsync() { }
        public virtual System.Threading.Tasks.Task MoveForwardAsync() { }
        public virtual System.Threading.Tasks.Task MoveToPageAsync(int indexOfNextPage) { }
        protected override void OnPropertyChanged(Catel.Data.AdvancedPropertyChangedEventArgs e) { }
        protected void RaiseCanceled() { }
        protected void RaiseMovedBack() { }
        protected void RaiseMovedForward() { }
        protected void RaiseResumed() { }
        public void RemovePage(Orc.Wizard.IWizardPage page) { }
        public virtual System.Threading.Tasks.Task ResumeAsync() { }
        [ObsoleteEx(RemoveInVersion="4.0", ReplacementTypeOrMember="ResumeAsync", TreatAsErrorFromVersion="3.0")]
        public virtual System.Threading.Tasks.Task SaveAsync() { }
        protected virtual Orc.Wizard.IWizardPage SetCurrentPage(int newIndex) { }
        public virtual System.Threading.Tasks.Task ShowHelpAsync() { }
        protected virtual System.Threading.Tasks.Task<bool> ValidateAndSaveCurrentPageAsync() { }
    }
    public static class WizardConfiguration
    {
        public static readonly int CannotNavigate;
        public static System.TimeSpan AnimationDuration { get; set; }
    }
    public class WizardNavigationButton : Catel.Data.ModelBase, Orc.Wizard.IWizardNavigationButton
    {
        public static readonly Catel.Data.PropertyData ContentEvaluatorProperty;
        public static readonly Catel.Data.PropertyData ContentProperty;
        public static readonly Catel.Data.PropertyData IsVisibleEvaluatorProperty;
        public static readonly Catel.Data.PropertyData IsVisibleProperty;
        public WizardNavigationButton() { }
        public System.Windows.Input.ICommand Command { get; set; }
        public string Content { get; set; }
        public System.Func<string> ContentEvaluator { get; set; }
        public bool IsVisible { get; set; }
        public System.Func<bool> IsVisibleEvaluator { get; set; }
        public void Update() { }
    }
    public abstract class WizardPageBase : Catel.Data.ModelBase, Orc.Wizard.IWizardPage
    {
        public static readonly Catel.Data.PropertyData BreadcrumbTitleProperty;
        public static readonly Catel.Data.PropertyData DescriptionProperty;
        public static readonly Catel.Data.PropertyData IsOptionalProperty;
        public static readonly Catel.Data.PropertyData IsVisitedProperty;
        public static readonly Catel.Data.PropertyData NumberProperty;
        public static readonly Catel.Data.PropertyData TitleProperty;
        protected WizardPageBase() { }
        public string BreadcrumbTitle { get; set; }
        public string Description { get; set; }
        public bool IsOptional { get; set; }
        public bool IsVisited { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public Catel.MVVM.IViewModel ViewModel { get; set; }
        public Orc.Wizard.IWizard Wizard { get; set; }
        public event System.EventHandler<Orc.Wizard.ViewModelChangedEventArgs> ViewModelChanged;
        public virtual System.Threading.Tasks.Task AfterWizardPagesSavedAsync() { }
        public virtual System.Threading.Tasks.Task CancelAsync() { }
        public virtual Orc.Wizard.ISummaryItem GetSummary() { }
        public virtual System.Threading.Tasks.Task SaveAsync() { }
    }
    public class WizardPageSelectionBehavior : Catel.Windows.Interactivity.BehaviorBase<System.Windows.Controls.ContentControl>
    {
        public static readonly System.Windows.DependencyProperty WizardProperty;
        public WizardPageSelectionBehavior() { }
        public Orc.Wizard.IWizard Wizard { get; set; }
        protected override void OnAssociatedObjectLoaded() { }
        protected override void OnAssociatedObjectUnloaded() { }
    }
    public class WizardPageViewModelBase<TWizardPage> : Catel.MVVM.ViewModelBase, Catel.Data.IValidatable, Catel.MVVM.IViewModel, Orc.Wizard.IWizardPageViewModel, System.ComponentModel.IDataErrorInfo, System.ComponentModel.IDataWarningInfo, System.ComponentModel.INotifyDataErrorInfo, System.ComponentModel.INotifyDataWarningInfo, System.ComponentModel.INotifyPropertyChanged
        where TWizardPage :  class, Orc.Wizard.IWizardPage
    {
        public static readonly Catel.Data.PropertyData WizardPageProperty;
        public WizardPageViewModelBase(TWizardPage wizardPage) { }
        public Catel.MVVM.TaskCommand<Orc.Wizard.IWizardPage> QuickNavigateToPage { get; }
        public Orc.Wizard.IWizard Wizard { get; }
        [Catel.MVVM.Model(SupportIEditableObject=false)]
        public TWizardPage WizardPage { get; }
        public virtual void EnableValidationExposure() { }
        public bool QuickNavigateToPageCanExecute(Orc.Wizard.IWizardPage parameter) { }
        public System.Threading.Tasks.Task QuickNavigateToPageExecuteAsync(Orc.Wizard.IWizardPage parameter) { }
    }
    public class WizardPageViewModelLocator : Catel.MVVM.ViewModelLocator, Catel.MVVM.ILocator, Catel.MVVM.IViewModelLocator, Orc.Wizard.IWizardPageViewModelLocator
    {
        public WizardPageViewModelLocator() { }
    }
    public class WizardService : Orc.Wizard.IWizardService
    {
        public WizardService(Catel.Services.IUIVisualizerService uiVisualizerService) { }
        public System.Threading.Tasks.Task<bool?> ShowWizardAsync(Orc.Wizard.IWizard wizard) { }
    }
}
namespace Orc.Wizard.ViewModels
{
    public class FullScreenWizardViewModel : Orc.Wizard.ViewModels.WizardViewModel
    {
        public FullScreenWizardViewModel(Orc.Wizard.IWizard wizard, Catel.Services.IMessageService messageService, Catel.Services.ILanguageService languageService) { }
    }
    public class SideNavigationWizardViewModel : Orc.Wizard.ViewModels.WizardViewModel
    {
        public SideNavigationWizardViewModel(Orc.Wizard.IWizard wizard, Catel.Services.IMessageService messageService, Catel.Services.ILanguageService languageService) { }
    }
    public class SummaryWizardPageViewModel : Orc.Wizard.WizardPageViewModelBase<Orc.Wizard.SummaryWizardPage>
    {
        public static readonly Catel.Data.PropertyData SummaryItemsProperty;
        public SummaryWizardPageViewModel(Orc.Wizard.SummaryWizardPage wizardPage) { }
        public System.Collections.Generic.List<Orc.Wizard.ISummaryItem> SummaryItems { get; }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
    public class WizardViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData CurrentPageProperty;
        public static readonly Catel.Data.PropertyData IsHelpVisibleProperty;
        public static readonly Catel.Data.PropertyData IsPageOptionalProperty;
        public static readonly Catel.Data.PropertyData MaxSizeProperty;
        public static readonly Catel.Data.PropertyData MinSizeProperty;
        public static readonly Catel.Data.PropertyData PageDescriptionProperty;
        public static readonly Catel.Data.PropertyData PageTitleProperty;
        public static readonly Catel.Data.PropertyData ResizeModeProperty;
        public static readonly Catel.Data.PropertyData ShowInTaskbarProperty;
        public static readonly Catel.Data.PropertyData WizardButtonsProperty;
        public static readonly Catel.Data.PropertyData WizardPagesProperty;
        public static readonly Catel.Data.PropertyData WizardProperty;
        public WizardViewModel(Orc.Wizard.IWizard wizard, Catel.Services.IMessageService messageService, Catel.Services.ILanguageService languageService) { }
        [Catel.MVVM.ViewModelToModel("Wizard", "CurrentPage")]
        public Orc.Wizard.IWizardPage CurrentPage { get; set; }
        [Catel.MVVM.ViewModelToModel("Wizard", "IsHelpVisible")]
        public bool IsHelpVisible { get; set; }
        public bool IsPageOptional { get; }
        [Catel.MVVM.ViewModelToModel("Wizard", "MaxSize")]
        public System.Windows.Size MaxSize { get; set; }
        [Catel.MVVM.ViewModelToModel("Wizard", "MinSize")]
        public System.Windows.Size MinSize { get; set; }
        public string PageDescription { get; }
        public string PageTitle { get; }
        [Catel.MVVM.ViewModelToModel("Wizard", "ResizeMode")]
        public System.Windows.ResizeMode ResizeMode { get; set; }
        public Catel.MVVM.TaskCommand ShowHelp { get; set; }
        [Catel.MVVM.ViewModelToModel("Wizard", "ShowInTaskbar")]
        public bool ShowInTaskbar { get; set; }
        [Catel.MVVM.Model(SupportIEditableObject=false)]
        public Orc.Wizard.IWizard Wizard { get; set; }
        public System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardNavigationButton> WizardButtons { get; }
        public System.Collections.Generic.IEnumerable<Orc.Wizard.IWizardPage> WizardPages { get; }
        protected override System.Threading.Tasks.Task<bool> CancelAsync() { }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
        public Orc.Wizard.IWizardPage get_CurrentPage() { }
        public bool get_IsHelpVisible() { }
        public System.Windows.Size get_MaxSize() { }
        public System.Windows.Size get_MinSize() { }
        public System.Windows.ResizeMode get_ResizeMode() { }
        public bool get_ShowInTaskbar() { }
        public void set_CurrentPage(Orc.Wizard.IWizardPage ) { }
        public void set_IsHelpVisible(bool ) { }
        public void set_MaxSize(System.Windows.Size ) { }
        public void set_MinSize(System.Windows.Size ) { }
        public void set_ResizeMode(System.Windows.ResizeMode ) { }
        public void set_ShowInTaskbar(bool ) { }
    }
}
namespace Orc.Wizard.Views
{
    public class FullScreenWizardWindow : Catel.Windows.DataWindow, System.Windows.Markup.IComponentConnector
    {
        public FullScreenWizardWindow() { }
        public FullScreenWizardWindow(Orc.Wizard.ViewModels.FullScreenWizardViewModel viewModel) { }
        public void InitializeComponent() { }
        protected override void OnLoaded(System.EventArgs e) { }
        protected override void OnViewModelPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e) { }
    }
    public class SideNavigationWizardWindow : Catel.Windows.DataWindow, System.Windows.Markup.IComponentConnector
    {
        public SideNavigationWizardWindow() { }
        public SideNavigationWizardWindow(Orc.Wizard.ViewModels.SideNavigationWizardViewModel viewModel) { }
        public void InitializeComponent() { }
        protected override void OnLoaded(System.EventArgs e) { }
        protected override void OnViewModelChanged() { }
        protected override void OnViewModelPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e) { }
    }
    public class SummaryWizardPageView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public SummaryWizardPageView() { }
        public void InitializeComponent() { }
    }
    public class WizardWindow : Catel.Windows.DataWindow, System.Windows.Markup.IComponentConnector
    {
        public WizardWindow() { }
        public WizardWindow(Orc.Wizard.ViewModels.WizardViewModel viewModel) { }
        public void InitializeComponent() { }
        protected override void OnLoaded(System.EventArgs e) { }
        protected override void OnViewModelPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e) { }
    }
}