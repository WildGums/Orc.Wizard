namespace Orc.Wizard.Tests;

using System.Threading.Tasks;
using Catel.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

public class WizardBaseFacts
{
    [TestFixture]
    public class TheAddPageMethod
    {
        [Test]
        public async Task Raises_PageAdded_Event_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var executedEvent = false;

            var testWizard = new TestWizard(serviceProvider);
            var wizardPage = new TestWizardPage();

            testWizard.PageAdded += (sender, e) =>
            {
                Assert.That(ReferenceEquals(wizardPage, e.WizardPage), Is.True);

                executedEvent = true;
            };

            testWizard.AddPage(wizardPage);

            Assert.That(executedEvent, Is.True);
        }
    }

    [TestFixture]
    public class TheRemovePageMethod
    {
        [Test]
        public async Task Raises_PageRemoved_Event_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var executedEvent = false;

            var testWizard = new TestWizard(serviceProvider);
            var wizardPage1 = new TestWizardPage();
            var wizardPage2 = new TestWizardPage();

            testWizard.AddPage(wizardPage1);
            testWizard.AddPage(wizardPage2);

            testWizard.PageRemoved += (sender, e) =>
            {
                Assert.That(ReferenceEquals(wizardPage2, e.WizardPage), Is.True);

                executedEvent = true;
            };

            testWizard.RemovePage(wizardPage2);

            Assert.That(executedEvent, Is.True);
        }
    }
}
