require('string-format')
require('./config')

jake.addListener 'start', ->
    jake.logger.log('\n{0} jake file starting ...'.format(config.name))

jake.addListener 'complete', ->
    jake.logger.log('{0} Done.\n'.format(config.name))
