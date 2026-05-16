namespace Orc.Wizard.Example.Wizard.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.MVVM;
using Catel.Services;
using Microsoft.Extensions.Logging;

public class AgeWizardPageViewModel : WizardPageViewModelBase<AgeWizardPage>
{
    private readonly ILogger<AgeWizardPageViewModel> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageService _messageService;
    private readonly ILanguageService _languageService;

    public AgeWizardPageViewModel(AgeWizardPage wizardPage, ILogger<AgeWizardPageViewModel> logger,
        IServiceProvider serviceProvider, IMessageService messageService, ILanguageService languageService)
        : base(wizardPage, serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _messageService = messageService;
        _languageService = languageService;

        AddPage = new TaskCommand(serviceProvider, OnAddPageExecuteAsync);
    }

    [ViewModelToModel]
    public string Age { get; set; }

    public TaskCommand AddPage { get; private set; }

    private async Task OnAddPageExecuteAsync()
    {
        if ((await _messageService.ShowAsync(
                _languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPageViewModel_AddPageQuestion"),
                _languageService.GetRequiredString("Orc_Wizard_Example_AgeWizardPageViewModel_AddPageTitle"),
                MessageButton.YesNo, MessageImage.Question)) != MessageResult.Yes)
        {
            return;
        }

        Wizard.InsertPage<AgeWizardPage>(_serviceProvider, WizardPage.Number);
    }

    protected override Task<bool> CancelAsync()
    {
        _logger.LogInformation("Canceling wizard page viewmodel");

        return base.CancelAsync();
    }
}
