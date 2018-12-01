import jetbrains.buildServer.configs.kotlin.v2018_1.*
import jetbrains.buildServer.configs.kotlin.v2018_1.buildFeatures.commitStatusPublisher
import jetbrains.buildServer.configs.kotlin.v2018_1.buildFeatures.nuGetPackagesIndexer
import jetbrains.buildServer.configs.kotlin.v2018_1.buildFeatures.swabra
import jetbrains.buildServer.configs.kotlin.v2018_1.buildFeatures.vcsLabeling
import jetbrains.buildServer.configs.kotlin.v2018_1.buildSteps.MSBuildStep
import jetbrains.buildServer.configs.kotlin.v2018_1.buildSteps.msBuild
import jetbrains.buildServer.configs.kotlin.v2018_1.buildSteps.script
import jetbrains.buildServer.configs.kotlin.v2018_1.failureConditions.BuildFailureOnMetric
import jetbrains.buildServer.configs.kotlin.v2018_1.failureConditions.failOnMetricChange
import jetbrains.buildServer.configs.kotlin.v2018_1.triggers.finishBuildTrigger
import jetbrains.buildServer.configs.kotlin.v2018_1.triggers.vcs
import jetbrains.buildServer.configs.kotlin.v2018_1.vcs.GitVcsRoot

/*
The settings script is an entry point for defining a TeamCity
project hierarchy. The script should contain a single call to the
project() function with a Project instance or an init function as
an argument.

VcsRoots, BuildTypes, Templates, and subprojects can be
registered inside the project using the vcsRoot(), buildType(),
template(), and subProject() methods respectively.

To debug settings scripts in command-line, run the

    mvnDebug org.jetbrains.teamcity:teamcity-configs-maven-plugin:generate

command and attach your debugger to the port 8000.

To debug in IntelliJ Idea, open the 'Maven Projects' tool window (View
-> Tool Windows -> Maven Projects), find the generate task node
(Plugins -> teamcity-configs -> teamcity-configs:generate), the
'Debug' option is available in the context menu for the task.
*/

version = "2018.1"

project {

    vcsRoot(WellFiredGuacamoleMaster)

    buildType(ReleaseBuildTestAndDeploy)
    buildType(ContinuousBuildAndTest)

    template(ManualBuildTestRelease)

    subProject(Documentation)
}

object ContinuousBuildAndTest : BuildType({
    templates(AbsoluteId("WellFiredUnityProduct_ContinuousBuildTestAndPublish"))
    name = "Continuous Build And Test"

    params {
        param("ProjectName", "WellFired.Guacamole")
        param("AssemblyVersion", "2018.2.0")
    }

    vcs {
        root(DslContext.settingsRoot)
    }

    steps {
        step {
            name = "NuGet Pack"
            id = "RUNNER_45"
            type = "jb.nuget.pack"
            param("nuget.pack.specFile", """
                solution/WellFired.Guacamole.Data/WellFired.Guacamole.Data.csproj
                solution/WellFired.Guacamole.Drawing/WellFired.Guacamole.Drawing.csproj
                solution/WellFired.Guacamole/WellFired.Guacamole.csproj
                solution/WellFired.Guacamole.Unity.Editor/WellFired.Guacamole.Unity.Editor.csproj
            """.trimIndent())
            param("nuget.pack.include.sources", "true")
            param("nuget.pack.output.directory", "build/packages")
            param("nuget.pack.commandline", "-MsbuildPath /usr/lib/mono/msbuild/15.0/bin/ -verbosity detailed -IncludeReferencedProjects")
            param("nuget.path", "%teamcity.tool.NuGet.CommandLine.DEFAULT%")
            param("nuget.pack.as.artifact", "true")
        }
        stepsOrder = arrayListOf("RUNNER_15", "RUNNER_2", "RUNNER_17", "RUNNER_18", "RUNNER_5", "RUNNER_19", "RUNNER_41", "RUNNER_42", "RUNNER_43", "RUNNER_45", "RUNNER_26")
    }

    triggers {
        vcs {
            id = "vcsTrigger"
            triggerRules = """
                -:user=wellfiredbuildmachine:**
                -:user=admin:**
            """.trimIndent()
        }
    }

    features {
        commitStatusPublisher {
            id = "BUILD_EXT_9"
            vcsRootExtId = "${DslContext.settingsRoot.id}"
            publisher = github {
                githubUrl = "https://api.github.com"
                authType = personalToken {
                    token = "credentialsJSON:9d67b277-229c-4c57-adec-246955cf14d3"
                }
            }
            param("secure:github_password", "credentialsJSON:52839beb-c983-4802-a59e-e0a9e155617c")
            param("github_username", "buildmachine@wellfired.com")
            param("github_oauth_user", "WellFiredDevelopment")
        }
        nuGetPackagesIndexer {
            id = "BUILD_EXT_10"
            feed = "WellFiredUnityProduct/default"
        }
        feature {
            id = "JetBrains.AssemblyInfo"
            type = "JetBrains.AssemblyInfo"
            param("file-format", "%system.build.number%")
            param("info-format", "%system.build.number%")
        }
    }
})

