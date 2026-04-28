namespace Orc.Wizard;

using System;
using System.Collections.Generic;
using System.Windows;
using Catel.MVVM;
using Catel.Services;

public class DefaultNavigationController : INavigationController
{
    protected readonly IServiceProvider _serviceProvider;
    protected readonly ILanguageService _languageService;
    protected readonly IMessageService _messageService;

    private readonly List<IWizardNavigationButton> _wizardNavigationButtons = new List<IWizardNavigationButton>();

    public DefaultNavigationController(IWizard wizard, IServiceProvider serviceProvider, 
        ILanguageService languageService, IMessageService messageService)
    {
        Wizard = wizard;
        _serviceProvider = serviceProvider;
        _languageService = languageService;
        _messageService = messageService;
    }

    public IWizard Wizard { get; }

    public IEnumerable<IWizardNavigationButton> GetNavigationButtons()
    {
        if (_wizardNavigationButtons.Count == 0)
        {
            _wizardNavigationButtons.AddRange(CreateNavigationButtons(Wizard));
        }

        return _wizardNavigationButtons;
    }
        
    public void EvaluateNavigationCommands()
    {
        _wizardNavigationButtons.ForEach(x =>
        {
            x.Update();
        });
    }

    protected virtual IEnumerable<IWizardNavigationButton> CreateNavigationButtons(IWizard wizard)
    {
        var buttons = new List<WizardNavigationButton>
        {
            CreateBackButton(wizard),
            CreateForwardButton(wizard),
            CreateFinishButton(wizard),
            CreateCancelButton(wizard)
        };

        return buttons;
    }

    protected virtual WizardNavigationButton CreateBackButton(IWizard wizard)
    {
        var button = new WizardNavigationButton
        {
            Content = _languageService.GetRequiredString("Wizard_Back"),
            IsVisibleEvaluator = () => !wizard.IsFirstPage(),
            Command = new TaskCommand(_serviceProvider, async () =>
                {
                    await wizard.MoveBackAsync();
                },
                () =>
                {
                    if (!wizard.HandleNavigationStates)
                    {
                        return true;
                    }

                    return wizard.CanMoveBack;
                })
        };

        return button;
    }

    protected virtual WizardNavigationButton CreateForwardButton(IWizard wizard)
    {
        var button = new WizardNavigationButton
        {
            Content = _languageService.GetRequiredString("Wizard_Next"),
            IsVisibleEvaluator = () => !wizard.IsLastPage(),
            StyleEvaluator = (x) =>
            {
                var styleName = !wizard.IsLastPage() ? "WizardNavigationPrimaryButtonStyle" : "WizardNavigationButtonStyle";

                var application = System.Windows.Application.Current;
                return application?.TryFindResource(styleName) as Style;
            },
            Command = new TaskCommand(_serviceProvider, async () =>
                {
                    await wizard.MoveForwardAsync();
                },
                () =>
                {
                    if (!wizard.HandleNavigationStates)
                    {
                        return true;
                    }

                    return wizard.CanMoveForward;
                })
        };

        return button;
    }

    protected virtual WizardNavigationButton CreateFinishButton(IWizard wizard)
    {
        var button = new WizardNavigationButton
        {
            Content = _languageService.GetRequiredString("Wizard_Finish"),
            IsVisibleEvaluator = () => wizard.IsLastPage(),
            StyleEvaluator = (x) =>
            {
                var styleName = wizard.IsLastPage() ? "WizardNavigationPrimaryButtonStyle" : "WizardNavigationButtonStyle";

                var application = System.Windows.Application.Current;
                return application?.TryFindResource(styleName) as Style;
            },
            Command = new TaskCommand(_serviceProvider, async () =>
                {
                    await wizard.ResumeAsync();
                },
                () =>
                {
                    if (!wizard.HandleNavigationStates)
                    {
                        return true;
                    }

                    if (!Wizard.CanResume)
                    {
                        return false;
                    }

                    // Don't validate
                    var validationSummary = wizard.GetValidationContextForCurrentPage(false);
                    if (!validationSummary.HasErrors)
                    {
                        return true;
                    }

                    return false;
                })
        };

        return button;
    }

    protected virtual WizardNavigationButton CreateCancelButton(IWizard wizard)
    {
        var button = new WizardNavigationButton
        {
            Content = _languageService.GetRequiredString("Wizard_Cancel"),
            IsVisible = true,
            Command = new TaskCommand(_serviceProvider, async () =>
                {
                    if (await _messageService.ShowAsync(_languageService.GetRequiredString("Wizard_AreYouSureYouWantToCancelWizard"), button: MessageButton.YesNo) == MessageResult.No)
                    {
                        return;
                    }

                    await Wizard.CancelAsync();
                },
                () =>
                {
                    if (!wizard.HandleNavigationStates)
                    {
                        return true;
                    }

                    return wizard.CanCancel;
                })
        };

        return button;
    }
}
