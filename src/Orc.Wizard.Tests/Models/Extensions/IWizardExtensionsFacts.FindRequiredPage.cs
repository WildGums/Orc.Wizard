namespace Orc.Wizard.Tests
{
    using System;
    using System.Threading.Tasks;
    using Catel.IoC;
    using NUnit.Framework;

    public partial class IWizardExtensionsFacts
    {
        [TestFixture]
        public class The_FindRequiredPage_Method
        {
            [Test]
            public async Task Throws_Exception_When_Page_Not_Found_Async()
            {
                var wizard = new TestWizard(TypeFactory.Default);

                Assert.Throws<InvalidOperationException>(() => wizard.FindRequiredPage(x => x.Title == "Non-Existing"));
            }

            [Test]
            public async Task Returns_Page_Async()
            {
                var wizard = new TestWizard(TypeFactory.Default);
                wizard.AddPage<TestWizardPage>();

                var page = wizard.FindRequiredPage(x => x is TestWizardPage);

                Assert.That(page, Is.Not.Null);
            }
        }
    }
}
