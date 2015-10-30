// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Views
{
    using Catel.Logging;
    using Logging;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();

            var logListener = new TextBoxLogListener(loggingTextBox)
            {
                IgnoreCatelLogging = true
            };

            LogManager.AddListener(logListener);
        }
    }
}