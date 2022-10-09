namespace Orc.Wizard.Automation
{
    using Orc.Automation;

    [ActiveAutomationModel]
    public class BreadcrumbItemModel : ControlModel
    {
        public BreadcrumbItemModel(AutomationElementAccessor accessor)
            : base(accessor)
        {
        }

        public IWizardPage? Page { get; set; }
        public IWizardPage? CurrentPage { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Number { get; set; }
    }
}
