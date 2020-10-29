namespace Orc.Wizard
{
    using System.Collections.Generic;
    using Catel.Services;

    public class FastForwardNavigationController : DefaultNavigationController
    {
        public FastForwardNavigationController(IWizard wizard, ILanguageService languageService)
            : base(wizard, languageService)
        {

        }

        protected override IEnumerable<IWizardNavigationButton> CreateNavigationButtons(IWizard wizard)
        {
            var buttons = new List<WizardNavigationButton>();

            buttons.Add(CreateBackButton(wizard));
            buttons.Add(CreateForwardButton(wizard));

            // Special case: finish button is always visible
            var finishButton = CreateFinishButton(wizard);
            finishButton.IsVisibleEvaluator = () => true;
            buttons.Add(finishButton);
            
            buttons.Add(CreateCancelButton(wizard));

            return buttons;
        }
    }
}
