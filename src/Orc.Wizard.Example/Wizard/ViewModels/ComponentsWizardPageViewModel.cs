namespace Orc.Wizard.Example.Wizard.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Component = Example.Component;

public class ComponentsWizardPageViewModel : WizardPageViewModelBase<ComponentsWizardPage>
{
    private readonly ILanguageService _languageService;

    public ComponentsWizardPageViewModel(ComponentsWizardPage wizardPage, IServiceProvider serviceProvider)
        : this(wizardPage, serviceProvider, serviceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public ComponentsWizardPageViewModel(ComponentsWizardPage wizardPage, IServiceProvider serviceProvider, ILanguageService languageService)
        : base(wizardPage, serviceProvider)
    {
        _languageService = languageService;
        SelectAll = new Command(serviceProvider, OnSelectAllExecute);
        MoveBackViaCode = new TaskCommand(serviceProvider, OnMoveBackViaCodeExecuteAsync);
        MoveForwardViaCode = new TaskCommand(serviceProvider, OnMoveForwardViaCodeExecuteAsync);
        CancelViaCode = new TaskCommand(serviceProvider, OnCancelViaCodeExecuteAsync);
        ResumeViaCode = new TaskCommand(serviceProvider, OnResumeViaCodeExecuteAsync);
    }

    [ViewModelToModel]
    public System.Collections.ObjectModel.ObservableCollection<Component> Components { get; private set; }

    #region Commands
    public Command SelectAll { get; private set; }

    private void OnSelectAllExecute()
    {
        Components.ForEach(x => x.IsSelected = true);
    }

    public TaskCommand MoveBackViaCode { get; private set; }

    private async Task OnMoveBackViaCodeExecuteAsync()
    {
        await Wizard.MoveBackAsync();
    }

    public TaskCommand MoveForwardViaCode { get; private set; }

    private async Task OnMoveForwardViaCodeExecuteAsync()
    {
        await Wizard.MoveForwardAsync();
    }

    public TaskCommand CancelViaCode { get; private set; }

    private async Task OnCancelViaCodeExecuteAsync()
    {
        await Wizard.CancelAsync();
    }

    public TaskCommand ResumeViaCode { get; private set; }

    private async Task OnResumeViaCodeExecuteAsync()
    {
        await Wizard.MoveForwardOrResumeAsync();
    }
    #endregion

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        Components.ForEach(x => x.PropertyChanged += OnComponentPropertyChanged);
    }

    protected override Task<bool> SaveAsync()
    {
        return base.SaveAsync();
    }

    protected override async Task CloseAsync()
    {
        Components.ForEach(x => x.PropertyChanged -= OnComponentPropertyChanged);

        await base.CloseAsync();
    }

    private void OnComponentPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Validate(true);
    }

    protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
    {
        base.ValidateBusinessRules(validationResults);

        var components = Components;
        if (components is not null)
        {
            if (!components.Any(x => x.IsSelected))
            {
                validationResults.Add(BusinessRuleValidationResult.CreateError(
                    _languageService.GetRequiredString("Orc_Wizard_Example_ComponentsWizardPageViewModel_SelectAtLeast1Component")));
            }
        }
    }
}
