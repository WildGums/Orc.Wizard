// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleInitializer.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
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
        public static void Initialize()
        {
            var serviceLocator = ServiceLocator.Default;

            serviceLocator.RegisterType<IWizardService, WizardService>();
            serviceLocator.RegisterType<IWizardPageViewModelLocator, WizardPageViewModelLocator>();

            var languageService = serviceLocator.ResolveType<ILanguageService>();
            languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.Wizard", "Orc.Wizard.Properties", "Resources"));
        }
    }
}