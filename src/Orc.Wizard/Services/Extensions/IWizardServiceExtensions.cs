namespace Orc.Wizard;

using System;
using System.Threading.Tasks;
using Catel;
using Catel.IoC;
using Catel.Logging;
using Catel.Reflection;
using Catel.Services;

public static class IWizardServiceExtensions
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public static Task<UIVisualizerResult> ShowWizardAsync<TWizard>(this IWizardService wizardService, object? model = null)
        where TWizard : IWizard
    {
        ArgumentNullException.ThrowIfNull(wizardService);

#pragma warning disable IDISP001 // Dispose created
        var typeFactory = wizardService.GetTypeFactory();
#pragma warning restore IDISP001 // Dispose created

        IWizard wizard;

        if (model is not null)
        {
            Log.Debug("Creating wizard '{0}' with model '{1}'", typeof(TWizard).GetSafeFullName(), ObjectToStringHelper.ToFullTypeString(model));

            wizard = typeFactory.CreateRequiredInstanceWithParametersAndAutoCompletion<TWizard>(model);
        }
        else
        {
            Log.Debug("Creating wizard '{0}'", typeof(TWizard).GetSafeFullName());

            wizard = typeFactory.CreateRequiredInstance<TWizard>();
        }

        return wizardService.ShowWizardAsync(wizard);
    }
}