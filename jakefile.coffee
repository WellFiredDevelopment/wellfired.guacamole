require('string-format')
_ = require('underscore')
jetpack = require('fs-jetpack')


global.config = {
    name:           'Gucamole',
    slnPath:        'Solution/Guacamole.sln',
    unityAssets:    'Unity/Assets',
    unityBin:       'Unity/Assets/Code',
    unityEditorBin: 'Unity/Assets/Code/Editor',
    integrationDlls:'Solution/Test/*/bin/Debug/*.Integration.dll'
    testDlls:       'Solution/Test/*/bin/Debug/*.Tests.dll'
}

global.yask = ->
    t = task.apply(global, arguments)

    t.addListener 'start', ->
        WellFired.logStart(this);

    return t

jake.addListener 'start', ->
    jake.logger.log('\n{0} jake file starting ...'.format(config.name))

jake.addListener 'complete', ->
    jake.logger.log('{0} Done.\n'.format(config.name))
