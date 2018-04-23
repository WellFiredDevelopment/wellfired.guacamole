nunit = require('./modules/nunit')
utils = require('./modules/utils')
globals = require('../globals')
wtask = require('../globals').wtask

namespace 'test', ->

    desc 'Runs all tests'
    wtask 'all', {async: true}, ->

        files = utils.find('', { matching: [globals.config().testDlls] })
        files.concat utils.find('', { matching: [globals.config().integrationDlls] })

        if files.length == 0
            WellFired.error('No tests to run')
            return

        run files


    desc 'Runs all unit tests'
    wtask 'unit', {async: true}, ->

        files = utils.find('', { matching: [globals.config().testDlls] })

        if files.length == 0
            WellFired.error('No unit tests to run')
            return

        run files


    desc 'Runs all integration tests'
    wtask 'integration', {async: true}, ->

        files = utils.find('', { matching: [globals.config().integrationDlls] })

        if files.length == 0
            WellFired.error('No Integration tests to run')
            return

        run files


    run = (p) ->
        runner = new nunit p.join(" ")

        runner.on 'done', ->
            complete()

        runner.run()