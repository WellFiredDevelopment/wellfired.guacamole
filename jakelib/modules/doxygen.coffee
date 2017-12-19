EventEmitter = require('events').EventEmitter
spawn = require('child_process').spawn


class doxygen

    emitter = new EventEmitter

    constructor: (@doxyconfig) ->
    
    generate: ->
        args = [@doxyconfig]

        opts = { stdio: 'inherit' }

        # Using spawn as it allows us to retain the coloured output from nunit.
        # I couldn't get execfile doesn't retain colour info
        exec = spawn 'doxygen', args, opts

        exec.on 'close', (code) ->

            if code == 0
                emitter.emit 'success'
            else
                emitter.emit 'error'

            emitter.emit 'done'


    on: (name, cb) ->
        emitter.on name, cb

module.exports = doxygen