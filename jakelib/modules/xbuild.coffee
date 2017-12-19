EventEmitter = require('events').EventEmitter
execFile     = require('child_process').execFile
jetpack      = require('fs-jetpack')
stringformat = require('string-format')

class xbuild

    emitter = new EventEmitter

    constructor: (@path, @target, @configuration) ->

    run: ->
        # The environment variable platform is read by xbuild so we must clear it in case it has been set
        # (we often set platform in our build scripts - this will break xbuild)
        platform = process.env.platform
        delete process.env.platform
        
        args = [ '/p:Configuration={0}'.format(@configuration), 
                 '/t:{0}'.format(@target), 
                 # note: to include condition compilation constants follow the format below.
                 # also: if you define a constant then DEBUG is no longer defined and you will need to redefine.
                 # '/p:DefineConstants=abc def DEBUG',
                 @path]

        opts = {
          timeout:   0,
          maxBuffer: 1024 * 1024,  # we were exceeding the default buffer size
        }

        exec = execFile 'xbuild', args, opts, (error, stdout, stderr) ->
            process.env.platform = platform
            
            if error?
                emitter.emit 'error', error
            else
                emitter.emit 'success', stdout

            emitter.emit 'done', error, stdout, stderr

        exec.stdout.on 'data', (data) ->
            emitter.emit 'data', data

    on: (name, cb) ->
        emitter.on name, cb


module.exports = xbuild
