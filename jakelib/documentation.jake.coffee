doxygen = require('./modules/doxygen')
doxygen2sphinx = require('./modules/doxygen2sphinx/doxygen2sphinx')
wtask = require('./tasks').wtask


namespace 'documentation', ->
    
    desc 'Generates our API documentation using doxygen'
    wtask 'generate', { async: true }, (c) ->
        doxyConf = config.doxyConfig || 'documentation/doxyconf'
        runner = new doxygen doxyConf

        runner.on 'data', (data) ->
            WellFired.info data

        runner.on 'error', (error) ->
            WellFired.error error

        runner.on 'success', (stdout) ->
            complete()

        runner.generate()


    desc 'Converts our API documentation from doxygen format into a format that sphinx understands'
    wtask 'convert', { async: true }, (c) ->
        sphinxInputDir = config.sphinxInputDir || 'documentation/xml'
        sphinxOutputDir = config.sphinxOutputDir || 'documentation/sphinx'
        sphinxProjectName = config.sphinxProjectName || 'Invalid'

        runner = new doxygen2sphinx sphinxInputDir, sphinxOutputDir, sphinxProjectName

        runner.on 'data', (data) ->
            WellFired.info data

        runner.on 'error', (error) ->
            WellFired.error error

        runner.on 'success', (stdout) ->
            complete()

        runner.convert()
