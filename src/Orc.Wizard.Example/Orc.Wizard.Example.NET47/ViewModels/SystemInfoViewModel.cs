// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfoViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SystemInfo.Example.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Threading;

    public class SystemInfoViewModel : ViewModelBase
    {
        private readonly ISystemInfoService _systemInfoService;

        public SystemInfoViewModel(ISystemInfoService systemInfoService)
        {
            Argument.IsNotNull(() => systemInfoService);

            _systemInfoService = systemInfoService;
        }

        public bool IsBusy { get; private set; }

        public string SystemInfo { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            IsBusy = true;

            var systemInfo = await TaskHelper.Run(() => _systemInfoService.GetSystemInfo(), true);
            var systemInfoLines = systemInfo.Select(x => string.Format("{0} {1}", x.Name, x.Value));
            SystemInfo = string.Join("\n", systemInfoLines);

            IsBusy = false;
        }
    }
}