namespace Orc.Wizard;

using System;
using System.Threading.Tasks;
using Catel;
using Catel.IoC;
using Catel.Logging;
using Catel.Reflection;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class IWizardServiceExtensions
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(IWizardServiceExtensions));

    public static Task<UIVisualizerResult> ShowWizardAsync<TWizard>(this IWizardService wizardService, object? model = null)
        where TWizard : IWizard
    {
        ArgumentNullException.ThrowIfNull(wizardService);

        IWizard wizard;

        if (model is not null)
        {
            Logger.LogDebug("Creating wizard '{0}' with model '{1}'", typeof(TWizard).GetSafeFullName(), ObjectToStringHelper.ToFullTypeString(model));

            wizard = (TWizard)ActivatorUtilities.CreateInstance(IoCContainer.ServiceProvider, typeof(TWizard), model);
        }
        else
        {
            Logger.LogDebug("Creating wizard '{0}'", typeof(TWizard).GetSafeFullName());

            wizard = (TWizard)ActivatorUtilities.CreateInstance(IoCContainer.ServiceProvider, typeof(TWizard));
        }

        return wizardService.ShowWizardAsync(wizard);
    }
}
