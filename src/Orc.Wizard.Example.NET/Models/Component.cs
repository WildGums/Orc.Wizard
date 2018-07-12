﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Component.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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