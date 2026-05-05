namespace Orc.Wizard.Example.Wizard.ViewModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Data;
using Catel.Logging;
using Catel.MVVM;
using Microsoft.Extensions.Logging;

public class PersonWizardPageViewModel : WizardPageViewModelBase<PersonWizardPage>
{
    private readonly ILogger<PersonWizardPageViewModel> _logger;

    public PersonWizardPageViewModel(PersonWizardPage wizardPage, ILogger<PersonWizardPageViewModel> logger, 
        IServiceProvider serviceProvider)
        : base(wizardPage, serviceProvider)
    {
        _logger = logger;
    }

    [ViewModelToModel]
    public string FirstName { get; set; }

    [ViewModelToModel]
    public string LastName { get; set; }

    protected override Task InitializeAsync()
    {
        _logger.LogDebug("Initializing");

        return base.InitializeAsync();
    }

    protected override Task CloseAsync()
    {
        _logger.LogDebug("Closing");

        return base.CloseAsync();
    }

    protected override void ValidateFields(List<IFieldValidationResult> validationResults)
    {
        base.ValidateFields(validationResults);

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            validationResults.Add(FieldValidationResult.CreateError("FirstName", "First name is required"));
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            validationResults.Add(FieldValidationResult.CreateError("LastName", "Last name is required"));
        }
    }

    protected override Task<bool> CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page viewmodel");

        return base.CancelAsync();
    }
}
