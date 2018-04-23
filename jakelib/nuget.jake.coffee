nugetRestore = require('./modules/nugetrestore')
nugetProject = require('./modules/nugetproject')
globals = require('../globals')
wtask = require('../globals').wtask

namespace 'nuget', ->
    
    desc 'Restores all nuget packages in the provided solution'
    wtask 'restore', { async: true }, (c) ->
        runner = new nugetRestore globals.config().slnPath, globals.config().nugetConfig

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
            runner = new nugetProject globals.config().basecsproj

            runner.on 'data', (data) ->
                WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack() 


        desc 'Packs the base assembly into a nuget package'
        wtask 'unityeditor', {async: true}, (c) ->
            runner = new nugetProject globals.config().unityEditor

            runner.on 'data', (data) ->
                WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack()   


        desc 'Packs the base assembly into a nuget package'
        wtask 'unityruntime', {async: true}, (c) ->
            runner = new nugetProject globals.config().unityRuntime

            runner.on 'data', (data) ->
                 WellFired.info data

            runner.on 'error', (error) ->
                WellFired.error error

            runner.on 'success', (stdout) ->
                complete()

            runner.pack()       