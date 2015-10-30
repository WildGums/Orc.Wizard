// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizard.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
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
        string Title { get; }
        bool CanResume { get; }
        bool CanCancel { get; }
        bool CanMoveForward { get; }
        bool CanMoveBack { get; }
        #endregion

        Task ResumeAsync();
        Task CancelAsync();
        Task MoveForwardAsync();
        Task MoveBackAsync();

        event EventHandler MovedForward;
        event EventHandler MovedBack;
    }
}