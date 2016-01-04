// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewProjectWizardWindow.xaml.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Views
{
    using System.ComponentModel;
    using System.Windows.Controls;
    using Catel.Windows;
    using ViewModels;

    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow
    {
        #region Constructors
        public WizardWindow()
            : this(null)
        {
        }

        public WizardWindow(WizardViewModel viewModel)
            : base(viewModel, DataWindowMode.Custom, infoBarMessageControlGenerationMode: InfoBarMessageControlGenerationMode.Overlay)
        {
            InitializeComponent();
        }

        protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnViewModelPropertyChanged(e);

            if (e.HasPropertyChanged("CurrentPage"))
            {
                var scrollviewer = breadcrumb.FindLogicalOrVisualAncestorByType<ScrollViewer>();
                if (scrollviewer != null)
                {
                    // TODO: scroll somehow
                }
            }
        }
        #endregion
    }
}