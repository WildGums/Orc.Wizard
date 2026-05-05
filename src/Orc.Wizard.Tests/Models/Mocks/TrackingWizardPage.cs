namespace Orc.Wizard.Tests;

using System;
using System.Threading.Tasks;

public class TrackingWizardPage : WizardPageBase
{
    public Action OnCancelAsyncCallback { get; set; } = () => { };

    public override Task CancelAsync()
    {
        OnCancelAsyncCallback.Invoke();
        return base.CancelAsync();
    }
}
