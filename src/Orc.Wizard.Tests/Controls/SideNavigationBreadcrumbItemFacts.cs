namespace Orc.Wizard.Tests;

using System;
using System.Linq;
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
    public void Trims_Title_Instead_Of_Wrapping()
    {
        var control = CreateControlInHost(new SideNavigationBreadcrumbItem());

        var titleTextBlock = control.FindName("txtTitle") as TextBlock;

        Assert.That(titleTextBlock, Is.Not.Null);
        Assert.That(titleTextBlock!.TextWrapping, Is.EqualTo(TextWrapping.NoWrap));
        Assert.That(titleTextBlock.TextTrimming, Is.EqualTo(TextTrimming.CharacterEllipsis));
    }

    private static SideNavigationBreadcrumbItem CreateControlInHost(SideNavigationBreadcrumbItem control)
    {
        var host = new ContentControl
        {
            Content = control
        };

        host.ApplyTemplate();
        host.Measure(new Size(400, 300));
        host.Arrange(new Rect(0, 0, 400, 300));
        host.UpdateLayout();

        control.ApplyTemplate();
        control.Measure(new Size(400, 300));
        control.Arrange(new Rect(0, 0, 400, 300));
        control.UpdateLayout();

        return control;
    }
}
