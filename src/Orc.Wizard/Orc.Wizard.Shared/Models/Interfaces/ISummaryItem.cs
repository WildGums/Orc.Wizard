// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISummaryItem.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    public interface ISummaryItem
    {
        string Title { get; set; }
        string Summary { get; set; }
    }
}