object ReleaseBuildTestAndDeploy : BuildType({
    templates(ManualBuildTestRelease)
    name = "Release Build Test And Deploy"

    artifactRules = """
        %ProjectName%.unitypackage
        +:documentation/xml => documentation.tar.gz
    """.trimIndent()
    buildNumberPattern = "%AssemblyVersion%.%build.counter%"

    params {
        param("ProjectName", "WellFired.Guacamole")
        param("AssemblyVersion", "2018.2.0")
    }

    vcs {
        root(WellFiredGuacamoleMaster)

        cleanCheckout = true
    }
})

object ManualBuildTestRelease : Template({
    name = "Build Test And Release Template"

    artifactRules = """
        %ProjectName%.unitypackage
        +:documentation/xml => documentation.tar.gz
    """.trimIndent()
    buildNumberPattern = "%AssemblyVersion%.%build.counter%"

    params {
        param("ProjectName", "WellFired.Guacamole")
        param("AssemblyVersion", "2018.0.0")
    }

    steps {
        script {
            name = "NPM Install"
            id = "RUNNER_3"
            scriptContent = "npm install"
        }
        step {
            name = "NuGet Restore"
            id = "RUNNER_4"
            type = "jb.nuget.installer"
            param("nuget.path", "%teamcity.tool.NuGet.CommandLine.DEFAULT%")
            param("nuget.updatePackages.mode", "sln")
            param("sln.path", "solution/%ProjectName%.sln")
        }
        msBuild {
            name = "Build Debug"
            id = "RUNNER_8"
            path = "solution/%ProjectName%.sln"
            version = MSBuildStep.MSBuildVersion.MONO_v4_5
            toolsVersion = MSBuildStep.MSBuildToolsVersion.V4_0
            platform = MSBuildStep.Platform.x64
            args = "/p:Configuration=Debug"
        }
        script {
            name = "Run Unit Tests"
            id = "RUNNER_12"
            scriptContent = "jake test:unit -c"
        }
        script {
            name = "Run Integration Tests"
            id = "RUNNER_14"
            scriptContent = "jake test:integration -c"
        }
        msBuild {
            name = "Build Release"
            id = "RUNNER_7"
            path = "solution/%ProjectName%.sln"
            version = MSBuildStep.MSBuildVersion.MONO_v4_5
            toolsVersion = MSBuildStep.MSBuildToolsVersion.V4_0
            platform = MSBuildStep.Platform.x64
            args = "/p:Configuration=Release"
        }
        script {
            name = "Process Shared Tools"
            id = "RUNNER_24"
            scriptContent = "jake unity:processSharedTools -c"
        }
        script {
            name = "Copy Changelog file"
            id = "RUNNER_39"
            scriptContent = "jake unity:updateChangelog -c"
        }
        script {
            name = "Build Unity Package"
            id = "RUNNER_13"
            executionMode = BuildStep.ExecutionMode.RUN_ON_SUCCESS
            scriptContent = "jake unity:package:build[unity/Assets/WellFired,%ProjectName%.unitypackage] -c"
        }
        step {
            name = "NuGet Pack"
            id = "RUNNER_11"
            type = "jb.nuget.pack"
            param("nuget.pack.specFile", """
                solution/WellFired.Guacamole.Data/WellFired.Guacamole.Data.csproj
                solution/WellFired.Guacamole.Drawing/WellFired.Guacamole.Drawing.csproj
                solution/WellFired.Guacamole/WellFired.Guacamole.csproj
                solution/WellFired.Guacamole.Unity.Editor/WellFired.Guacamole.Unity.Editor.csproj
            """.trimIndent())
            param("nuget.pack.output.directory", "build/packages")
            param("nuget.pack.commandline", "-MsbuildPath /usr/lib/mono/msbuild/15.0/bin/ -verbosity detailed -IncludeReferencedProjects")
            param("nuget.path", "%teamcity.tool.NuGet.CommandLine.DEFAULT%")
            param("nuget.pack.as.artifact", "true")
            param("nuget.pack.prefer.project", "true")
        }
        script {
            name = "Generate Documentation"
            id = "RUNNER_20"
            executionMode = BuildStep.ExecutionMode.RUN_ON_SUCCESS
            scriptContent = "jake documentation:generate -c"
        }
    }

    triggers {
        vcs {
            id = "vcsTrigger"
            branchFilter = ""
        }
    }

    failureConditions {
        failOnMetricChange {
            id = "BUILD_EXT_4"
            metric = BuildFailureOnMetric.MetricType.TEST_COUNT
            threshold = 20
            units = BuildFailureOnMetric.MetricUnit.PERCENTS
            comparison = BuildFailureOnMetric.MetricComparison.LESS
            compareTo = build {
                buildRule = lastSuccessful()
            }
        }
        failOnMetricChange {
            id = "BUILD_EXT_5"
            metric = BuildFailureOnMetric.MetricType.ARTIFACT_SIZE
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.LESS
            compareTo = value()
            param("anchorBuild", "lastSuccessful")
        }
    }

    features {
        swabra {
            id = "swabra"
            forceCleanCheckout = true
        }
        feature {
            id = "JetBrains.AssemblyInfo"
            type = "JetBrains.AssemblyInfo"
            param("file-format", "%system.build.number%")
            param("info-format", "%system.build.number%")
        }
        nuGetPackagesIndexer {
            id = "BUILD_EXT_31"
            feed = "_Root/default"
        }
    }
})

