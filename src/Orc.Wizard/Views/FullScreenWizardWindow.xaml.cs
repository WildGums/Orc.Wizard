namespace Orc.Wizard.Views;

using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Catel.Windows;
using Orc.Wizard;
using ViewModels;

public partial class FullScreenWizardWindow
{
    public FullScreenWizardWindow()
        : this(null)
    {
    }

    public FullScreenWizardWindow(FullScreenWizardViewModel? viewModel)
        : base(viewModel, DataWindowMode.Custom, infoBarMessageControlGenerationMode: InfoBarMessageControlGenerationMode.Overlay)
    {
        InitializeComponent();
    }

    protected override void OnLoaded(EventArgs e)
    {
        base.OnLoaded(e);

        Dispatcher.BeginInvoke(() =>
        {
            UpdateOpacityMask();
        });
    }

    protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnViewModelPropertyChanged(e);

#pragma warning disable WPF1014
        if (e.HasPropertyChanged("CurrentPage"))
#pragma warning restore WPF1014
        {
#pragma warning disable AvoidAsyncVoid
            Dispatcher.BeginInvoke(async () =>
            {
#pragma warning restore AvoidAsyncVoid
                var vm = (FullScreenWizardViewModel?) ViewModel;
                if (vm is null)
                {
                    return;
                }

                breadcrumb.CenterSelectedItem();
                breadcrumbProgress.UpdateProgress(vm.Wizard?.CurrentPage?.Number ?? 0, vm.Wizard?.Pages.Count() ?? 0);

                // We need to await the animation
                await Task.Delay(WizardConfiguration.AnimationDuration);

                UpdateOpacityMask();
            });
        }
    }

    private void UpdateOpacityMask()
    {
        var scrollViewer = breadcrumb.FindVisualDescendantByType<ScrollViewer>();
        if (scrollViewer is null)
        {
            return;
        }

        var opacityMask = new LinearGradientBrush();
        if (scrollViewer.HorizontalOffset > 0d)
        {
            opacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 0d));
            opacityMask.GradientStops.Add(new GradientStop(Colors.Black, 0.05d));
        }

        var scrollableWidth = scrollViewer.ScrollableWidth;
        if (scrollableWidth > scrollViewer.HorizontalOffset)
        {
            opacityMask.GradientStops.Add(new GradientStop(Colors.Black, 0.95d));
            opacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 1d));
        }

        breadcrumb.SetCurrentValue(OpacityMaskProperty, opacityMask.GradientStops.Count > 0 ? opacityMask : null);
    }
}