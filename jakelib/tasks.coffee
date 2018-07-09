require('../config')

module.exports = {
    config: -> return config

    wtask: ->
        t = task.apply(global, arguments)

        t.addListener 'start', ->
            WellFired.logStart(this);

        return t
}