global.config = {
    name:               'WellFired.Guacamole',
    sphinxProjectName:  'dotGuacamole',
    integrationDlls:    'solution/Tests/Core/*/bin/Debug/*.Integration.dll',
    testDlls:           'solution/Tests/Core/*/bin/Debug/*.Unit.dll',
    sharedTools:    [
                        '**/AsyncBridge.Net35.*'
                        '**/System.Threading.*'
                        "**/WellFired.Json.Unity*"
                    ]
}