EventEmitter = require('events').EventEmitter
spawn = require('child_process').spawn

class nunit

    emitter = new EventEmitter

    constructor: (@path, @where) ->

    run: ->

        args = ['--debug', 'solution/packages/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe']

        @path.map((path) ->
         args.push path
        )

        if @where?
            args.push @where

        opts = { stdio: 'inherit' }

        # Using spawn as it allows us to retain the coloured output from nunit.
        # I couldn't get execfile doesn't retain colour info
        exec = spawn 'mono', args, opts

        exec.on 'close', (code) ->

            if code == 0
                emitter.emit 'success'
            else
                emitter.emit 'fail'

            emitter.emit 'done'

    on: (name, cb) ->
        emitter.on name, cb

module.exports = nunit