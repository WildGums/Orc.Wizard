namespace Orc.Wizard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class SummaryWizardPageViewModel : WizardPageViewModelBase<SummaryWizardPage>
    {
        public SummaryWizardPageViewModel(SummaryWizardPage wizardPage) 
            : base(wizardPage)
        {
            SummaryItems = new ObservableCollection<ISummaryItem>();
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
    }
}
