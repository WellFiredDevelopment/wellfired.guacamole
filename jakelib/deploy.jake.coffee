s3 = require('./modules/s3')
utils = require('./modules/utils')
wtask = require('./tasks').wtask


namespace 'deploy', ->

    desc 'Deploys the passed file to s3. [filename,version], e.g. jake deploy:s3[a-local.file,2018.1.0]'
    wtask 's3', { async: true }, (filename, ver) ->
        if !filename?
            WellFired.error 'Requires a local filename to be provided, eg. [a-local.file]'
            return

        if !ver?
            WellFired.error 'Requires version to be provided, eg. [2018.1.0]'
            return

        key = config.name + '.' + ver + '.' + utils.fileExtension(filename)
        s3 = new s3 config.deployBucket, key, filename

        s3.on 'data', (data) ->
            WellFired.info data

        s3.on 'error', (error) ->
            WellFired.error error

        s3.on 'success', () ->
            complete()

        s3.push()