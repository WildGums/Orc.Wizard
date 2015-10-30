// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardBase.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.Threading;

    public abstract class WizardBase : ModelBase, IWizard
    {
        #region Fields
        private readonly IList<IWizardPage> _pages = new List<IWizardPage>();
        private readonly ITypeFactory _typeFactory;

        private int _currentIndex = 0;
        private IWizardPage _currentPage;
        #endregion

        protected WizardBase(ITypeFactory typeFactory)
        {
            Argument.IsNotNull(() => typeFactory);

            _typeFactory = typeFactory;
        }

        #region Properties
        public IWizardPage CurrentPage
        {
            get
            {
                if (_currentPage == null)
                {
                    _currentPage = _pages[_currentIndex];
                }

                return _currentPage;
            }
        }

        public IEnumerable<IWizardPage> Pages
        {
            get { return _pages.AsEnumerable(); }
        }

        public string Title { get; protected set; }

        public virtual bool CanResume
        {
            get { return _currentIndex == _pages.Count - 1; }
        }

        public virtual bool CanCancel
        {
            get { return true; }
        }

        public virtual bool CanMoveForward
        {
            get
            {
                if (_currentPage != null)
                {
                    var vm = _currentPage.ViewModel;
                    if (vm != null)
                    {
                        if (!vm.ValidateViewModel())
                        {
                            return false;
                        }
                    }
                }

                return _pages.Any() && _currentIndex + 1 < _pages.Count;
            }
        }

        public virtual bool CanMoveBack
        {
            get { return _pages.Any() && _currentIndex - 1 >= 0; }
        }
        #endregion

        #region Methods
        protected virtual void AddPage(IWizardPage page)
        {
            Argument.IsNotNull(() => page);

            page.Wizard = this;
            _pages.Add(page);
        }

        protected void AddPage<TWizardPage>()
            where TWizardPage : IWizardPage
        {
            var page = _typeFactory.CreateInstance<TWizardPage>();

            AddPage(page);
        }

        public virtual Task ResumeAsync()
        {
            return TaskHelper.Completed;
        }

        public virtual async Task CancelAsync()
        {
            // TODO: cancel all pages with view models
        }

        public virtual async Task MoveForwardAsync()
        {
            if (!CanMoveForward)
            {
                return;
            }

            var currentPage = _currentPage;
            if (currentPage != null)
            {
                var vm = currentPage.ViewModel;
                if (vm != null)
                {
                    await vm.SaveAndCloseViewModelAsync();
                }
            }

            SetCurrentPage(_currentIndex + 1);

            MovedForward.SafeInvoke(this);
        }

        public virtual async Task MoveBackAsync()
        {
            if (!CanMoveBack)
            {
                return;
            }

            // TODO: cancel or remember state?

            SetCurrentPage(_currentIndex - 1);

            MovedBack.SafeInvoke(this);
        }

        protected virtual IWizardPage SetCurrentPage(int newIndex)
        {
            var currentPage = _currentPage;
            if (currentPage != null)
            {
                currentPage.ViewModelChanged -= OnPageViewModelChanged;

                var vm = currentPage.ViewModel;
                if (vm != null)
                {
                    vm.PropertyChanged -= OnPageViewModelPropertyChanged;
                }
            }

            _currentPage = null;
            _currentIndex = newIndex;

            var newPage = CurrentPage;
            if (newPage != null)
            {
                newPage.ViewModelChanged += OnPageViewModelChanged;

                var vm = newPage.ViewModel;
                if (vm != null)
                {
                    vm.PropertyChanged += OnPageViewModelPropertyChanged;
                }
            }

            return newPage;
        }

        private void OnPageViewModelChanged(object sender, ViewModelChangedEventArgs e)
        {
            var oldVm = e.OldViewModel;
            if (oldVm != null)
            {
                oldVm.PropertyChanged -= OnPageViewModelPropertyChanged;
            }

            var newVm = e.NewViewModel;
            if (newVm != null)
            {
                newVm.PropertyChanged += OnPageViewModelPropertyChanged;
            }
        }

        private void OnPageViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => CanMoveBack);
            RaisePropertyChanged(() => CanMoveForward);
            RaisePropertyChanged(() => CanResume);
            RaisePropertyChanged(() => CanCancel);
        }

        public event EventHandler MovedForward;
        public event EventHandler MovedBack;
        #endregion
    }
}