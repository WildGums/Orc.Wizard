namespace Orc.Wizard.Automation
{
    using System.Windows.Automation;
    using Orc.Automation.Controls;

    public class BreadcrumbItem : FrameworkElement<BreadcrumbItemModel, BreadcrumbItemMap>
    {
        public BreadcrumbItem(AutomationElement element) 
            : base(element)
        {
        }

        public string? EllipseText => Map.EllipseText?.Value;
        public string? Title => Map.TitleText?.Value;
    }
}
