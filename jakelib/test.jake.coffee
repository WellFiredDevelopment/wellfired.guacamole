nunit = require('./modules/nunit')
utils = require('./modules/utils')
wtask = require('./tasks').wtask
require('../config')


namespace 'test', ->

    desc 'Runs all tests'
    wtask 'all', {async: true}, ->

        integrationDlls = config.integrationDlls || 'solution/Tests/*/bin/Debug/*.Integration.Tests.dll'
        testDlls = config.testDlls || 'solution/Tests/*/bin/Debug/*.Unit.Tests.dll'

        files = utils.find('', { matching: [testDlls] })
        files.concat utils.find('', { matching: [integrationDlls] })

        if files.length == 0
            WellFired.error('No tests to run')
            return

        run files


    desc 'Runs all unit tests'
    wtask 'unit', {async: true}, ->

        testDlls = config.testDlls || 'solution/Tests/*/bin/Debug/*.Unit.Tests.dll'
        files = utils.find('', { matching: [testDlls] })

        if files.length == 0
            WellFired.error('No unit tests to run')
            return

        run files


    desc 'Runs all integration tests'
    wtask 'integration', {async: true}, ->

        integrationDlls = config.integrationDlls || 'solution/Tests/*/bin/Debug/*.Integration.Tests.dll'
        files = utils.find('', { matching: [integrationDlls] })

        if files.length == 0
            WellFired.error('No Integration tests to run')
            return

        run files


    run = (p) ->

        runner = new nunit p

        runner.on 'done', ->
            complete()

        runner.run()