// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Component.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Models
{
    using Catel.Data;

    public class Component : ModelBase
    {
        public bool IsSelected { get; set; }

        public string Name { get; set; }
    }
}