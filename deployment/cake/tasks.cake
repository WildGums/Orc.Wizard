#l "lib-generic.cake"
#l "lib-nuget.cake"
#l "lib-sourcelink.cake"
#l "issuetrackers.cake"
#l "notifications.cake"
#l "generic-tasks.cake"
#l "apps-uwp-tasks.cake"
#l "apps-web-tasks.cake"
#l "apps-wpf-tasks.cake"
#l "components-tasks.cake"
#l "tools-tasks.cake"
#l "docker-tasks.cake"
#l "github-pages-tasks.cake"
#l "vsextensions-tasks.cake"
#l "tests.cake"

#addin "nuget:?package=System.Net.Http&version=4.3.3"
#addin "nuget:?package=Newtonsoft.Json&version=11.0.2"
#addin "nuget:?package=Cake.Sonar&version=1.1.0"

#tool "nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.3.0"
#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012&prerelease"

//-------------------------------------------------------------

Initialize();

Information("Running target '{0}'", Target);
Information("Using output directory '{0}'", OutputRootDirectory);

//-------------------------------------------------------------

Information("Validating input");

ValidateGenericInput();
ValidateUwpAppsInput();
ValidateWebAppsInput();
ValidateWpfAppsInput();
ValidateComponentsInput();
ValidateToolsInput();
ValidateDockerImagesInput();
ValidateGitHubPagesInput();
ValidateVsExtensionsInput();

//-------------------------------------------------------------

public void Initialize()
{
    LogSeparator("Initializing versioning");

    if (string.IsNullOrWhiteSpace(VersionNuGet) || VersionNuGet == "unknown")
    {
        Information("No version info specified, falling back to GitVersion");

        var gitVersion = GitVersionContext;
        
        VersionMajorMinorPatch = gitVersion.MajorMinorPatch;
        VersionFullSemVer = gitVersion.FullSemVer;
        VersionNuGet = gitVersion.NuGetVersionV2;
        VersionCommitsSinceVersionSource = (gitVersion.CommitsSinceVersionSource ?? 0).ToString();
    }

    var versionToCheck = VersionFullSemVer;
    if (versionToCheck.Contains("alpha"))
    {
        IsAlphaBuild = true;
    }
    else if (versionToCheck.Contains("beta"))
    {
        IsBetaBuild = true;
    }
    else
    {
        IsOfficialBuild = true;
    }

    Information("Defined version: '{0}', commits since version source: '{1}'", VersionFullSemVer, VersionCommitsSinceVersionSource);

    if (string.IsNullOrWhiteSpace(RepositoryCommitId))
    {
        Information("No commit id specified, falling back to GitVersion");

        var gitVersion = GitVersionContext;

        RepositoryCommitId = gitVersion.Sha;
    }

    OutputRootDirectory = System.IO.Path.GetFullPath(OutputRootDirectory);

    LogSeparator("Initializing the state of the build");

    // Determine some special variables
    Channel = DetermineChannel();
    PublishType = DeterminePublishType();

    Information($"IsAlphaBuild: {IsAlphaBuild}");
    Information($"IsBetaBuild: {IsBetaBuild}");
    Information($"IsOfficialBuild: {IsOfficialBuild}");
    Information($"Channel: {Channel}");
    Information($"PublishType: {PublishType}");
}

//-------------------------------------------------------------

private string DetermineChannel()
{
    var version = VersionFullSemVer;

    var channel = "stable";

    if (IsAlphaBuild)
    {
        channel = "alpha";
    }
    else if (IsBetaBuild)
    {
        channel = "beta";
    }

    return channel;
}

//-------------------------------------------------------------

private string DeterminePublishType()
{
    var publishType = "Unknown";

    if (IsOfficialBuild)
    {
        publishType = "Official";
    }
    else if (IsBetaBuild)
    {
        publishType = "Beta";
    }
    else if (IsAlphaBuild)
    {
        publishType = "Alpha";
    }
    
    return publishType;
}

