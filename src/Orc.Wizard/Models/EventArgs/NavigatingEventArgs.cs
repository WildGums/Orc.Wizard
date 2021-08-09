namespace Orc.Wizard
{
    using System;

    public class NavigatingEventArgs : EventArgs
    {
        public NavigatingEventArgs(IWizardPage from, IWizardPage to, bool cancel)
        {
            From = from;
            To = to;
            Cancel = cancel;
        }

        public IWizardPage From { get; }

        public IWizardPage To { get; }

        public bool Cancel { get; }
    }
}
