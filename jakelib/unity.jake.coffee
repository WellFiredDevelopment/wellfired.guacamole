dgram  = require('dgram');
utils = require('./modules/utils')


namespace 'unity', ->

    desc 'Upgrades the Unity dependencies. Requires version argument, eg. [5.3.1f1]'
    yask 'upgrade', (ver) ->

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
    yask 'switch-dev-platform', (platform) ->

        if !platform?
            WellFired.error('Requires platform to be provided, eg. [windows], [macos]')
            return

        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEditor.dll",        'WellFired.Platformer.Solution/Assemblies/UnityEditor.dll',         { clean: true }
        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEngine.dll",        'WellFired.Platformer.Solution/Assemblies/UnityEngine.dll',         { clean: true }
        utils.copy "WellFired.Platformer.Solution/Assemblies/Unity/#{platform}/UnityEngine.UI.dll",     'WellFired.Platformer.Solution/Assemblies/UnityEngine.UI.dll',      { clean: true }


    namespace 'editor', ->

        desc 'Sends the BUILD command to the Unity editor. Requires platform and config, eg. platform=macos config=release'
        yask 'build', { async: true }, ->

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
        yask 'profile-sample', { async: true },  ->
            sendCommand('profile-sample')

        desc 'Sends the PLAY command to the Unity editor'
        yask 'play', { async: true },  ->
            sendCommand('play')
        

        desc 'Sends the REFRESH command to the Unity editor and waits for it to finish'
        yask 'refresh', { async: true },  ->
            sendSyncCommand('refresh', complete)


        desc 'Sends the REFRESH-PLAY command to the Unity editor'
        yask 'refresh-play', { async: true },  ->
            sendCommand('refresh-play')
        

        desc 'Sends the STOP command to the Unity editor'
        yask 'stop', { async: true },  ->
            sendCommand('stop')


        desc 'Sends the TOGGLE-PAUSE command to the Unity editor'
        yask 'toggle-pause', { async: true },  ->
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

      