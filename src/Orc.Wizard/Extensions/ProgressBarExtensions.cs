﻿namespace Orc.Wizard;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

public static class ProgressBarExtensions
{
    public static readonly DependencyProperty SmoothProgressProperty = DependencyProperty.RegisterAttached("SmoothProgress",
        typeof(double), typeof(ProgressBarExtensions), new UIPropertyMetadata(0.0, OnSmoothProgressChanged));

    public static void SetSmoothProgress(FrameworkElement target, double value)
    {
        target.SetValue(SmoothProgressProperty, value);
    }

    public static double GetSmoothProgress(FrameworkElement target)
    {
        return (double)target.GetValue(SmoothProgressProperty);
    }

    private static void OnSmoothProgressChanged(DependencyObject? target, DependencyPropertyChangedEventArgs e)
    {
        if (target is ProgressBar progressBar)
        {
            progressBar.SetCurrentValue(RangeBase.ValueProperty, (double) e.NewValue);
        }
    }

    public static void UpdateProgress(this ProgressBar progressBar, int currentItem, int totalItems)
    {
        ArgumentNullException.ThrowIfNull(progressBar);

        progressBar.SetCurrentValue(RangeBase.MinimumProperty, (double)0);
        progressBar.SetCurrentValue(RangeBase.MaximumProperty, (double)totalItems);

        var progressAnimation = new DoubleAnimation
        {
            From = progressBar.Value,
            To = currentItem,
            DecelerationRatio = .2,
            Duration = new Duration(WizardConfiguration.AnimationDuration)
        };

        var storyboard = new Storyboard();
        storyboard.Children.Add(progressAnimation);
        Storyboard.SetTarget(progressAnimation, progressBar);
        Storyboard.SetTargetProperty(progressAnimation, new PropertyPath(SmoothProgressProperty));
        storyboard.Begin();
    }
}
