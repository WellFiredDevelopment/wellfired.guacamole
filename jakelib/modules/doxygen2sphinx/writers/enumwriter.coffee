writerutil      = require('./../writerutil')

class EnumWriter
    constructor: () ->
    write: (data, cb) ->
        writerutil.buildHeader data, cb
        writerutil.buildDescription data, cb
        writerutil.writeTable (writerutil.buildParamArray data.enumMembers), cb

module.exports = EnumWriter