//-------------------------------------------------------------

private void BuildTestProjects()
{
    // In case of a local build and we have included / excluded anything, skip tests
    if (IsLocalBuild && (Include.Length > 0 || Exclude.Length > 0))
    {
        Information("Skipping test project because this is a local build with specific includes / excludes");
        return;
    }

    foreach (var testProject in TestProjects)
    {
        LogSeparator("Building test project '{0}'", testProject);

        var projectFileName = GetProjectFileName(testProject);
        
        var msBuildSettings = new MSBuildSettings
        {
            Verbosity = Verbosity.Quiet, // Verbosity.Diagnostic
            ToolVersion = MSBuildToolVersion.Default,
            Configuration = ConfigurationName,
            MSBuildPlatform = MSBuildPlatform.x86, // Always require x86, see platform for actual target platform
            PlatformTarget = PlatformTarget.MSIL
        };

        ConfigureMsBuild(msBuildSettings, testProject);

        // Always disable SourceLink
        msBuildSettings.WithProperty("EnableSourceLink", "false");

        // Force disable SonarQube
        msBuildSettings.WithProperty("SonarQubeExclude", "true");

        // Note: we need to set OverridableOutputPath because we need to be able to respect
        // AppendTargetFrameworkToOutputPath which isn't possible for global properties (which
        // are properties passed in using the command line)
        var outputDirectory = string.Format("{0}/{1}/", OutputRootDirectory, testProject);
        Information("Output directory: '{0}'", outputDirectory);
        msBuildSettings.WithProperty("OverridableOutputPath", outputDirectory);
        msBuildSettings.WithProperty("PackageOutputPath", OutputRootDirectory);

        MSBuild(projectFileName, msBuildSettings);
    }
}

//-------------------------------------------------------------

Task("Initialize")
    .Does(async () =>
{
    LogSeparator("Writing special values back to build server");

    var displayVersion = VersionFullSemVer;
    if (IsCiBuild)
    {
        displayVersion += " ci";
    }

    SetBuildServerVersion(displayVersion);

    var variablesToUpdate = new Dictionary<string, string>();
    variablesToUpdate["channel"] = Channel;
    variablesToUpdate["publishType"] = PublishType.ToString();
    variablesToUpdate["isAlphaBuild"] = IsAlphaBuild.ToString();
    variablesToUpdate["isBetaBuild"] = IsBetaBuild.ToString();
    variablesToUpdate["isOfficialBuild"] = IsOfficialBuild.ToString();

    // Also write back versioning (then it can be cached), "worst case scenario" it's writing back the same versions
    variablesToUpdate["GitVersion_MajorMinorPatch"] = VersionMajorMinorPatch;
    variablesToUpdate["GitVersion_FullSemVer"] = VersionFullSemVer;
    variablesToUpdate["GitVersion_NuGetVersion"] = VersionNuGet;
    variablesToUpdate["GitVersion_CommitsSinceVersionSource"] = VersionCommitsSinceVersionSource;

    foreach (var variableToUpdate in variablesToUpdate)
    {
        SetBuildServerVariable(variableToUpdate.Key, variableToUpdate.Value);
    }
});

//-------------------------------------------------------------

Task("Prepare")
    .Does(async () =>
{
    await PrepareForComponentsAsync();
    await PrepareForToolsAsync();
    await PrepareForUwpAppsAsync();
    await PrepareForWebAppsAsync();
    await PrepareForWpfAppsAsync();
    await PrepareForDockerImagesAsync();
    await PrepareForGitHubPagesAsync();
    await PrepareForVsExtensionsAsync();
});

//-------------------------------------------------------------

Task("UpdateInfo")
    .IsDependentOn("Prepare")
    .Does(() =>
{
    UpdateSolutionAssemblyInfo();
    
    UpdateInfoForComponents();
    UpdateInfoForTools();
    UpdateInfoForUwpApps();
    UpdateInfoForWebApps();
    UpdateInfoForWpfApps();
    UpdateInfoForDockerImages();
    UpdateInfoForGitHubPages();
    UpdateInfoForVsExtensions();
});

