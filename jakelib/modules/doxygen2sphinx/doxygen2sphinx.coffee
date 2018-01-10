EventEmitter    = require('events').EventEmitter
spawn           = require('child_process').spawn
utils           = require('./../utils')
reader          = require('./reader')
writer          = require('./writer')

class doxygen2sphinx

    emitter = new EventEmitter

    constructor: (@sphinxInDir, @sphinxOutDir, @projectName) ->
    
    convert: ->
        _outputDir = @sphinxOutDir
        _projectName = @projectName
        utils.delete "#{_outputDir}/api"

        doxyReader = new reader @sphinxInDir
        doxyReader.read()
        .then (results) ->
            sphinxWriter = new writer _outputDir, results, _projectName
            sphinxWriter.write()
        .catch (error) ->
            emitter.emit 'error', error
        .then (results) ->
            emitter.emit 'data', 'done'

    on: (name, cb) ->
        emitter.on name, cb

module.exports = doxygen2sphinx