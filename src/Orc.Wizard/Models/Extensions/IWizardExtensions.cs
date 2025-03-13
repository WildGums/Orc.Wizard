namespace Orc.Wizard;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catel.IoC;
using Catel.Logging;
using Catel.Reflection;

public static class IWizardExtensions
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public static async Task MoveForwardOrResumeAsync(this IWizard wizard)
    {
        ArgumentNullException.ThrowIfNull(wizard);

        if (wizard.CanMoveForward)
        {
            Log.Debug("Moving forward from MoveNextOrResumeAsync()");

            await wizard.MoveForwardAsync();
            return;
        }

        if (wizard.CanResume)
        {
            Log.Debug("Resuming from MoveNextOrResumeAsync()");

            await wizard.ResumeAsync();
            return;
        }

        Log.Debug("Could not move forward or resume from MoveNextOrResumeAsync()");
    }

    public static Task MoveToPageAsync(this IWizard wizard, IWizardPage wizardPage)
    {
        var index = wizard.Pages.ToList().IndexOf(wizardPage);
        if (index < 0)
        {
            return Task.CompletedTask;
        }

        return wizard.MoveToPageAsync(index);
    }

    public static IWizardPage AddPage(this IWizard wizard, IWizardPage page)
    {
        ArgumentNullException.ThrowIfNull(wizard);
        ArgumentNullException.ThrowIfNull(page);

        wizard.InsertPage(wizard.Pages.Count(), page);

        return page;
    }

    public static TWizardPage AddPage<TWizardPage>(this IWizard wizard)
        where TWizardPage : IWizardPage
    {
        ArgumentNullException.ThrowIfNull(wizard);

        return wizard.InsertPage<TWizardPage>(wizard.Pages.Count());
    }

    public static TWizardPage InsertPage<TWizardPage>(this IWizard wizard, int index)
        where TWizardPage : IWizardPage
    {
        ArgumentNullException.ThrowIfNull(wizard);

#pragma warning disable IDISP001 // Dispose created
        var typeFactory = wizard.GetTypeFactory();
#pragma warning restore IDISP001 // Dispose created
        var page = typeFactory.CreateRequiredInstance<TWizardPage>();

        wizard.InsertPage(index, page);

        return page;
    }

    public static TWizardPage AddPage<TWizardPage>(this IWizard wizard, object model)
        where TWizardPage : IWizardPage
    {
        ArgumentNullException.ThrowIfNull(wizard);

        return wizard.InsertPage<TWizardPage>(wizard.Pages.Count(), model);
    }

    public static TWizardPage InsertPage<TWizardPage>(this IWizard wizard, int index, object model)
        where TWizardPage : IWizardPage
    {
        ArgumentNullException.ThrowIfNull(wizard);

#pragma warning disable IDISP001 // Dispose created
        var typeFactory = wizard.GetTypeFactory();
#pragma warning restore IDISP001 // Dispose created
        var page = typeFactory.CreateRequiredInstanceWithParametersAndAutoCompletion<TWizardPage>(model);

        wizard.InsertPage(index, page);

        return page;
    }

    public static TWizardPage? FindPageByType<TWizardPage>(this IWizard wizard)
        where TWizardPage : IWizardPage
    {
        return (TWizardPage?)FindPage(wizard, x => typeof(TWizardPage).IsAssignableFromEx(x.GetType()));
    }

    public static TWizardPage FindRequiredPageByType<TWizardPage>(this IWizard wizard)
        where TWizardPage : IWizardPage
    {
        var page = wizard.FindPageByType<TWizardPage>();
        if (page is null)
        {
            throw Log.ErrorAndCreateException<InvalidOperationException>($"Could not find required page of type '{typeof(TWizardPage).Name}'");
        }

        return page;
    }

    public static IWizardPage? FindPage(this IWizard wizard, Func<IWizardPage, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(wizard);
        ArgumentNullException.ThrowIfNull(predicate);

        var allPages = wizard.Pages.ToList();
        if (allPages.Count == 0)
        {
            return null;
        }

        return allPages.FirstOrDefault(predicate);
    }

    public static IWizardPage FindRequiredPage(this IWizard wizard, Func<IWizardPage, bool> predicate)
    {
        var page = wizard.FindPage(predicate);
        if (page is null)
        {
            throw Log.ErrorAndCreateException<InvalidOperationException>($"Could not find required page using the specified predicate");
        }

        return page;
    }

    public static bool IsFirstPage(this IWizard wizard, IWizardPage? wizardPage = null)
    {
        return IsPage(wizard, wizardPage, x => x.First());
    }

    public static bool IsLastPage(this IWizard wizard, IWizardPage? wizardPage = null)
    {
        return IsPage(wizard, wizardPage, x => x.Last());
    }

    private static bool IsPage(this IWizard wizard, IWizardPage? wizardPage, Func<List<IWizardPage>, IWizardPage> selector)
    {
        ArgumentNullException.ThrowIfNull(wizard);

        if (wizardPage is null)
        {
            wizardPage = wizard.CurrentPage;
        }

        var allPages = wizard.Pages.ToList();
        if (allPages.Count == 0)
        {
            return false;
        }

        var isLastPage = ReferenceEquals(selector(allPages), wizardPage);
        return isLastPage;
    }
}
