// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageSelectionBehavior.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.MVVM.Views;
    using Catel.Windows.Interactivity;


    public class WizardPageSelectionBehavior : BehaviorBase<ContentControl>
    {
        #region Fields
        private IWizardPage _lastPage;
        #endregion

        #region Properties
        public IWizard Wizard
        {
            get { return (IWizard)GetValue(WizardProperty); }
            set { SetValue(WizardProperty, value); }
        }

        public static readonly DependencyProperty WizardProperty = DependencyProperty.Register("Wizard", typeof(IWizard),
            typeof(WizardPageSelectionBehavior), new PropertyMetadata(OnWizardChanged));
        #endregion

        private static void OnWizardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as WizardPageSelectionBehavior;
            if (behavior != null)
            {
                if (e.NewValue == null)
                {
                    return;
                }

                behavior.UpdatePage();

                var oldWizard = e.OldValue as IWizard;
                if (oldWizard != null)
                {
                    oldWizard.MovedBack -= behavior.OnMovedBack;
                    oldWizard.MovedForward -= behavior.OnMovedForward;
                }

                var wizard = behavior.Wizard;
                if (wizard != null)
                {
                    wizard.MovedBack += behavior.OnMovedBack;
                    wizard.MovedForward += behavior.OnMovedForward;
                }
            }
        }

        protected override void OnAssociatedObjectLoaded()
        {
            UpdatePage();
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            base.OnAssociatedObjectUnloaded();

            var wizard = Wizard;
            if (wizard == null)
            {
                return;
            }

            wizard.MovedBack -= OnMovedBack;
            wizard.MovedForward -= OnMovedForward;

            Wizard = null;
        }

        private void OnMovedForward(object sender, EventArgs e)
        {
            UpdatePage();
        }

        private void OnMovedBack(object sender, EventArgs e)
        {
            UpdatePage();
        }

        private void UpdatePage()
        {
            if (AssociatedObject == null)
            {
                return;
            }

            var wizard = Wizard;
            if (wizard == null)
            {
                return;
            }

            if (_lastPage != null)
            {
                _lastPage.ViewModel = null;
                _lastPage = null;
            }

            _lastPage = wizard.CurrentPage;

            var serviceLocator = this.GetServiceLocator();
            var viewModelLocator = serviceLocator.ResolveType<IWizardPageViewModelLocator>();
            var pageViewModelType = viewModelLocator.ResolveViewModel(_lastPage.GetType());

            var viewLocator = serviceLocator.ResolveType<IViewLocator>();
            var viewType = viewLocator.ResolveView(pageViewModelType);

            var typeFactory = serviceLocator.ResolveType<ITypeFactory>();
            var view = typeFactory.CreateInstance(viewType) as IView;
            if (view == null)
            {
                return;
            }

            // TODO: Consider setting the data context to a model instead of manually creating a vm
            var viewModelFactory = serviceLocator.ResolveType<IViewModelFactory>();
            var viewModel = viewModelFactory.CreateViewModel(pageViewModelType, wizard.CurrentPage);

            _lastPage.ViewModel = viewModel;

            view.DataContext = viewModel;
            AssociatedObject.Content = view;
        }
    }
}