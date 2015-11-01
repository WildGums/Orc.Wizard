// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleWizard.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.Models
{
    using Catel.IoC;

    public class ExampleWizard : WizardBase
    {
        public ExampleWizard(ITypeFactory typeFactory)
            : base(typeFactory)
        {
            Title = "Orc.Wizard example"; 

            this.AddPage<PersonWizardPage>();
            this.AddPage<SkillsWizardPage>();
            this.AddPage<ComponentsWizardPage>();
            this.AddPage<SummaryWizardPage>();
        }
    }
}