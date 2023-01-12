namespace Orc.Wizard.Example.Wizard
{
    public class AgeWizardPage : WizardPageBase
    {
        public AgeWizardPage()
        {
            Title = "Age";
            Description = "Specify the age of the person";
            IsOptional = true;
        }

        public string Age { get; set; }

        public override ISummaryItem GetSummary()
        {
            return new SummaryItem
            {
                Title = "Age",
                Summary = Age
            };
        }
    }
}
