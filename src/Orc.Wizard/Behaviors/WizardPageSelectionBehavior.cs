namespace Orc.Wizard;

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Catel;
using Catel.IoC;
using Catel.MVVM;
using Catel.MVVM.Views;
using Catel.Windows;
using Catel.Windows.Interactivity;

public class WizardPageSelectionBehavior : BehaviorBase<ContentControl>
{
    private readonly ConditionalWeakTable<object, ScrollInfo> _scrollPositions = new();
    private readonly ConditionalWeakTable<object, CachedView> _cachedViews = new();

    private ScrollViewer? _scrollViewer;
    private IWizardPage? _lastPage;

    public IWizard? Wizard
    {
        get { return (IWizard?)GetValue(WizardProperty); }
        set { SetValue(WizardProperty, value); }
    }

    public static readonly DependencyProperty WizardProperty = DependencyProperty.Register(nameof(Wizard), typeof(IWizard),
        typeof(WizardPageSelectionBehavior), new PropertyMetadata(OnWizardChanged));

    private bool CacheViews
    {
        get
        {
            return Wizard?.CacheViews ?? true;
        }
    }

    private static void OnWizardChanged(DependencyObject? d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not WizardPageSelectionBehavior behavior)
        {
            return;
        }

        behavior.UpdatePage();

        if (e.OldValue is IWizard oldWizard)
        {
            oldWizard.CurrentPageChanged -= behavior.OnCurrentPageChanged;
            oldWizard.MovedBack -= behavior.OnMovedBack;
            oldWizard.MovedForward -= behavior.OnMovedForward;
        }

        if (e.NewValue is IWizard wizard)
        {
            wizard.CurrentPageChanged += behavior.OnCurrentPageChanged;
            wizard.MovedBack += behavior.OnMovedBack;
            wizard.MovedForward += behavior.OnMovedForward;
        }
    }

    protected override void OnAssociatedObjectLoaded()
    {
        _scrollViewer = AssociatedObject?.FindLogicalOrVisualAncestorByType<ScrollViewer>();

        UpdatePage();
    }

    protected override void OnAssociatedObjectUnloaded()
    {
        base.OnAssociatedObjectUnloaded();

        var wizard = Wizard;
        if (wizard is null)
        {
            return;
        }

        wizard.CurrentPageChanged -= OnCurrentPageChanged;
        wizard.MovedBack -= OnMovedBack;
        wizard.MovedForward -= OnMovedForward;

        SetCurrentValue(WizardProperty, null);
    }

    private void OnCurrentPageChanged(object? sender, EventArgs e)
    {
        UpdatePage();
    }

    private void OnMovedForward(object? sender, EventArgs e)
    {
        UpdatePage();
    }

    private void OnMovedBack(object? sender, EventArgs e)
    {
        UpdatePage();
    }

#pragma warning disable WPF0005 // Name of PropertyChangedCallback should match registered name.
    private void UpdatePage()
#pragma warning restore WPF0005 // Name of PropertyChangedCallback should match registered name.
    {
        if (AssociatedObject is null)
        {
            return;
        }

        var wizard = Wizard;
        if (wizard is null)
        {
            return;
        }

        var scrollViewer = _scrollViewer;

        var lastPage = _lastPage;
        if (lastPage is not null)
        {
            if (ReferenceEquals(lastPage, wizard.CurrentPage))
            {
                // Nothing has really changed
                return;
            }

            if (scrollViewer is not null)
            {
                _scrollPositions.AddOrUpdate(lastPage, new ScrollInfo
                {
                    VerticalOffset = scrollViewer.VerticalOffset,
                    HorizontalOffset = scrollViewer.HorizontalOffset
                });
            }

            // Even though we cache views, we need to re-use the vm's since the view models will be closed when moving next
            //_lastPage.ViewModel = null;

            if (CacheViews)
            {
                _cachedViews.AddOrUpdate(lastPage, new CachedView
                {
                    View = AssociatedObject.Content as IView
                });
            }

            _lastPage = null;
        }

        _lastPage = wizard.CurrentPage;

        if (_lastPage is null)
        {
            return;
        }

        var dependencyResolver = this.GetDependencyResolver();
        var viewModelLocator = dependencyResolver.ResolveRequired<IWizardPageViewModelLocator>();
        var pageViewModelType = viewModelLocator.ResolveViewModel(_lastPage.GetType());
        if (pageViewModelType is null)
        {
            throw new InvalidOperationException($"Cannot find page view model type of view '{_lastPage.GetType().Name}'");
        }

        var viewLocator = dependencyResolver.ResolveRequired<IViewLocator>();
        var viewType = viewLocator.ResolveView(pageViewModelType);
        if (viewType is null)
        {
            throw new InvalidOperationException($"Cannot find page view type of view model '{pageViewModelType.Name}'");
        }

        var typeFactory = dependencyResolver.ResolveRequired<ITypeFactory>();

        IView? view = null;

        if (_cachedViews.TryGetValue(_lastPage, out var cachedView))
        {
            view = cachedView.View;
        }

        if (view is null)
        {
            view = typeFactory.CreateRequiredInstance(viewType) as IView;
            if (view is null)
            {
                return;
            }
        }

        var viewModelFactory = dependencyResolver.ResolveRequired<IViewModelFactory>();
        var viewModel = viewModelFactory.CreateRequiredViewModel(pageViewModelType, wizard.CurrentPage);

        view.DataContext = viewModel;

        _lastPage.ViewModel = viewModel;

        AssociatedObject.SetCurrentValue(ContentControl.ContentProperty, view);

        var verticalScrollViewerOffset = 0d;
        var horizontalScrollViewerOffset = 0d;

        if (_scrollPositions.TryGetValue(_lastPage, out var scrollInfo))
        {
            verticalScrollViewerOffset = scrollInfo.VerticalOffset;
            horizontalScrollViewerOffset = scrollInfo.HorizontalOffset;
        }

        if (scrollViewer is not null &&
            (Wizard?.RestoreScrollPositionPerPage ?? true))
        {
            scrollViewer.ScrollToVerticalOffset(verticalScrollViewerOffset);
            scrollViewer.ScrollToHorizontalOffset(horizontalScrollViewerOffset);
        }
    }

    private class ScrollInfo
    {
        public double VerticalOffset { get; set; }

        public double HorizontalOffset { get; set; }
    }

    private class CachedView
    {
        public IView? View { get; set; }
    }
}
