namespace Orc.Wizard.Controls;

using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Automation;
using Wizard;

public sealed partial class BreadcrumbItem
{
    public BreadcrumbItem()
    {
        InitializeComponent();
    }

    public IWizardPage? Page
    {
        get { return (IWizardPage?)GetValue(PageProperty); }
        set { SetValue(PageProperty, value); }
    }

    public static readonly DependencyProperty PageProperty = DependencyProperty.Register(nameof(Page), typeof(IWizardPage),
        typeof(BreadcrumbItem), new PropertyMetadata(null, (sender, _) => ((BreadcrumbItem)sender).OnPageChanged()));


    public IWizardPage? CurrentPage
    {
        get { return (IWizardPage?)GetValue(CurrentPageProperty); }
        set { SetValue(CurrentPageProperty, value); }
    }

    public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(nameof(CurrentPage), typeof(IWizardPage),
        typeof(BreadcrumbItem), new PropertyMetadata(null, (sender, _) => ((BreadcrumbItem)sender).OnCurrentPageChanged()));


    public string? Title
    {
        get { return (string?)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string),
        typeof(BreadcrumbItem), new PropertyMetadata(string.Empty));


    public string? Description
    {
        get { return (string?)GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string),
        typeof(BreadcrumbItem), new PropertyMetadata(string.Empty));
        
    public int Number
    {
        get { return (int)GetValue(NumberProperty); }
        set { SetValue(NumberProperty, value); }
    }

    public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof(Number), typeof(int),
        typeof(BreadcrumbItem), new PropertyMetadata(0));

    private void OnPageChanged()
    {
        var page = Page;
        if (page is not null)
        {
            SetCurrentValue(NumberProperty, page.Number);
            SetCurrentValue(TitleProperty, page.BreadcrumbTitle ?? page.Title);
            SetCurrentValue(DescriptionProperty, page.Description);

            pathline.SetCurrentValue(VisibilityProperty, page.Wizard?.IsLastPage(page) ?? false ? Visibility.Collapsed : Visibility.Visible);
        }
    }

    private void OnCurrentPageChanged()
    {
        var page = Page;
        var currentPage = CurrentPage;

        var isSelected = ReferenceEquals(currentPage, page);
        var isCompleted = page is not null && currentPage is not null && page.Number < currentPage.Number;
        var isVisited = page?.IsVisited ?? false;

        SetCurrentValue(CursorProperty, (page?.Wizard?.AllowQuickNavigation ?? false) ? System.Windows.Input.Cursors.Hand : null);
        UpdateContent(isCompleted);
        UpdateSelection(isSelected, isCompleted, isVisited);
    }

    private void UpdateSelection(bool isSelected, bool isCompleted, bool isVisited)
    {
        UpdateShapeColor(pathline, isCompleted && !isSelected);
        UpdateShapeColor(ellipse, isSelected || isVisited);

        txtTitle.SetCurrentValue(System.Windows.Controls.TextBlock.ForegroundProperty, isSelected ?
            TryFindResource("Orc.Brushes.Black") : (isVisited ?
                TryFindResource("Orc.Brushes.GrayBrush2") :
                TryFindResource("Orc.Brushes.GrayBrush1")));
    }

    private void UpdateContent(bool isCompleted)
    {
        ellipseText.SetCurrentValue(VisibilityProperty, isCompleted ? Visibility.Hidden : Visibility.Visible);
        ellipseCheck.SetCurrentValue(VisibilityProperty, isCompleted ? Visibility.Visible : Visibility.Hidden);
    }

    private void UpdateShapeColor(Shape shape, bool isSelected)
    {
        var storyboard = new Storyboard();

        if (shape.Fill is null)
        {
#pragma warning disable WPF0041 // Set mutable dependency properties using SetCurrentValue.
            shape.Fill = (SolidColorBrush)TryFindResource(ThemingKeys.AccentColorBrush40);
#pragma warning restore WPF0041 // Set mutable dependency properties using SetCurrentValue.
        }

        var fromColor = ((SolidColorBrush?)shape.Fill)?.Color ?? Colors.Transparent;
        var targetColor = this.GetAccentColorBrush(isSelected).Color;

        var colorAnimation = new ColorAnimation(fromColor, targetColor, WizardConfiguration.AnimationDuration);
        Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Fill.(SolidColorBrush.Color)", Array.Empty<object>()));

        storyboard.Children.Add(colorAnimation);

        storyboard.Begin(shape);
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new BreadcrumbItemPeer(this);
    }
}
