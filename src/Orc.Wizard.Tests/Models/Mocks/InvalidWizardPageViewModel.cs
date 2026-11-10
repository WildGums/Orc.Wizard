namespace Orc.Wizard.Tests;

using System;
using System.Collections.Generic;
using Catel.Data;

internal class InvalidWizardPageViewModel : WizardPageViewModelBase<TestWizardPage>
{
    public InvalidWizardPageViewModel(TestWizardPage wizardPage, IServiceProvider serviceProvider)
        : base(wizardPage, serviceProvider)
    {
    }

    protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
    {
        base.ValidateBusinessRules(validationResults);

        validationResults.Add(BusinessRuleValidationResult.CreateError("The wizard page is invalid."));
    }
}