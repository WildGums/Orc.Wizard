namespace Orc.Wizard
{
    using System;
    using Catel.MVVM;

    public class ViewModelChangedEventArgs : EventArgs
    {
        public ViewModelChangedEventArgs(IViewModel? oldViewModel, IViewModel? newViewModel)
        {
            OldViewModel = oldViewModel;
            NewViewModel = newViewModel;
        }

        public IViewModel? OldViewModel { get; private set; }
        public IViewModel? NewViewModel { get; private set; }
    }
}
