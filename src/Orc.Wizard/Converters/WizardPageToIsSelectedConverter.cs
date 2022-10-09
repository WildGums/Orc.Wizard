namespace Orc.Wizard.Converters
{
    using System;
    using Catel.MVVM.Converters;

    public class WizardPageToIsSelectedConverter : ValueConverterBase<IWizardPage>
    {
        protected override object? Convert(IWizardPage? value, Type targetType, object? parameter)
        {
            if (value is null)
            {
                return false;
            }

            var wizard = value.Wizard;

            if (ReferenceEquals(wizard?.CurrentPage, value))
            {
                return true;
            }

            return false;
        }
    }
}
