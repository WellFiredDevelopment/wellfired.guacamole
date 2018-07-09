nugetRestore = require('./modules/nugetrestore')
nugetProject = require('./modules/nugetproject')
wtask = require('./tasks').wtask


namespace 'nuget', ->
    
    desc 'Restores all nuget packages in the provided solution'
    wtask 'restore', { async: true }, (c) ->

        slnPath = config.slnPath || "solution/#{config.name}.sln"
        nugetConfig = config.nugetConfig || 'solution/NuGet.config'

        runner = new nugetRestore slnPath, nugetConfig

        runner.on 'data', (data) ->
            WellFired.info data

        runner.on 'error', (error) ->
            WellFired.error error

        runner.on 'success', (stdout) ->
            complete()

        runner.restore()

    namespace 'pack', ->
        desc 'Packs all csproj into nuget packages'
        wtask 'all', { async: true }, ->
            WellFired.call  'nuget:pack:base',
                            'nuget:pack:unityruntime', 
                            'nuget:pack:unityeditor'


        desc 'Packs the base assembly into a nuget package'
        wtask 'base', {async: true}, (c) ->
            basecsproj = config.basecsproj || "solution/#{config.name}/#{config.name}.csproj"

            runner = new nugetProject basecsproj

            runner.on 'data', (data) ->
                WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack() 


        desc 'Packs the base assembly into a nuget package'
        wtask 'unityeditor', {async: true}, (c) ->
            unityEditor = config.unityEditor || "solution/#{config.name}.Unity.Editor/#{config.name}.Unity.Editor.csproj"
            
            runner = new nugetProject unityEditor

            runner.on 'data', (data) ->
                WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack()   


        desc 'Packs the base assembly into a nuget package'
        wtask 'unityruntime', {async: true}, (c) ->
            unityRuntime = config.unityRuntime || "solution/#{config.name}.Unity.Runtime/#{config.name}.Unity.Runtime.csproj"

            runner = new nugetProject unityRuntime

            runner.on 'data', (data) ->
                 WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack()       