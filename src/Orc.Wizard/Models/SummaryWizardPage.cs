namespace Orc.Wizard
{
    using System;
    using Catel;
    using Catel.Services;

    public class SummaryWizardPage : WizardPageBase
    {
        public SummaryWizardPage(ILanguageService languageService)
        {
            ArgumentNullException.ThrowIfNull(languageService);

            Title = languageService.GetRequiredString("Wizard_SummaryTitle");
            Description = languageService.GetRequiredString("Wizard_SummaryDescription");
        }
    }
}
