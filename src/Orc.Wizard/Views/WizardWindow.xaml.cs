namespace Orc.Wizard.Views;

using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Catel.Services;
using Catel.Windows;
using ViewModels;

public partial class WizardWindow
{
    private readonly ILanguageService _languageService;
    private readonly IMessageService _messageService;

    public WizardWindow(IServiceProvider serviceProvider, IWrapControlService wrapControlService,
        ILanguageService languageService, IMessageService messageService)
        : base(serviceProvider, wrapControlService, languageService)
    {
        _languageService = languageService;
        _messageService = messageService;

        InitializeComponent();

        Mode = DataWindowMode.Custom;
        InfoBarMessageControlGenerationMode = InfoBarMessageControlGenerationMode.Overlay;
    }

    protected override void OnLoaded(EventArgs e)
    {
        base.OnLoaded(e);

        Dispatcher.BeginInvoke(() =>
        {
            UpdateOpacityMask();
        });
    }

    protected override async Task<bool> DiscardChangesAsync()
    {
        var wizard = ViewModel is WizardViewModel vm ? vm.Wizard : null;
        if (wizard is not null)
        {
            if (!wizard.IsCanceling)
            {
                if (await _messageService.ShowAsync(_languageService.GetRequiredString("Wizard_AreYouSureYouWantToCancelWizard"), button: MessageButton.YesNo) == MessageResult.No)
                {
                    return false;
                }
            }
        }

        return await base.DiscardChangesAsync();
    }

    protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnViewModelPropertyChanged(e);

#pragma warning disable WPF1014
        if (e.HasPropertyChanged(nameof(IWizard.CurrentPage)))
#pragma warning restore WPF1014
        {
#pragma warning disable AvoidAsyncVoid
            Dispatcher.BeginInvoke(async () =>
            {
#pragma warning restore AvoidAsyncVoid
                var vm = (WizardViewModel?) ViewModel;
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

    //protected override AutomationPeer OnCreateAutomationPeer()
    //{
    //    return new WizardWindowPeer(this);
    //}
}
