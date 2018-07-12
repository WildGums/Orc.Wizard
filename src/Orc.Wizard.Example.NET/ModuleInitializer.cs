using Catel.IoC;
using Catel.IO;
using Catel.MVVM;

using Orc.Wizard.Example.Views;
using Orc.Wizard.Example.ViewModels;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;
        var viewModelLocator = serviceLocator.ResolveType<IViewModelLocator>();
        viewModelLocator.Register(typeof(MainView), typeof(MainViewModel));
    }
}