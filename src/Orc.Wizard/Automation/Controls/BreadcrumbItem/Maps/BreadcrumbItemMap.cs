namespace Orc.Wizard.Automation;

using System.Windows.Automation;
using Orc.Automation;
using Orc.Automation.Controls;

public class BreadcrumbItemMap : AutomationBase
{
    public BreadcrumbItemMap(AutomationElement element) 
        : base(element)
    {
    }

    public Text? EllipseText => By.Id("ellipseText").One<Text>();
    public Text? TitleText => By.Id("txtTitle").One<Text>();
}
