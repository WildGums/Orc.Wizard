// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Windows.Media;

    public static class WizardConfiguration
    {
        public static TimeSpan AnimationDuration = TimeSpan.FromMilliseconds(300);
        public static readonly int CannotNavigate = -1;
    }

    public static class DefaultColorNames
    {
        public const string AccentColorBrush = "AccentColorBrush";
        public const string AccentColorBrush4 = "AccentColorBrush4";
        public const string AccentColor = "AccentColor";
        public const string AccentColor4 = "AccentColor4";
    }

    public static class DefaultColors
    {
        public static Color AccentColor = Colors.Orange;
        public static Color AccentColor4 = Colors.Bisque;
    }
}