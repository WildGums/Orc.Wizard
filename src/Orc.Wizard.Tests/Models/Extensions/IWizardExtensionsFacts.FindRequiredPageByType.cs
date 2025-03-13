namespace Orc.Wizard.Tests
{
    using System;
    using System.Threading.Tasks;
    using Catel.IoC;
    using NUnit.Framework;

    public partial class IWizardExtensionsFacts
    {
        [TestFixture]
        public class The_FindRequiredPageByType_Method
        {
            [Test]
            public async Task Throws_Exception_When_Page_Not_Found_Async()
            {
                var wizard = new TestWizard(TypeFactory.Default);

                Assert.Throws<InvalidOperationException>(() => wizard.FindRequiredPageByType<TestWizardPage>());
            }

            [Test]
            public async Task Returns_Page_Async()
            {
                var wizard = new TestWizard(TypeFactory.Default);
                wizard.AddPage<TestWizardPage>();

                var page = wizard.FindRequiredPageByType<TestWizardPage>();

                Assert.That(page, Is.Not.Null);
            }
        }
    }
}
