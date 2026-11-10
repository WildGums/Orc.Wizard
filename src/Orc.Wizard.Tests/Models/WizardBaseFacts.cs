namespace Orc.Wizard.Tests;

using System.Threading.Tasks;
using Catel.MVVM;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

public class WizardBaseFacts
{
    [TestFixture]
    public class The_CanMoveForward_Property
    {
        [TestCase(false)]
        [TestCase(true)]
        public async Task Allows_Validation_Errors_Only_While_Results_Are_Hidden_Async(bool exposeValidationResults)
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var wizard = new TestWizard(serviceProvider);
            var page = new TestWizardPage();
            IWizardPageViewModel viewModel = new InvalidWizardPageViewModel(page, serviceProvider);
            page.ViewModel = viewModel;
            wizard.AddPage(page);
            wizard.AddPage(new TestWizardPage());
            Assert.That(wizard.CurrentPage, Is.SameAs(page));
            await viewModel.InitializeViewModelAsync();

            if (exposeValidationResults)
            {
                viewModel.EnableValidationExposure();
            }

            var canMoveForward = wizard.CanMoveForward;

            Assert.That(viewModel.ValidationContext.HasErrors, Is.True);
            Assert.That(viewModel.IsHidingValidationResults, Is.EqualTo(!exposeValidationResults));
            Assert.That(canMoveForward, Is.EqualTo(!exposeValidationResults));
        }
    }

    [TestFixture]
    public class The_CanResume_Property
    {
        [TestCase(false)]
        [TestCase(true)]
        public async Task Allows_Validation_Errors_Only_While_Results_Are_Hidden_Async(bool exposeValidationResults)
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var wizard = new TestWizard(serviceProvider);
            var page = new TestWizardPage();
            IWizardPageViewModel viewModel = new InvalidWizardPageViewModel(page, serviceProvider);
            page.ViewModel = viewModel;
            wizard.AddPage(page);
            wizard.AddPage(ActivatorUtilities.CreateInstance<SummaryWizardPage>(serviceProvider));
            Assert.That(wizard.CurrentPage, Is.SameAs(page));
            await viewModel.InitializeViewModelAsync();

            if (exposeValidationResults)
            {
                viewModel.EnableValidationExposure();
            }

            var canResume = wizard.CanResume;

            Assert.That(viewModel.ValidationContext.HasErrors, Is.True);
            Assert.That(viewModel.IsHidingValidationResults, Is.EqualTo(!exposeValidationResults));
            Assert.That(canResume, Is.EqualTo(!exposeValidationResults));
        }
    }

    [TestFixture]
    public class The_MoveForwardAsync_Method
    {
        [Test]
        public async Task Exposes_Hidden_Validation_Errors_Without_Leaving_Current_Page_Async()
        {
            var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var wizard = new TestWizard(serviceProvider);
            var page = new TestWizardPage();
            IWizardPageViewModel viewModel = new InvalidWizardPageViewModel(page, serviceProvider);
            page.ViewModel = viewModel;
            wizard.AddPage(page);
            wizard.AddPage(new TestWizardPage());
            Assert.That(wizard.CurrentPage, Is.SameAs(page));
            await viewModel.InitializeViewModelAsync();
            Assert.That(wizard.CanMoveForward, Is.True);
            Assert.That(viewModel.ValidationContext.HasErrors, Is.True);
            Assert.That(viewModel.IsHidingValidationResults, Is.True);

            await wizard.MoveForwardAsync();

            Assert.That(wizard.CurrentPage, Is.SameAs(page));
            Assert.That(viewModel.ValidationContext.HasErrors, Is.True);
            Assert.That(viewModel.IsHidingValidationResults, Is.False);
            Assert.That(wizard.CanMoveForward, Is.False);
        }
    }

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

            var result = await testWizard.CancelAsync();

            Assert.That(result, Is.True);
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
        }
    }
}
