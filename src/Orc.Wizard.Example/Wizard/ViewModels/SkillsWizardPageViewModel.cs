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

public class SkillsWizardPageViewModel : WizardPageViewModelBase<SkillsWizardPage>
{
    private readonly ILanguageService _languageService;

    public SkillsWizardPageViewModel(SkillsWizardPage wizardPage, IServiceProvider serviceProvider)
        : this(wizardPage, serviceProvider, serviceProvider.GetRequiredService<ILanguageService>())
    {
    }

    public SkillsWizardPageViewModel(SkillsWizardPage wizardPage, IServiceProvider serviceProvider, ILanguageService languageService)
        : base(wizardPage, serviceProvider)
    {
        _languageService = languageService;
        SelectAll = new Command(serviceProvider, OnSelectAllExecute);
    }

    [ViewModelToModel]
    public System.Collections.ObjectModel.ObservableCollection<Skill> Skills { get; private set; }

    #region Commands
    public Command SelectAll { get; private set; }

    private void OnSelectAllExecute()
    {
        Skills.ForEach(x => x.IsSelected = true);
    }
    #endregion

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        Skills.ForEach(x => x.PropertyChanged += OnSkillPropertyChanged);
    }

    protected override async Task CloseAsync()
    {
        Skills.ForEach(x => x.PropertyChanged -= OnSkillPropertyChanged);

        await base.CloseAsync();
    }

    private void OnSkillPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Validate(true);
    }

    protected override void ValidateBusinessRules(List<IBusinessRuleValidationResult> validationResults)
    {
        base.ValidateBusinessRules(validationResults);

        var skills = Skills;
        if (skills is not null)
        {
            if (!skills.Any(x => x.IsSelected))
            {
                validationResults.Add(BusinessRuleValidationResult.CreateError(
                    _languageService.GetRequiredString("Orc_Wizard_Example_SkillsWizardPageViewModel_SelectAtLeast1Skill")));
            }
        }
    }
}
