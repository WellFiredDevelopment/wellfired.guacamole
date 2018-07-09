EventEmitter  = require('events').EventEmitter
AWS           = require('aws-sdk')
utils         = require('./utils')

class s3

    emitter = new EventEmitter

    constructor: (@bucket, @key, @filename) ->

    push: ->

        emitter.emit 'data', 'Reading: ' + @filename
        emitter.emit 'data', 'Deploying to bucket: ' + @bucket
        emitter.emit 'data', 'Deploying with key: ' + @key

        params = {
            Bucket: @bucket,
            Key: @key
        }

        utils.readAsync @filename
        .then (data) ->
            s3 = new AWS.S3 { region: 'eu-west-2' }
            params.Body = new Buffer data, 'binary'
            return s3.putObject(params).promise()
        .then () ->
            emitter.emit 'success'
            emitter.emit 'done'
        .catch (error) ->
            emitter.emit 'error', error

    on: (name, cb) ->
        emitter.on name, cb

module.exports = s3
