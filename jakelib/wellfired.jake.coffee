chalk           = require('chalk')
argv            = require('yargs').argv
stringformat    = require('string-format')

class WellFired

    chatty = argv.c

    config: global.config

    always: (message) ->
        @debug message
    alwaysF: (message, args) ->
        @debugF message, args

    debug: (message) ->
        jake.logger.log(chalk.white(message))
    debugF: (message, args) ->
        this.info(message.format(args))

    info: (message) ->
        jake.logger.log(chalk.white(message)) if chatty
    infoF: (message, args) ->
        this.info(message.format(args)) if chatty

    warn: (message) ->
        jake.logger.log(chalk.yellow(message))
    warnF: (message, args) ->
        this.warn(message.format(args))

    error: (message) ->
        jake.logger.error(chalk.red(message))
    errorF: (message, args) ->
        jake.logger.error(chalk.red(message.format(args)))

    fail: (message) ->
        fail('\n' + chalk.red(message)) # There is a bug in the Jake Runner just now that requires a newline on Fail, it assumes a callstack
    failF: (message, args) ->
        fail('\n' + message.format(args)) # There is a bug in the Jake Runner just now that requires a newline on Fail, it assumes a callstack


    logStart: (task) ->
        msg = chalk.white('> Starting: ') + chalk.green(task.fullName)
        
        jake.logger.log(msg)


    invoke: (name, args...) ->
        task = jake.Task[name]
        
        task.addListener('complete', complete)
        
        task.invoke.apply(task, args)


    call: (names...) ->

        run = (names, i) ->

            task = jake.Task[names[i]]
            
            task.addListener 'complete', ->
                if ++i == names.length
                    complete()
                else
                    run names, i

            task.invoke.apply(task)
        
        run names, 0


    validatePlatform: -> 
        platform = process.env.platform

        if !platform?
            @error('Requires platform to be provided, eg. platform=ios')
            return false
        
        switch platform.toLowerCase()
            when 'editor'  then return true
            else 
                @errorF('Unrecognised platform {0}.', process.env.platform)
                return false

global.WellFired = new WellFired()