namespace Orc.Wizard.Automation;

using Orc.Automation;

public class WizardWindowPeer : AutomationWindowPeerBase<Views.WizardWindow>
{
    public WizardWindowPeer(Views.WizardWindow owner)
        : base(owner)
    {
    }
}