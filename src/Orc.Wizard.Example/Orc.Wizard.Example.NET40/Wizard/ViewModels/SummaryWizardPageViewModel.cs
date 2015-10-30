// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryWizardPageViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.Collections;
    using Example.Models;
    using Models;

    public class SummaryWizardPageViewModel : WizardPageViewModelBase<SummaryWizardPage>
    {
        public SummaryWizardPageViewModel(SummaryWizardPage wizardPage) 
            : base(wizardPage)
        {
            Skills = new ObservableCollection<Skill>();
            Components = new ObservableCollection<Component>();
        }

        public string FullName { get; private set; }

        public ObservableCollection<Skill> Skills { get; private set; }

        public ObservableCollection<Component> Components { get; private set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var wizard = Wizard;
            if (wizard == null)
            {
                return;
            }

            var personWizardPage = wizard.FindPageByType<PersonWizardPage>();
            if (personWizardPage != null)
            {
                FullName = string.Format("{0} {1}", personWizardPage.FirstName, personWizardPage.LastName);
            }

            var skillsWizardPage = wizard.FindPageByType<SkillsWizardPage>();
            if (skillsWizardPage != null)
            {
                Skills.AddRange(skillsWizardPage.Skills.Where(x => x.IsSelected));
            }

            var componentsWizardPage = wizard.FindPageByType<ComponentsWizardPage>();
            if (componentsWizardPage != null)
            {
                Components.AddRange(componentsWizardPage.Components.Where(x => x.IsSelected));
            }
        }
    }
}