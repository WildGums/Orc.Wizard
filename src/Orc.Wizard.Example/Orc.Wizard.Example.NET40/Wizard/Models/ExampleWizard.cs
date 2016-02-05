// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleWizard.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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
            this.AddPage<AgeWizardPage>();
            this.AddPage<SkillsWizardPage>();
            this.AddPage<GadgetsWizardPage>();
            this.AddPage<ComponentsWizardPage>();
            this.AddPage<SummaryWizardPage>();
        }
    }
}