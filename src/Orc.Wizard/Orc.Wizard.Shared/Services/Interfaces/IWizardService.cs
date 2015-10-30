using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.Wizard.Services
{
    using System.Threading.Tasks;

    public interface IWizardService
    {
        Task ShowWizardAsync(IWizard wizard);
    }
}
