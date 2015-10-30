// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example
{
    using System.Windows;
    using Catel.Logging;
    using Catel.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            LogManager.AddDebugListener(true);
#endif

            StyleHelper.CreateStyleForwardersForDefaultStyles();
        }
    }
}