namespace Orc.Wizard.Example.Views;

using Catel.Logging;
using Logging;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView
{
    public MainView()
    {
        InitializeComponent();

        var logListener = new TextBoxLogListener(loggingTextBox)
        {
            IgnoreCatelLogging = true
        };

        LogManager.AddListener(logListener);
    }
}