// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleWizard.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard
{
    public interface IExampleWizard : IWizard
    {
        bool AllowQuickNavigationWrapper { get; set; }
        bool HandleNavigationStatesWrapper { get; set; }
        INavigationController NavigationControllerWrapper { get; set; }
        bool ShowHelpWrapper { get; set; }
        bool ShowInTaskbarWrapper { get; set; }
        bool CacheViewsWrapper { get; set; }
    }
}
