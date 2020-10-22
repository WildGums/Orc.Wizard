namespace Orc.Wizard
{
    using Catel;
    using Catel.MVVM;

    public class WizardPageViewModelBase<TWizardPage> : ViewModelBase, IWizardPageViewModel
        where TWizardPage : class, IWizardPage
    {
        #region Constructors
        public WizardPageViewModelBase(TWizardPage wizardPage)
        {
            Argument.IsNotNull(() => wizardPage);

            DeferValidationUntilFirstSaveCall = true;

            WizardPage = wizardPage;
        }
        #endregion

        #region Properties

        [Model(SupportIEditableObject = false)]
        public TWizardPage WizardPage { get; private set; }

        public IWizard Wizard
        {
            get
            {
                var wizardPage = WizardPage;
                if (wizardPage is null)
                {
                    return null;
                }

                return wizardPage.Wizard;
            }
        }

        public virtual void EnableValidationExposure()
        {
            DeferValidationUntilFirstSaveCall = false;
            Validate(true);
        }
        #endregion
    }
}
