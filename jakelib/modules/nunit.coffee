EventEmitter = require('events').EventEmitter
spawn = require('child_process').spawn

class nunit

    emitter = new EventEmitter

    constructor: (@path, @where) ->

    run: ->

        args = [@path]

        if @where?
            args.push @where

        opts = { stdio: 'inherit' }

        # Using spawn as it allows us to retain the coloured output from nunit.
        # I couldn't get execfile to retain the colour, which is pretty bad for test results.
        exec = spawn 'External/nunit_framework4.5_3.4.1/nunit-console.sh', args, opts

        exec.on 'close', (code) ->

            if code == 0
                emitter.emit 'success'
            else
                emitter.emit 'fail'

            emitter.emit 'done'

    on: (name, cb) ->
        emitter.on name, cb

module.exports = nunit