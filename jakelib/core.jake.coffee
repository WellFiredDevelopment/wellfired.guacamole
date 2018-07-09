xbuild = require('./modules/xbuild')
utils = require('./modules/utils')
wtask = require('./tasks').wtask


namespace 'core', ->     

    desc 'Builds the .NET assemblies. [d|debug]'
    wtask 'build', { async: true }, (c) ->
        build(c, 'build');
    
    desc 'Rebuilds the .NET assemblies. [d|debug]'
    wtask 'rebuild', { async: true }, (c) ->
        build(c, 'rebuild')


    build = (c, t) ->
        buildConfig = utils.getConfig(c, 'debug', 'release')
        target = if t == 'r' or t == 'rebuild' then 'Rebuild' else 'Build'

        slnPath = config.slnPath || "solution/#{config.name}.sln"

        xb = new xbuild slnPath, target, buildConfig

        xb.on 'data', (data) ->
            WellFired.info data

        xb.on 'error', (error) ->
            WellFired.error error

        xb.on 'success', (stdout) ->
            complete()

        xb.run()

    desc 'Update version number, eg. [2018.1.0]'
    wtask 'updateversion', { async: true }, (ver) ->
        if !ver?
            WellFired.error 'Requires version to be provided, eg. [2018.1.0]'
            return

        UpdateChangeLog ver
        UpdateTCSetting ver

        complete()

    UpdateChangeLog = (version) ->
        regex = /\nVersion ([0-9]+\.[0-9]+\.[0-9]+(?:[a-zA-Z-+][a-zA-Z0-9-\.:]*)?)\n/
        logFileLocation = config.changelog || 'changelog.txt'
        logFile = utils.read logFileLocation
        currentVersion = regex.exec(logFile);

        if version != currentVersion[1]
            logFile = logFile.slice(0, currentVersion.index) + "\nVersion #{version}\n\n##### Tell the world what changed in this new version ! #####\n\n" +
            logFile.slice(currentVersion.index + 1)
            utils.write(logFileLocation, logFile)
        else
            WellFired.fail 'Version number is the same'

    UpdateTCSetting = (version) ->
        tcConfigFileLocation = config.tcConfigFile || '.teamcity/settings.kts'
        tcConfigFile = utils.read tcConfigFileLocation
        tcConfigFile = tcConfigFile.replace(/param\("AssemblyVersion", ".*"\)/g, "param(\"AssemblyVersion\", \"#{ version}\")")
        utils.write(tcConfigFileLocation, tcConfigFile)
