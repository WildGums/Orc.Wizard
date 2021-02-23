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
            BreadcrumbMouseDown = new Command<object, object>(BreadcrumbMouseDownExecuteAsync, BreadcrumbMouseDownCanExecute);
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

        #region Commands

        public Command<object, object> BreadcrumbMouseDown { get; set; }

        public bool BreadcrumbMouseDownCanExecute(object parameter)
            => Wizard.AllowQuickNavigation;

        public void BreadcrumbMouseDownExecuteAsync(object parameter)
        {
            var page = parameter as IWizardPage;
            if (page != null && page.IsVisited && Wizard.Pages is System.Collections.Generic.List<IWizardPage>)
            {
                var list = Wizard.Pages as System.Collections.Generic.List<IWizardPage>;
                var idx = list.IndexOf(page);
                Wizard.MoveToPageAsync(idx);
            }
        }
        #endregion
    }
}
