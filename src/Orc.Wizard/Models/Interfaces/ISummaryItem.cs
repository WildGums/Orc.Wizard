namespace Orc.Wizard;

public interface ISummaryItem
{
    IWizardPage? Page { get; set; }

    string? Title { get; set; }
    string? Summary { get; set; }
}