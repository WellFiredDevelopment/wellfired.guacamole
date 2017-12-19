class ContentBuffer
    indentLevel: undefined
    buffer: ''

    increaseIndent: () ->
        this.indentLevel += 1

    decreaseIndent: () ->
        this.indentLevel -= 1

    beginLine: () ->
        this.indentLevel ?= 0
        this.buffer += '    ' for count in [0...this.indentLevel]

    write: (toWrite) ->
        this.buffer += toWrite

    writeLine: (line, newLineCount) ->
        line ?= ''
        newLineCount ?= 1

        this.beginLine()
        this.buffer += line
        this.buffer += '\n' for count in [0...newLineCount]

module.exports.ContentBuffer = ContentBuffer