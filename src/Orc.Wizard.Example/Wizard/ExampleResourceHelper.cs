namespace Orc.Wizard.Example.Wizard;

using System;
using System.Globalization;
using System.Resources;

internal static class ExampleResourceHelper
{
    private static readonly ResourceManager ResourceManager = new("Orc.Wizard.Example.Properties.Resources", typeof(ExampleResourceHelper).Assembly);

    public static string GetRequiredString(string resourceName)
    {
        var value = ResourceManager.GetString(resourceName, CultureInfo.CurrentUICulture);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Resource '{resourceName}' could not be resolved.");
        }

        return value;
    }
}
