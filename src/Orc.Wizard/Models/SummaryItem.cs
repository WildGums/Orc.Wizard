namespace Orc.Wizard
{
    public class SummaryItem : ISummaryItem
    {
        public IWizardPage? Page { get; set; }

        public string? Title { get; set; }

        public string? Summary { get; set; }
    }
}
