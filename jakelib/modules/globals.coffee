global.config = {
    name:           'Guacamole',
    slnPath:        'solution/Guacamole.sln',
    unityAssets:    'unity/Assets',
    unityBin:       'unity/Assets/Code',
    unityEditorBin: 'unity/Assets/Code/Editor',
    integrationDlls:'solution/Tests/Core/*/bin/Debug/*.Integration.dll',
    testDlls:       'solution/Tests/Core/*/bin/Debug/*.Tests.dll',
    nugetConfig:    'solution/NuGet.config',
    basecsproj:     'solution/WellFired.Guacamole/WellFired.Guacamole.csproj',
    unityEditor:    'solution/WellFired.Guacamole.Unity.Editor/WellFired.Guacamole.Unity.Editor.csproj',
    unityRuntime:   'solution/WellFired.Guacamole.Unity.Runtime/WellFired.Guacamole.Unity.Runtime.csproj'
}

module.exports = {
    config: -> return config

    wtask: ->
        t = task.apply(global, arguments)

        t.addListener 'start', ->
            WellFired.logStart(this);

        return t
}