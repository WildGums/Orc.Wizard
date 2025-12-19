namespace Orc.Wizard.Example.Wizard.ViewModels;

using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Data;
using Catel.Logging;
using Catel.MVVM;

public class PersonWizardPageViewModel : WizardPageViewModelBase<PersonWizardPage>
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public PersonWizardPageViewModel(PersonWizardPage wizardPage)
        : base(wizardPage)
    {
    }

    [ViewModelToModel]
    public string FirstName { get; set; }

    [ViewModelToModel]
    public string LastName { get; set; }

    protected override Task InitializeAsync()
    {
        Log.Debug("Initializing");

        return base.InitializeAsync();
    }

    protected override Task CloseAsync()
    {
        Log.Debug("Closing");

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
}
