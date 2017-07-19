// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gadget.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Models
{
    using Catel.Data;

    public class Gadget : ModelBase
    {
        public bool IsSelected { get; set; }

        public string Name { get; set; }
    }
}