namespace Orc.Wizard.Tests;

using System.Threading.Tasks;
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

    [TestFixture]
    public class The_IsCanceling_Property
    {
        [Test]
        public async Task Is_False_Before_CancelAsync_Is_Called_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var testWizard = new TestWizard(serviceProvider);
            testWizard.AddPage(new TestWizardPage());

            Assert.That(testWizard.IsCanceling, Is.False);
        }

        [Test]
        public async Task Is_True_During_CancelAsync_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var isCancelingDuringCancel = false;
            var trackingPage = new TrackingWizardPage();

            var testWizard = new TestWizard(serviceProvider);
            testWizard.AddPage(trackingPage);

            trackingPage.OnCancelAsyncCallback = () =>
            {
                isCancelingDuringCancel = testWizard.IsCanceling;
            };

            await testWizard.CancelAsync();

            Assert.That(isCancelingDuringCancel, Is.True);
        }

        [Test]
        public async Task Remains_True_After_CancelAsync_Completes_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var testWizard = new TestWizard(serviceProvider);
            testWizard.AddPage(new TestWizardPage());

            await testWizard.CancelAsync();

            Assert.That(testWizard.IsCanceling, Is.True);
        }
    }

    [TestFixture]
    public class TheCancelAsyncMethod
    {
        [Test]
        public async Task Calls_CancelAsync_On_All_Pages_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var page1CancelCalled = false;
            var page2CancelCalled = false;

            var page1 = new TrackingWizardPage();
            var page2 = new TrackingWizardPage();

            page1.OnCancelAsyncCallback = () => page1CancelCalled = true;
            page2.OnCancelAsyncCallback = () => page2CancelCalled = true;

            var testWizard = new TestWizard(serviceProvider);
            testWizard.AddPage(page1);
            testWizard.AddPage(page2);

            await testWizard.CancelAsync();

            Assert.That(page1CancelCalled, Is.True);
            Assert.That(page2CancelCalled, Is.True);
        }

        [Test]
        public async Task Raises_Canceled_Event_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var canceledEventRaised = false;

            var testWizard = new TestWizard(serviceProvider);
            testWizard.AddPage(new TestWizardPage());

            testWizard.Canceled += (sender, e) => canceledEventRaised = true;

            await testWizard.CancelAsync();

            Assert.That(canceledEventRaised, Is.True);
        }

        [Test]
        public async Task Does_Not_Call_CancelAsync_When_CanCancel_Is_False_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var pageCancelCalled = false;

            var page = new TrackingWizardPage();
            page.OnCancelAsyncCallback = () => pageCancelCalled = true;

            var testWizard = new NonCancellableTestWizard(serviceProvider);
            testWizard.AddPage(page);

            await testWizard.CancelAsync();

            Assert.That(pageCancelCalled, Is.False);
            Assert.That(testWizard.IsCanceling, Is.False);
        }
    }
}
