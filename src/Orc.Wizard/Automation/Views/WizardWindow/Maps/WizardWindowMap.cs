namespace Orc.Wizard.Automation
{
    using System.Windows.Automation;
    using Orc.Automation;
    using Orc.Automation.Controls;

    public class WizardWindowMap : AutomationBase
    {
        public WizardWindowMap(AutomationElement element)
            : base(element)
        {
        }

        public List BreadCrumbList => By.Id("breadcrumb").One<List>();
    }
}
