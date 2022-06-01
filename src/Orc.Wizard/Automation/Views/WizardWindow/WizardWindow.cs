namespace Orc.Wizard.Automation
{
    using System.Windows.Automation;
    using Orc.Automation;
    using Orc.Automation.Controls;

    [AutomatedControl(Class = typeof(Views.WizardWindow))]
    public class WizardWindow : Window<WindowModel, WizardWindowMap>
    {
        public WizardWindow(AutomationElement element)
            : base(element)
        {
        }
    }
}