//-------------------------------------------------------------

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("UpdateInfo")
    .IsDependentOn("VerifyDependencies")
    .IsDependentOn("CleanupCode")
    .Does(async () =>
{
    var enableSonar = !SonarDisabled && !string.IsNullOrWhiteSpace(SonarUrl);
    if (enableSonar)
    {
        SonarBegin(new SonarBeginSettings 
        {
            // SonarQube info
            Url = SonarUrl,
            Login = SonarUsername,
            Password = SonarPassword,

            // Project info
            Key = SonarProject,
            // Branch only works with the branch plugin
            //Branch = RepositoryBranchName,
            Version = VersionFullSemVer,
            
            // Minimize extreme logging
            Verbose = false,
            Silent = true,
        });
    }
    else
    {
        Information("Skipping Sonar integration since url is not specified or it has been explicitly disabled");
    }

    BuildComponents();
    BuildTools();
    BuildUwpApps();
    BuildWebApps();
    BuildWpfApps();
    BuildDockerImages();
    BuildGitHubPages();
    BuildVsExtensions();

    if (enableSonar)
    {
        SonarEnd(new SonarEndSettings 
        {
            Login = SonarUsername,
            Password = SonarPassword,
        });
        
        Information("Checking whether the project passed the SonarQube gateway...");
            
        var status = "none";

        // We need to use /api/qualitygates/project_status
        var client = new System.Net.Http.HttpClient();
        using (client)
        {
            var queryUri = string.Format("{0}/api/qualitygates/project_status?projectKey={1}", SonarUrl, SonarProject);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", SonarUsername, SonarPassword));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Debug("Invoking GET request: '{0}'", queryUri);

            var response = await client.GetAsync(new Uri(queryUri));

            Debug("Parsing request contents");

            var content = response.Content;
            var jsonContent = await content.ReadAsStringAsync();

            Debug(jsonContent);

            dynamic result = Newtonsoft.Json.Linq.JObject.Parse(jsonContent);
            status = result.projectStatus.status;
        }

        Information("SonarQube gateway status returned from request: '{0}'", status);

        if (string.IsNullOrWhiteSpace(status))
        {
            status = "none";
        }

        status = status.ToLower();

        switch (status)
        {
            case "error":
                throw new Exception(string.Format("The SonarQube gateway for '{0}' returned ERROR, please check the error(s) at {1}/dashboard?id={0}", SonarProject, SonarUrl));

            case "warn":
                Warning("The SonarQube gateway for '{0}' returned WARNING, please check the warning(s) at {1}/dashboard?id={0}", SonarProject, SonarUrl);
                break;

            case "none":
                Warning("The SonarQube gateway for '{0}' returned NONE, please check why no gateway status is available at {1}/dashboard?id={0}", SonarProject, SonarUrl);
                break;

            case "ok":
                Information("The SonarQube gateway for '{0}' returned OK, well done! If you want to show off the results, check out {1}/dashboard?id={0}", SonarProject, SonarUrl);
                break;

            default:
                throw new Exception(string.Format("Unexpected SonarQube gateway status '{0}' for project '{1}'", status, SonarProject));
        }
    }

    BuildTestProjects();
});

//-------------------------------------------------------------

Task("Test")
    // Note: no dependency on 'build' since we might have already built the solution
    .Does(() =>
{
    foreach (var testProject in TestProjects)
    {
        LogSeparator("Running tests for '{0}'", testProject);

        RunUnitTests(testProject);
    }
});

//-------------------------------------------------------------

