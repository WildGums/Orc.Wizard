namespace Orc.Wizard;

using System.Collections.Generic;

public interface INavigationController
{
    IReadOnlyList<IWizardNavigationButton> GetNavigationButtons();
    void EvaluateNavigationCommands();
}
