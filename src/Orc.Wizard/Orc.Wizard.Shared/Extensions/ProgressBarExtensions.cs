// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2016 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public static class ProgressBarExtensions
    {
        public static readonly DependencyProperty SmoothProgressProperty = DependencyProperty.RegisterAttached("SmoothProgress",
            typeof(double), typeof(ProgressBarExtensions), new UIPropertyMetadata(0.0, OnSmoothProgressChanged));

        public static void SetSmoothProgressOffset(FrameworkElement target, double value)
        {
            target.SetValue(SmoothProgressProperty, value);
        }

        public static double GetSmoothProgress(FrameworkElement target)
        {
            return (double)target.GetValue(SmoothProgressProperty);
        }

        private static void OnSmoothProgressChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = target as ProgressBar;
            if (progressBar != null)
            {
                progressBar.Value = (double) e.NewValue;
            }
        }

        public static void UpdateProgress(this ProgressBar progressBar, int currentItem, int totalItems)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = totalItems;

            var progressAnimation = new DoubleAnimation();
            progressAnimation.From = progressBar.Value;
            progressAnimation.To = currentItem;
            progressAnimation.DecelerationRatio = .2;
            progressAnimation.Duration = new Duration(WizardConfiguration.AnimationDuration);

            var storyboard = new Storyboard();
            storyboard.Children.Add(progressAnimation);
            Storyboard.SetTarget(progressAnimation, progressBar);
            Storyboard.SetTargetProperty(progressAnimation, new PropertyPath(SmoothProgressProperty));
            storyboard.Begin();
        }
    }
}