Task("Package")
    // Note: no dependency on 'build' since we might have already built the solution
    // Make sure we have the temporary "project.assets.json" in case we need to package with Visual Studio
    .IsDependentOn("RestorePackages")
    // Make sure to update if we are running on a new agent so we can sign nuget packages
    .IsDependentOn("UpdateNuGet")
    .IsDependentOn("CodeSign")
    .Does(() =>
{
    PackageComponents();
    PackageTools();
    PackageUwpApps();
    PackageWebApps();
    PackageWpfApps();
    PackageDockerImages();
    PackageGitHubPages();
    PackageVsExtensions();
});

//-------------------------------------------------------------

Task("PackageLocal")
    .IsDependentOn("Package")
    .Does(() =>
{
    // For now only package components, we might need to move this to components-tasks.cake in the future
    if (!HasComponents() && !HasTools())
    {
        return;
    }

    Information("Copying build artifacts to '{0}'", NuGetLocalPackagesDirectory);
    
    CreateDirectory(NuGetLocalPackagesDirectory);

    foreach (var component in Components)
    {
        Information("Copying build artifact for '{0}'", component);
    
        var sourceFile = string.Format("{0}/{1}.{2}.nupkg", OutputRootDirectory, component, VersionNuGet);
        CopyFiles(new [] { sourceFile }, NuGetLocalPackagesDirectory);
    }
});

//-------------------------------------------------------------

Task("Deploy")
    // Note: no dependency on 'package' since we might have already packaged the solution
    // Make sure we have the temporary "project.assets.json" in case we need to package with Visual Studio
    .IsDependentOn("RestorePackages")
    .Does(async () =>
{
    await DeployComponentsAsync();
    await DeployToolsAsync();
    await DeployUwpAppsAsync();
    await DeployWebAppsAsync();
    await DeployWpfAppsAsync();
    await DeployDockerImagesAsync();
    await DeployGitHubPagesAsync();
    await DeployVsExtensionsAsync();
});

//-------------------------------------------------------------

Task("Finalize")
    // Note: no dependency on 'deploy' since we might have already deployed the solution
    .Does(async () =>
{
    Information("Finalizing release '{0}'", VersionFullSemVer);

    await CreateAndReleaseVersionAsync();
});

//-------------------------------------------------------------
// Wrapper tasks since we don't want to add "Build" as a 
// dependency to "Package" because we want to run in multiple
// stages
//-------------------------------------------------------------

Task("BuildAndTest")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

//-------------------------------------------------------------

Task("BuildAndPackage")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

//-------------------------------------------------------------

Task("BuildAndPackageLocal")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build")
    //.IsDependentOn("Test") // Note: don't test for performance on local builds
    .IsDependentOn("PackageLocal");

//-------------------------------------------------------------

Task("BuildAndDeploy")
    .IsDependentOn("Initialize")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package")
    .IsDependentOn("Deploy");

//-------------------------------------------------------------

Task("Default")    
    .Does(async () =>
{
    Error("No target specified, please specify one of the following targets:\n" +
          " - Prepare\n" +
          " - UpdateInfo\n" +
          " - Build\n" + 
          " - Test\n" + 
          " - Package\n" + 
          " - Deploy\n" + 
          " - Finalize\n\n" + 
          "or one of the combined ones:\n" +
          " - BuildAndTest\n" + 
          " - BuildAndPackage\n" + 
          " - BuildAndPackageLocal\n" + 
          " - BuildAndDeploy\n");
});


//-------------------------------------------------------------
// Test wrappers
//-------------------------------------------------------------

Task("TestNotifications")    
    .Does(async () =>
{
    await NotifyAsync("MyProject", "This is a generic test");
    await NotifyAsync("MyProject", "This is a component test", TargetType.Component);
    await NotifyAsync("MyProject", "This is a docker image test", TargetType.DockerImage);
    await NotifyAsync("MyProject", "This is a web app test", TargetType.WebApp);
    await NotifyAsync("MyProject", "This is a wpf app test", TargetType.WpfApp);
    await NotifyErrorAsync("MyProject", "This is an error");
});


//-------------------------------------------------------------

RunTarget(Target);