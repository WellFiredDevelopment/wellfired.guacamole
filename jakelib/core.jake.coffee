xbuild = require('./modules/xbuild')
utils = require('./modules/utils')
globals = require('../globals')
wtask = require('../globals').wtask


namespace 'core', ->     

    desc 'Builds the .NET assemblies. [d|debug]'
    wtask 'build', { async: true }, (c) ->
        build(c, 'build');
    
    desc 'Rebuilds the .NET assemblies. [d|debug]'
    wtask 'rebuild', { async: true }, (c) ->
        build(c, 'rebuild')


    build = (c, t) ->

        config = utils.getConfig(c, 'debug', 'release')
        target = if t == 'r' or t == 'rebuild' then 'Rebuild' else 'Build'

        xb = new xbuild globals.config().slnPath, target, config

        xb.on 'data', (data) ->
            WellFired.info data

        xb.on 'error', (error) ->
            WellFired.error error

        xb.on 'success', (stdout) ->
            complete()

        xb.run()
