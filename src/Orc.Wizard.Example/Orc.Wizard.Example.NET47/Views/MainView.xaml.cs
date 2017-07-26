// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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