EventEmitter = require('events').EventEmitter
execFile     = require('child_process').execFile
jetpack      = require('fs-jetpack')


class shell

    emitter = new EventEmitter

    constructor: (@path, @args, @cwd) ->

    run: ->
        opts = {
          cwd:       @cwd,
          timeout:   0,
          maxBuffer: 1024 * 1024,  # we were exceeding the default buffer size
        }

        exec = execFile @path, @args, opts, (error, stdout, stderr) ->
            if error?
                emitter.emit 'error', error
            else
                emitter.emit 'success', stdout

            emitter.emit 'done', error, stdout, stderr

        exec.stdout.on 'data', (data) ->
            emitter.emit 'data', data

    on: (name, cb) ->
        emitter.on name, cb

module.exports = shell
