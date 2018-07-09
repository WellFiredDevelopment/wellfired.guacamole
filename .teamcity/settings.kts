import jetbrains.buildServer.configs.kotlin.v2018_1.*
import jetbrains.buildServer.configs.kotlin.v2018_1.buildFeatures.swabra
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

    vcsRoot(HttpsGithubComWellFiredDevelopmentWellfiredGuaca)

    buildType(BuildTestAndRelease)
    buildType(ContinuousBuildAndTest)

    template(ManualBuildTestAndRelease_2)

    features {
        feature {
            id = "PROJECT_EXT_11"
            type = "OAuthProvider"
            param("clientId", "5c4d9fa26949f362c5f6")
            param("secure:clientSecret", "credentialsJSON:c6257f53-15aa-479d-a746-1dd5daec390e")
            param("displayName", "GitHub.com")
            param("gitHubUrl", "https://github.com/")
            param("providerType", "GitHub")
        }
    }
    buildTypesOrder = arrayListOf(ContinuousBuildAndTest, BuildTestAndRelease)

    subProject(Documentation)
}

object BuildTestAndRelease : BuildType({
    templates(ManualBuildTestAndRelease_2)
    name = "Build Test And Release"

    params {
        param("AssemblyVersion", "1.0.0")
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
})

object ContinuousBuildAndTest : BuildType({
    templates(AbsoluteId("WellFiredUnityProduct_ContinuousBuildTestAndPublish"))
    name = "Continuous Build And Test"

    params {
        param("ProjectName", "WellFired.Guacamole")
        param("AssemblyVersion", "1.0.0")
    }

    vcs {
        root(DslContext.settingsRoot)
    }

    triggers {
        vcs {
            id = "vcsTrigger"
        }
    }

    failureConditions {
        failOnMetricChange {
            id = "BUILD_EXT_14"
            metric = BuildFailureOnMetric.MetricType.ARTIFACT_SIZE
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.LESS
            compareTo = value()
            param("anchorBuild", "lastSuccessful")
        }
    }
})

object ManualBuildTestAndRelease_2 : Template({
    name = "Build Test And Release"

    artifactRules = """
        %ProjectName%.unitypackage
        +:documentation/xml => documentation.tar.gz
    """.trimIndent()
    buildNumberPattern = "%AssemblyVersion%.%build.counter%"

    params {
        param("ProjectName", "WellFired.Guacamole")
        param("AssemblyVersion", "0.0.1")
    }

    vcs {
        root(HttpsGithubComWellFiredDevelopmentWellfiredGuaca)
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
            scriptContent = "jake test:unit"
        }
        script {
            name = "Run Integration Tests"
            id = "RUNNER_14"
            scriptContent = "jake test:integration"
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
            scriptContent = "jake unity:processSharedTools"
        }
        script {
            name = "Build Unity Package"
            id = "RUNNER_13"
            executionMode = BuildStep.ExecutionMode.RUN_ON_SUCCESS
            scriptContent = "jake unity:package:build[unity/Assets/WellFired,%ProjectName%.unitypackage]"
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
            scriptContent = "jake documentation:generate"
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
    }
})

object HttpsGithubComWellFiredDevelopmentWellfiredGuaca : GitVcsRoot({
    name = "WellFired.Guacamole (+all, -master"
    url = "https://github.com/WellFiredDevelopment/wellfired.guacamole.git"
    branch = "refs/heads/develop"
    branchSpec = """
        +:*
        -:refs/heads/develop
    """.trimIndent()
    agentCleanPolicy = GitVcsRoot.AgentCleanPolicy.ALWAYS
    authMethod = password {
        userName = "ArtOfSettling"
        password = "credentialsJSON:713ff8bc-24f8-4fe9-96ea-6871d915943f"
    }
})


object Documentation : Project({
    name = "Documentation"

    vcsRoot(Documentation_HttpsGithubComWellFiredDevelopment)

    buildType(Documentation_ContinuousBuildAndDeployDocumentat)
})

object Documentation_ContinuousBuildAndDeployDocumentat : BuildType({
    templates(AbsoluteId("WellFiredUnityProduct_DocumentationBuilder"))
    name = "Continuous Build And Deploy Documentation"

    params {
        param("GitPushUrl", "github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git")
    }

    vcs {
        root(Documentation_HttpsGithubComWellFiredDevelopment)
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
            id = "ARTIFACT_DEPENDENCY_1"
            buildRule = lastSuccessful()
            cleanDestination = true
            artifactRules = "+:documentation.tar.gz!**/*=>xml"
        }
    }
})

object Documentation_HttpsGithubComWellFiredDevelopment : GitVcsRoot({
    name = "https://github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git"
    url = "https://github.com/WellFiredDevelopment/dotGuacamoleDocumentation.git"
    agentCleanPolicy = GitVcsRoot.AgentCleanPolicy.ALWAYS
    authMethod = password {
        userName = "WellFiredDevelopment"
        password = "credentialsJSON:dc06a2d5-db89-4121-88a8-9cff2984b59a"
    }
})
