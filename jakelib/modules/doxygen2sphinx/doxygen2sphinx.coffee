EventEmitter    = require('events').EventEmitter
spawn           = require('child_process').spawn
utils           = require('./../utils')
reader          = require('./reader')
writer          = require('./writer')

class doxygen2sphinx

    emitter = new EventEmitter

    constructor: (@sphinxInDir, @sphinxOutDir) ->
    
    convert: ->
        _outputDir = @sphinxOutDir
        utils.delete "#{_outputDir}/api"

        doxyReader = new reader @sphinxInDir
        doxyReader.read()
        .then (results) ->
            sphinxWriter = new writer _outputDir, results, '.Profile'
            sphinxWriter.write()
        .catch (error) ->
            emitter.emit 'error', error
        .then (results) ->
            emitter.emit 'data', 'done'

    on: (name, cb) ->
        emitter.on name, cb

module.exports = doxygen2sphinx