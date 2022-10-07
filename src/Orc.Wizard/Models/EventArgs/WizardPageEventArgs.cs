namespace Orc.Wizard
{
    using System;

    public class WizardPageEventArgs : EventArgs
    {
        public WizardPageEventArgs(IWizardPage wizardPage)
        {
            ArgumentNullException.ThrowIfNull(wizardPage);

            WizardPage = wizardPage;
        }

        public IWizardPage WizardPage { get; private set; }
    }
}
