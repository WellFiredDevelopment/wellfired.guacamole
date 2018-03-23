utils = require('./modules/utils')
wtask = require('./modules/globals').wtask

namespace 'guacamole', ->     

    matchingFileToMove =
        [
            "**/WellFired.Guacamole.*"
            "!**/WellFired.Guacamole.Examples.*"
        ]

    codeLocation = 'unity/Assets/Code/Editor'
    defaultDestination = '../wellfired.peek/unity/Assets/WellFired/WellFired.Shared/Editor'

    desc 'Copy guacamole libraries to the specified path'
    wtask 'copy', { async: true }, (dest) ->
        copy(dest)

    copy = (dest) ->
        if!dest
            dest = defaultDestination

        filesToCopy = utils.find(codeLocation, { matching: matchingFileToMove})
        utils.copy "#{file}", "#{ dest }/#{utils.getFileName(file)}", {clean: true} for file in filesToCopy
        
        complete()
