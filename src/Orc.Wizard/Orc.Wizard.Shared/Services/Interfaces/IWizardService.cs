// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System.Threading.Tasks;

    public interface IWizardService
    {
        Task<bool?> ShowWizardAsync(IWizard wizard);
    }
}