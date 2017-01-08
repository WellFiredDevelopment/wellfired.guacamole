nunit = require('./modules/nunit')
utils = require('./modules/utils')

namespace 'test', ->

    desc 'Runs all tests'
    yask 'all', {async: true}, ->

        files = utils.find('', { matching: [WellFired.config.testDlls] })
        files.concat utils.find('', { matching: [WellFired.config.integrationDlls] })

        if files.length == 0
            WellFired.error('No tests to run')
            return

        run files


    desc 'Runs all unit tests'
    yask 'unit', {async: true}, ->

        files = utils.find('', { matching: [WellFired.config.testDlls] })

        if files.length == 0
            WellFired.error('No unit tests to run')
            return

        run files


    desc 'Runs all integration tests'
    yask 'integration', {async: true}, ->

        files = utils.find('', { matching: [WellFired.config.integrationDlls] })

        if files.length == 0
            WellFired.error('No Integration tests to run')
            return

        run files


    run = (p) ->
        runner = new nunit p.join(" ")

        runner.on 'done', ->
            complete()

        runner.run()