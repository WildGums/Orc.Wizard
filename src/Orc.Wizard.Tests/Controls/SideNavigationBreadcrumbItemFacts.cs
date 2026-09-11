namespace Orc.Wizard.Tests;

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Orc.Wizard.Controls;

[TestFixture]
[Apartment(ApartmentState.STA)]
public class SideNavigationBreadcrumbItemFacts
{
    [Test]
    public void Uses_Breadcrumb_Title_And_Description_In_Tooltip()
    {
        var control = new SideNavigationBreadcrumbItem
        {
            Page = new TestWizardPage
            {
                Title = "Full title",
                BreadcrumbTitle = "Breadcrumb title",
                Description = "Page description"
            }
        };

        Assert.That(control.ToolTip, Is.EqualTo($"Breadcrumb title{Environment.NewLine}Page description"));
    }

    [Test]
    public void Trims_Title_Instead_Of_Wrapping()
    {
        var control = new SideNavigationBreadcrumbItem();
        control.ApplyTemplate();

        var titleTextBlock = control.FindName("txtTitle") as TextBlock;

        Assert.That(titleTextBlock, Is.Not.Null);
        Assert.That(titleTextBlock!.TextWrapping, Is.EqualTo(TextWrapping.NoWrap));
        Assert.That(titleTextBlock.TextTrimming, Is.EqualTo(TextTrimming.CharacterEllipsis));
    }
}
