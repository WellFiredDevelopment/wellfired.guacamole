doxygen = require('./modules/doxygen')
doxygen2sphinx = require('./modules/doxygen2sphinx/doxygen2sphinx')
globals = require('../globals')
wtask = require('../globals').wtask

namespace 'documentation', ->
    
    desc 'Generates our API documentation using doxygen'
    wtask 'generate', { async: true }, (c) ->
        runner = new doxygen globals.config().doxyConfig

        runner.on 'data', (data) ->
            WellFired.info data

        runner.on 'error', (error) ->
            WellFired.error error

        runner.on 'success', (stdout) ->
            complete()

        runner.generate()


    desc 'Converts our API documentation from doxygen format into a format that sphinx understands'
    wtask 'convert', { async: true }, (c) ->
        runner = new doxygen2sphinx globals.config().sphinxInputDir, globals.config().sphinxOutputDir, globals.config().sphinxProjectName

        runner.on 'data', (data) ->
            WellFired.info data

        runner.on 'error', (error) ->
            WellFired.error error

        runner.on 'success', (stdout) ->
            complete()

        runner.convert()
