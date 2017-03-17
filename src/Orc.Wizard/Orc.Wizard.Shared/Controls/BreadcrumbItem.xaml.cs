// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadcrumbItem.xaml.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Wizard.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    public sealed partial class BreadcrumbItem
    {
        public BreadcrumbItem()
        {
            InitializeComponent();
        }

        public IWizardPage Page
        {
            get { return (IWizardPage)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty = DependencyProperty.Register("Page", typeof(IWizardPage),
            typeof(BreadcrumbItem), new PropertyMetadata(null, (sender, e) => ((BreadcrumbItem)sender).OnPageChanged()));


        public IWizardPage CurrentPage
        {
            get { return (IWizardPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(IWizardPage),
            typeof(BreadcrumbItem), new PropertyMetadata(null, (sender, e) => ((BreadcrumbItem)sender).OnCurrentPageChanged()));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(BreadcrumbItem), new PropertyMetadata(string.Empty));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string),
            typeof(BreadcrumbItem), new PropertyMetadata(string.Empty));


        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int),
            typeof(BreadcrumbItem), new PropertyMetadata(0));

        private void OnPageChanged()
        {
            var page = Page;
            if (page != null)
            {
                Number = page.Number;
                Title = page.BreadcrumbTitle ?? page.Title;
                Description = page.Description;
            }
        }

        private void OnCurrentPageChanged()
        {
            var isSelected = ReferenceEquals(CurrentPage, Page);

            UpdateSelection(isSelected);
        }

        private void UpdateSelection(bool isSelected)
        {
            var storyboard = new Storyboard();

            var colorName = isSelected ? DefaultColorNames.AccentColor : DefaultColorNames.AccentColor4;

            if (ellipse != null && ellipse.Fill == null)
            {
                ellipse.Fill = (SolidColorBrush)TryFindResource(DefaultColorNames.AccentColorBrush4) ?? new SolidColorBrush(DefaultColors.AccentColor4);
            }
            
            var fromColor = ((SolidColorBrush)ellipse?.Fill)?.Color ?? DefaultColors.AccentColor4;
            var targetColor = TryFindResource(colorName) ?? DefaultColors.AccentColor;
            if (targetColor is Color)
            {
                var colorAnimation = new ColorAnimation(fromColor, (Color)targetColor, WizardConfiguration.AnimationDuration);
                Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Fill.(SolidColorBrush.Color)"));

                storyboard.Children.Add(colorAnimation);
            }

            storyboard.Begin(ellipse);
            this.txtTitle.Foreground = isSelected ? Brushes.Black : Brushes.DimGray;
        }
    }
}