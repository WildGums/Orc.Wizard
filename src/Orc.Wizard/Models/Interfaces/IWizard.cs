﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizard.cs" company="WildGums">
//   Copyright (c) 2013 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWizard
    {
        #region Properties
        IWizardPage CurrentPage { get; }
        IEnumerable<IWizardPage> Pages { get; }
        INavigationStrategy NavigationStrategy { get; }
        string Title { get; }
        System.Windows.ResizeMode ResizeMode { get; }
        System.Windows.Size MinSize { get; }
        System.Windows.Size MaxSize { get; }
        bool CanResume { get; }
        bool CanCancel { get; }
        bool CanMoveForward { get; }
        bool CanMoveBack { get; }
        bool IsHelpVisible { get; }
        bool CanShowHelp { get; }
        bool ShowInTaskbar { get; }
        #endregion

        Task SaveAsync();
        Task CancelAsync();
        Task MoveForwardAsync();
        Task MoveBackAsync();
        Task ShowHelpAsync();

        event EventHandler<EventArgs> MovedForward;
        event EventHandler<EventArgs> MovedBack;
        event EventHandler<EventArgs> HelpShown;

        void InsertPage(int index, IWizardPage page);
        void RemovePage(IWizardPage page);
    }
}