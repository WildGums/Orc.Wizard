// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
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

            Title = _languageService.GetString("SummaryTitle");
            Description = _languageService.GetString("SummaryDescription");
        }
    }
}