object WellFiredGuacamoleMaster : GitVcsRoot({
    name = "WellFired.Guacamole (+master)"
    url = "https://github.com/WellFiredDevelopment/wellfired.guacamole.git"
    branchSpec = "+:refs/heads/(master)"
    userForTags = "wellfiredbuildmachine"
    agentCleanPolicy = GitVcsRoot.AgentCleanPolicy.ALWAYS
    authMethod = password {
        userName = "wellfiredbuildmachine"
        password = "credentialsJSON:fb813fbf-f069-48eb-94fa-4fbc35911b3c"
    }
})


object Documentation : Project({
    name = "Documentation"

    vcsRoot(WellFiredGuacamoleDocumentation)

    buildType(DocumentationReleaseBuild)
    buildType(DocumentationContinuousBuild)
})

object DocumentationContinuousBuild : BuildType({
    templates(AbsoluteId("WellFiredUnityProduct_DocumentationBuilder"))
    name = "Continuous Build And Deploy Documentation"

    params {
        param("GitPushUrl", "github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git")
    }

    vcs {
        root(WellFiredGuacamoleDocumentation)
    }

    triggers {
        finishBuildTrigger {
            id = "TRIGGER_5"
            buildTypeExtId = "${ContinuousBuildAndTest.id}"
            successfulOnly = true
        }
    }

    dependencies {
        artifacts(ContinuousBuildAndTest) {
            id = "ARTIFACT_DEPENDENCY_2"
            buildRule = lastSuccessful()
            cleanDestination = true
            artifactRules = "+:documentation.tar.gz!**/*=>xml"
        }
    }
})

object DocumentationReleaseBuild : BuildType({
    templates(AbsoluteId("WellFiredUnityProduct_DocumentationBuilder"))
    name = "Release Build And Deploy Documentation"

    params {
        param("GitPushUrl", "github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git")
    }

    vcs {
        root(WellFiredGuacamoleDocumentation)
    }

    triggers {
        finishBuildTrigger {
            id = "TRIGGER_7"
            buildTypeExtId = "WellFiredUnityProduct_DotGuacamole_NightlyBuildTestAndDeploy"
            successfulOnly = true
        }
    }

    features {
        vcsLabeling {
            id = "BUILD_EXT_7"
            vcsRootId = "WellFiredUnityProduct_DotGuacamole_WellFiredGuacamoleDocumentation"
            labelingPattern = "%dep.WellFiredUnityProduct_DotGuacamole_ReleaseBuildTestAndDeploy.env.BUILD_NUMBER%"
            successfulOnly = true
            branchFilter = ""
        }
    }

    dependencies {
        artifacts(ReleaseBuildTestAndDeploy) {
            id = "ARTIFACT_DEPENDENCY_2"
            buildRule = lastSuccessful()
            cleanDestination = true
            artifactRules = "+:documentation.tar.gz!**/*=>xml"
        }
    }
})

object WellFiredGuacamoleDocumentation : GitVcsRoot({
    name = "https://github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git"
    url = "https://github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git"
    agentCleanPolicy = GitVcsRoot.AgentCleanPolicy.ALWAYS
    authMethod = password {
        userName = "WellFiredDevelopment"
        password = "credentialsJSON:dc06a2d5-db89-4121-88a8-9cff2984b59a"
    }
})
