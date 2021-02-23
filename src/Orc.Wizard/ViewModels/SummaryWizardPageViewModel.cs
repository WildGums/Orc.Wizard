namespace Orc.Wizard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Catel.MVVM;

    public class SummaryWizardPageViewModel : WizardPageViewModelBase<SummaryWizardPage>
    {
        public SummaryWizardPageViewModel(SummaryWizardPage wizardPage) 
            : base(wizardPage)
        {
            SummaryItems = new ObservableCollection<ISummaryItem>();
            LabelMouseDown = new Command<object, object>(LabelMouseDownExecuteAsync, LabelMouseDownCanExecute);
        }

        public ObservableCollection<ISummaryItem> SummaryItems { get; private set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var wizard = Wizard;
            if (wizard is null)
            {
                return;
            }

            foreach (var page in wizard.Pages)
            {
                var summary = page.GetSummary();
                if (summary != null)
                {
                    SummaryItems.Add(summary);
                }
            }
        }

        #region Commands

        public Command<object, object> LabelMouseDown { get; set; }

        public bool LabelMouseDownCanExecute(object parameter)
            => Wizard.AllowQuickNavigation;

        public void LabelMouseDownExecuteAsync(object parameter)
        {
            var sumitem = parameter as SummaryItem;
            if (sumitem == null)
                return;
            IWizardPage page = null;
            foreach (var _page in Wizard.Pages)
            {
                if (_page.Title.Contains(sumitem.Title))
                {
                    page = _page;
                    break;
                }
            }
            if (page != null && page.IsVisited && Wizard.Pages is System.Collections.Generic.List<IWizardPage>)
            {
                var list = Wizard.Pages as System.Collections.Generic.List<IWizardPage>;
                var idx = list.IndexOf(page);
                Wizard.MoveToPageAsync(idx);
            }
        }
        #endregion
    }
}
