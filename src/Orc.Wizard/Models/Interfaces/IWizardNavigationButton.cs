namespace Orc.Wizard
{
    using System;
    using System.Windows.Input;

    public interface IWizardNavigationButton
    {
        string Content { get; }
        bool IsVisible { get; }
        ICommand Command { get; }

        void Update();
    }
}
