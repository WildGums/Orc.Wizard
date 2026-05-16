namespace Orc.Wizard.Example.Wizard.ViewModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Data;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;
using Microsoft.Extensions.Logging;

public class PersonWizardPageViewModel : WizardPageViewModelBase<PersonWizardPage>
{
    private readonly ILogger<PersonWizardPageViewModel> _logger;
    private readonly ILanguageService _languageService;

    public PersonWizardPageViewModel(PersonWizardPage wizardPage, ILogger<PersonWizardPageViewModel> logger,
        IServiceProvider serviceProvider, ILanguageService languageService)
        : base(wizardPage, serviceProvider)
    {
        _logger = logger;
        _languageService = languageService;
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
            validationResults.Add(FieldValidationResult.CreateError("FirstName",
                _languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPageViewModel_FirstNameRequired")));
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            validationResults.Add(FieldValidationResult.CreateError("LastName",
                _languageService.GetRequiredString("Orc_Wizard_Example_PersonWizardPageViewModel_LastNameRequired")));
        }
    }

    protected override Task<bool> CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page viewmodel");

        return base.CancelAsync();
    }
}
