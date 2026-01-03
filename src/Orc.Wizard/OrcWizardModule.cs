namespace Orc.Wizard
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcWizardModule
    {
        public static IServiceCollection AddOrcWizard(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IWizardService, WizardService>();
            serviceCollection.TryAddSingleton<IWizardPageViewModelLocator, WizardPageViewModelLocator>();

            var themeManager = ControlzEx.Theming.ThemeManager.Current;
            themeManager.RegisterLibraryThemeProvider(new LibraryThemeProvider());

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.Wizard", "Orc.Wizard.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.Wizard", "https://github.com/wildgums/orc.wizard"));

            return serviceCollection;
        }
    }
}
