namespace Orc.Wizard;

using System.Threading.Tasks;
using Catel.Services;

public interface IWizardService
{
    Task<UIVisualizerResult> ShowWizardAsync(IWizard wizard);
}