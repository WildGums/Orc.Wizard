// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Reflection;
    using Catel.Threading;

    public abstract class WizardBase : ModelBase, IWizard
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IList<IWizardPage> _pages = new List<IWizardPage>();
        private readonly ITypeFactory _typeFactory;

        private int _currentIndex = 0;
        private IWizardPage _currentPage;
        #endregion

        // Note: we can't remove this constructor, it would be a breaking change
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

        #region Events
        public event EventHandler MovedForward;
        public event EventHandler MovedBack;
        #endregion

        #region Methods
        public void InsertPage(int index, IWizardPage page)
        {
            Argument.IsNotNull(() => page);

            Log.Debug("Adding page '{0}' to index '{1}'", page.GetType().GetSafeFullName(), index);

            page.Wizard = this;

            _pages.Insert(index, page);

            UpdatePageNumbers();
        }

        public void RemovePage(IWizardPage page)
        {
            Argument.IsNotNull(() => page);

            for (int i = 0; i < _pages.Count; i++)
            {
                if (ReferenceEquals(page, _pages[i]))
                {
                    Log.Debug("Removing page '{0}' at index '{1}'", page.GetType().GetSafeFullName(), i);

                    page.Wizard = null;
                    _pages.RemoveAt(i--);
                }
            }

            UpdatePageNumbers();
        }

        public virtual async Task SaveAsync()
        {
            Log.Debug("Canceling wizard '{0}'", GetType().GetSafeFullName());

            foreach (var page in _pages)
            {
                await page.SaveAsync();
            }
        }

        public virtual async Task CancelAsync()
        {
            Log.Debug("Canceling wizard '{0}'", GetType().GetSafeFullName());

            foreach (var page in _pages)
            {
                await page.CancelAsync();
            }
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
                    var result = await vm.SaveAndCloseViewModelAsync();
                    if (!result)
                    {
                        return;
                    }
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

            SetCurrentPage(_currentIndex - 1);

            MovedBack.SafeInvoke(this);
        }

        protected virtual IWizardPage SetCurrentPage(int newIndex)
        {
            Log.Debug("Setting current page index to '{0}'", newIndex);

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

            RaisePropertyChanged("CurrentPage");

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

        private void UpdatePageNumbers()
        {
            var counter = 1;

            foreach (var page in _pages)
            {
                page.Number = counter++;
            }
        }
        #endregion
    }
}