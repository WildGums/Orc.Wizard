#l "./continuaci.cake"

var buildServerVariables = ContinuaCI.Environment.Variable;

var target = GetContinuaCIVariable("Target", "Default");
var versionMajorMinorPatch = GetContinuaCIVariable("GitVersion_MajorMinorPatch", "3.0.0");
var versionFullSemVer = GetContinuaCIVariable("GitVersion_FullSemVer", "3.0.0-alpha.1");
var versionNuGet = GetContinuaCIVariable("GitVersion_NuGetVersion", "3.0.0-alpha0001");
var solutionName = GetContinuaCIVariable("SolutionName", string.Format("{0}.sln", projectName));
var isCiBuild = bool.Parse(GetContinuaCIVariable("IsCiBuild", "False"));
var configurationName = GetContinuaCIVariable("ConfigurationName", "Release");
var outputRootDirectory = GetContinuaCIVariable("OutputRootDirectory", string.Format("./output/{0}", configurationName));
var codeSignWildCard = GetContinuaCIVariable("CodeSignWildcard", projectName);
var codeSignCertificateSubjectName = GetContinuaCIVariable("CodeSignCertificateSubjectName", company);
var codeSignTimeStampUri = GetContinuaCIVariable("CodeSignTimeStampUri", "http://timestamp.comodoca.com/authenticode");

var nuGetPackageSources = GetContinuaCIVariable("NuGetPackageSources", string.Empty);
var repositoryUrl = GetContinuaCIVariable("RepositoryUrl", defaultRepositoryUrl);
var repositoryBranchName = GetContinuaCIVariable("RepositoryBranchName", string.Empty);
var repositoryCommitId = GetContinuaCIVariable("RepositoryCommitId", string.Empty);

var sonarUrl = GetContinuaCIVariable("SonarUrl", string.Empty);
var sonarUsername = GetContinuaCIVariable("SonarUsername", string.Empty);
var sonarPassword = GetContinuaCIVariable("SonarPassword", string.Empty);
var sonarProject = GetContinuaCIVariable("SonarProject", projectName);

var solutionAssemblyInfoFileName = "./src/SolutionAssemblyInfo.cs";
var solutionFileName = string.Format("./src/{0}", solutionName);
var platforms = new Dictionary<string, PlatformTarget>();
platforms["AnyCPU"] = PlatformTarget.MSIL;
//platforms["x86"] = PlatformTarget.x86;
//platforms["x64"] = PlatformTarget.x64;
//platforms["arm"] = PlatformTarget.ARM;