using System.Runtime.CompilerServices;
namespace Orc.Wizard;

using Catel.IoC;
using Catel.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    [ModuleInitializer]
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IWizardService, WizardService>();
        serviceLocator.RegisterType<IWizardPageViewModelLocator, WizardPageViewModelLocator>();

        var themeManager = ControlzEx.Theming.ThemeManager.Current;
        themeManager.RegisterLibraryThemeProvider(new LibraryThemeProvider());

        var languageService = serviceLocator.ResolveRequiredType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.Wizard", "Orc.Wizard.Properties", "Resources"));
    }
}