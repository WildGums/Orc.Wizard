namespace Orc.Wizard.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Catel.Collections;
    using Orc.Wizard;

    public sealed partial class StepBar
    {
        public StepBar()
        {
            InitializeComponent();
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation),
            typeof(StepBar), new PropertyMetadata(Orientation.Vertical));
    }
}
