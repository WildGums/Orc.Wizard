// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryWizardPage.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using Catel;
    using Catel.Services;

    public class SummaryWizardPage : WizardPageBase
    {
        private readonly ILanguageService _languageService;

        public SummaryWizardPage(ILanguageService languageService)
        {
            Argument.IsNotNull(() => languageService);

            _languageService = languageService;

            Title = _languageService.GetString("Wizard_SummaryTitle");
            Description = _languageService.GetString("Wizard_SummaryDescription");
        }
    }
}