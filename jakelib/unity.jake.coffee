dgram  = require('dgram');
utils = require('./modules/utils')
unitypackage = require('./modules/unitypackage')
stringformat = require('string-format')
wtask = require('./tasks').wtask


namespace 'unity', ->

    desc 'Upgrades the Unity dependencies. Requires version argument, eg. [5.3.1f1]'
    wtask 'upgrade', (ver) ->

        if !ver?
            WellFired.error('Requires version to be provided, eg. [5.3.1f1]')
            return

        utils.copy "/Applications/Unity#{ver}/Unity#{ver}.app/Contents/Managed/UnityEditor.dll",                                "WellFired.Platformer.Solution/Assemblies/Unity/UnityEditor.dll",         { clean: true }
        utils.copy "/Applications/Unity#{ver}/Unity#{ver}.app/Contents/Managed/UnityEngine.dll",                                "WellFired.Platformer.Solution/Assemblies/Unity/UnityEngine.dll",         { clean: true }
        utils.copy "/Applications/Unity#{ver}/Unity#{ver}.app/Contents/UnityExtensions/Unity/GUISystem/UnityEngine.UI.dll",     "WellFired.Platformer.Solution/Assemblies/Unity/UnityEngine.UI.dll",      { clean: true }

        files = utils.find 'WellFired.Platformer.Solution', { matching: 'AssemblyInfo.cs' }

        for f in files
            do (f) ->
                utils.replace f, /UnityAPICompatibilityVersion\(.+?\)/, "UnityAPICompatibilityVersion(\"#{ver}\")"


    desc 'Switches provided Unity binaries between platform specific Unity binaries. Requires platform argument, eg. [windows], [macos]'
    wtask 'switch-dev-platform', (platform) ->

        if !platform?
            WellFired.error('Requires platform to be provided, eg. [windows], [macos]')
            return

        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEditor.dll",        'WellFired.Platformer.Solution/Assemblies/UnityEditor.dll',         { clean: true }
        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEngine.dll",        'WellFired.Platformer.Solution/Assemblies/UnityEngine.dll',         { clean: true }
        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEngine.UI.dll",     'WellFired.Platformer.Solution/Assemblies/UnityEngine.UI.dll',      { clean: true }

    desc 'Move shared libraries to WellFired/Shared.'
    wtask 'processSharedTools', { async: true }, ->

        unityAssets = config.unityAssets || 'unity/Assets'
        projectName = config.name
        sharedTools = config.sharedTools || []

        if sharedTools.length == 0
            WellFired.info 'No shared tools to process'
        else
            cleanerLibs = utils.find("#{ unityAssets }/WellFired/#{ projectName }", { matching: ["**/WellFired.Broom*"] })
            utils.move "#{file}", "#{ unityAssets }/WellFired/WellFired.Shared/Editor/#{utils.getFileName(file)}" for file in cleanerLibs

            files = utils.find("#{ unityAssets }/WellFired/#{ projectName }", { matching: sharedTools })

            runtimeFiles = files.filter((file) -> !file.includes("/Editor/"))
            utils.move "#{file}", "#{ unityAssets }/WellFired/WellFired.Shared/#{utils.getFileName(file)}" for file in runtimeFiles

            editorFiles = files.filter((file) -> file.includes("/Editor/"))
            utils.move "#{file}", "#{ unityAssets }/WellFired/WellFired.Shared/Editor/#{utils.getFileName(file)}" for file in editorFiles
    
        complete()


    desc 'Move changelog.txt to the unity project'
    wtask 'updateChangelog', { async: true }, ->
        changelogFile = config.changelog || 'changelog.txt'
        unityAssets = config.unityAssets || 'unity/Assets'

        utils.copy changelogFile, "#{unityAssets}/WellFired/#{config.name}/#{utils.getFileName(changelogFile)}", {clean: true}

    namespace 'package', ->

        desc 'Extracts the given .unitypackage to the given directory. Requires an inFile and an outDir, eg. [a/file.unitypackage,out]'
        wtask 'extract', { async: true }, (inFile, outDir) ->
            uPackage = new unitypackage

            uPackage.on 'data', (data) ->
                WellFired.info data

            uPackage.on 'error', (data) ->
                WellFired.error data

            uPackage.on 'success', (stdout) ->
                complete()

            uPackage.extract inFile, outDir

        desc 'Builds the given directory into a .unitypackage. Requires an inDir and an outFile, eg [in,a/file/unitypackage]'
        wtask 'build', { async: true }, (inDir, outFile) ->
            uPackage = new unitypackage

            uPackage.on 'data', (data) ->
                WellFired.info data

            uPackage.on 'error', (data) ->
                WellFired.error data

            uPackage.on 'success', (stdout) ->
                complete()

            uPackage.build inDir, outFile

    namespace 'editor', ->

        desc 'Sends the BUILD command to the Unity editor. Requires platform and config, eg. platform=macos config=release'
        wtask 'build', { async: true }, ->

            config   = process.env.config || 'release'
            mode     = process.env.mode
            platform = process.env.platform

            if !WellFired.validatePlatform()
               process.exit()
               return

            switch platform.toLowerCase()
                when 'macos'        then sendSyncCommand('build-macos {0} {1}'.format(config, mode), complete)
                when 'windows'      then sendSyncCommand('build-windows {0} {1}'.format(config, mode), complete)
                else WellFired.failF('Unrecognised platform {0}. Package aborted.', process.env.platform)


        desc 'Sends the PROFILER SAMPLE command to the Unity runtime'
        wtask 'profile-sample', { async: true },  ->
            sendCommand('profile-sample')

        desc 'Sends the PLAY command to the Unity editor'
        wtask 'play', { async: true },  ->
            sendCommand('play')


        desc 'Sends the REFRESH command to the Unity editor and waits for it to finish'
        wtask 'refresh', { async: true },  ->
            sendSyncCommand('refresh', complete)


        desc 'Sends the REFRESH-PLAY command to the Unity editor'
        wtask 'refresh-play', { async: true },  ->
            sendCommand('refresh-play')


        desc 'Sends the STOP command to the Unity editor'
        wtask 'stop', { async: true },  ->
            sendCommand('stop')


        desc 'Sends the TOGGLE-PAUSE command to the Unity editor'
        wtask 'toggle-pause', { async: true },  ->
            sendCommand('toggle-pause')


        sendCommand = (cmd) ->
            host = '127.0.0.1'
            port = 9050

            buffer = new Buffer(cmd)
            client = dgram.createSocket('udp4')

            client.bind()

            client.on 'listening', ->
                client.send buffer, 0, cmd.length, port, host, (err, bytes) ->
                    if err? && err != 0
                        WellFired.error err
                        return

                    WellFired.info 'UDP command sent to ' + host + ':' + port

                    client.close()
                    complete()

        sendSyncCommand = (cmd, oc) ->
            host = '127.0.0.1'
            port = 9050

            buffer = new Buffer(cmd)
            client = dgram.createSocket('udp4')

            client.bind(9060)

            client.on 'listening', ->
                client.send buffer, 0, cmd.length, port, host, (err, bytes) ->
                    if err? && err != 0
                        WellFired.error err
                        return

                    WellFired.info 'UDP command sent to ' + host + ':' + port

                client.on 'message', (msg, rinfo) ->
                    if msg.toString() == 'UNITY-DONE'
                        client.close()
                        oc()
                    else
                        WellFired.error 'Received unknown UDP msg: [{0}] from {1}:{2}'.format(msg, rinfo.address, rinfo.port)

      