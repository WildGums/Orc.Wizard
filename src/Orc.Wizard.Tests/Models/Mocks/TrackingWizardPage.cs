namespace Orc.Wizard.Tests;

using System;
using System.Threading.Tasks;
using Catel.Fody;

public class TrackingWizardPage : WizardPageBase
{
    [NoWeaving]
    public Action OnCancelAsyncCallback { get; set; } = () => { };

    public override Task CancelAsync()
    {
        OnCancelAsyncCallback.Invoke();
        return base.CancelAsync();
    }
}
