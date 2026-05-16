namespace Orc.Wizard.Tests;

using System.Linq;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Orc.Wizard.Example.Wizard;

public class ExampleLocalizationFacts
{
    [TestFixture]
    public class The_PersonWizardPage_Constructor
    {
        [Test]
        public void Uses_Localized_Title_Description_And_Summary_Title()
        {
            using var serviceProvider = CreateServiceProvider();
            using var loggerFactory = LoggerFactory.Create(x => { });
            var languageService = serviceProvider.GetRequiredService<ILanguageService>();
            var logger = loggerFactory.CreateLogger<PersonWizardPage>();

            var page = new PersonWizardPage(logger, languageService)
            {
                FirstName = "Jane",
                LastName = "Doe"
            };

            var summary = page.GetSummary();

            Assert.That(page.Title, Is.EqualTo("Person"));
            Assert.That(page.Description, Is.EqualTo("Enter the details of the person"));
            Assert.That(summary.Title, Is.EqualTo("Person"));
            Assert.That(summary.Summary, Is.EqualTo("Jane Doe"));
        }
    }

    [TestFixture]
    public class The_AgeWizardPage_Constructor
    {
        [Test]
        public void Uses_Localized_Title_Description_And_Summary_Title()
        {
            using var serviceProvider = CreateServiceProvider();
            using var loggerFactory = LoggerFactory.Create(x => { });
            var languageService = serviceProvider.GetRequiredService<ILanguageService>();
            var logger = loggerFactory.CreateLogger<AgeWizardPage>();

            var page = new AgeWizardPage(logger, languageService)
            {
                Age = "42"
            };

            var summary = page.GetSummary();

            Assert.That(page.Title, Is.EqualTo("Age"));
            Assert.That(page.Description, Is.EqualTo("Specify the age of the person"));
            Assert.That(summary.Title, Is.EqualTo("Age"));
            Assert.That(summary.Summary, Is.EqualTo("42"));
        }
    }

    [TestFixture]
    public class The_ComponentsWizardPage_Constructor
    {
        [Test]
        public void Uses_Localized_Title_Description_And_Summary_Title()
        {
            using var serviceProvider = CreateServiceProvider();
            var languageService = serviceProvider.GetRequiredService<ILanguageService>();

            var page = new ComponentsWizardPage(languageService);

            page.Components.First().IsSelected = true;

            var summary = page.GetSummary();

            Assert.That(page.Title, Is.EqualTo("Components selection"));
            Assert.That(page.Description, Is.EqualTo("Select the components"));
            Assert.That(summary.Title, Is.EqualTo("Components"));
            Assert.That(summary.Summary, Does.Contain("Orc.Analytics"));
        }
    }

    private static ServiceProvider CreateServiceProvider()
    {
        var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();
        serviceCollection.AddSingleton<ILanguageSource>(
            new LanguageResourceSource("Orc.Wizard.Example", "Orc.Wizard.Example.Properties", "Resources"));

        return serviceCollection.BuildServiceProvider();
    }
}
