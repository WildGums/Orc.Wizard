namespace Orc.Wizard.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    public partial class IWizardExtensionsFacts
    {
        [TestFixture]
        public class The_FindRequiredPageByType_Method
        {
            [Test]
            public async Task Throws_Exception_When_Page_Not_Found_Async()
            {
                var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

                using var serviceProvider = serviceCollection.BuildServiceProvider();

                var wizard = new TestWizard(serviceProvider);

                Assert.Throws<InvalidOperationException>(() => wizard.FindRequiredPageByType<TestWizardPage>());
            }

            [Test]
            public async Task Returns_Page_Async()
            {
                var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

                using var serviceProvider = serviceCollection.BuildServiceProvider();

                var wizard = new TestWizard(serviceProvider);
                wizard.AddPage<TestWizardPage>(serviceProvider);

                var page = wizard.FindRequiredPageByType<TestWizardPage>();

                Assert.That(page, Is.Not.Null);
            }
        }
    }
}
