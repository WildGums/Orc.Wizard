namespace Orc.Wizard.Example;

using System.Globalization;
using System.Windows;
using Catel;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orc.Controls;
using Orc.FileSystem;
using Orc.LogViewer;
using Orc.Serialization.Json;
using Orc.SystemInfo;
using Orc.Theming;
using Orc.Wizard.Example.ViewModels;
using Orc.Wizard.Example.Views;
using Orchestra;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
#pragma warning disable IDISP006 // Implement IDisposable
    private readonly IHost _host;
#pragma warning restore IDISP006 // Implement IDisposable

    public App()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddCatelCore();
                services.AddCatelMvvm();
                services.AddOrcControls();
                services.AddOrcFileSystem();
                services.AddOrcLogViewer();
                services.AddOrcSerializationJson();
                services.AddOrcSystemInfo();
                services.AddOrcTheming();
                services.AddOrcWizard();
                services.AddOrchestraCore();

                services.AddLogging(x =>
                {
                    x.AddConsole();
                    x.AddDebug();
                });

                services.AddSingleton<IMonitorAwareUIVisualizerService, MonitorAwareUIVisualizerService>();
                services.AddSingleton<IMonitorAwareWizardService, MonitorAwareWizardService>();

                services.AddSingleton<ViewModelLocatorInitializer>();
            });

        _host = hostBuilder.Build();

        IoCContainer.ServiceProvider = _host.Services;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceProvider = IoCContainer.ServiceProvider;

        serviceProvider.CreateTypesThatMustBeConstructedAtStartup();

        var languageService = serviceProvider.GetRequiredService<ILanguageService>();

        // Note: it's best to use .CurrentUICulture in actual apps since it will use the preferred language
        // of the user. But in order to demo multilingual features for devs (who mostly have en-US as .CurrentUICulture),
        // we use .CurrentCulture for the sake of the demo
        languageService.PreferredCulture = CultureInfo.CurrentCulture;
        languageService.FallbackCulture = new CultureInfo("en-US");

        StyleHelper.CreateStyleForwardersForDefaultStyles();

        this.ApplyTheme();

        var mainWindow = ActivatorUtilities.CreateInstance<MainView>(_host.Services);
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }

        base.OnExit(e);
    }

    private class ViewModelLocatorInitializer : IConstructAtStartup
    {
        public ViewModelLocatorInitializer(IViewModelLocator viewModelLocator)
        {
            viewModelLocator.Register(typeof(MainView), typeof(MainViewModel));
        }
    }
}
