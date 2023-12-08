namespace Orc.Wizard.Tests;

using System.Threading.Tasks;
using Catel.IoC;
using NUnit.Framework;

public class WizardBaseFacts
{
    [TestFixture]
    public class TheAddPageMethod
    {
        [Test]
        public async Task Raises_PageAdded_Event_Async()
        {
#pragma warning disable IDISP001 // Dispose created
            var serviceLocator = new ServiceLocator(ServiceLocator.Default);
#pragma warning restore IDISP001 // Dispose created

            var executedEvent = false;

            var testWizard = new TestWizard(serviceLocator.GetTypeFactory());
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
#pragma warning disable IDISP001 // Dispose created
            var serviceLocator = new ServiceLocator(ServiceLocator.Default);
#pragma warning restore IDISP001 // Dispose created

            var executedEvent = false;

            var testWizard = new TestWizard(serviceLocator.GetTypeFactory());
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
