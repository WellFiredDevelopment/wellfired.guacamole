module.exports = {
    dataTypeToPath: (type) ->
        if type == 'class' then return 'classes'
        if type == 'interface' then return 'interfaces'
        if type == 'namespace' then return 'namespaces'
        if type == 'enum' then return 'enums'

    memberTypeToReadable: (type) ->
        if type == 'public-static-func' then return 'Public Static Methods'
        if type == 'public-func' then return 'Public Methods'
        if type == 'public-attrib' then return 'Public Properties'
        if type == 'property' then return 'Properties'
        if type == 'event' then return 'Events'
        return type

    # Build a files header
    buildHeader: (data, cb) ->
        cb.writeLine ".. _#{data.type}#{data.id}:", 2
        cb.writeLine "#{data.readableName}"
        cb.writeLine "#{this.buildUnderlineFor data.readableName, '='}", 2
        this.buildNamespace data, cb
        this.buildImplements data, cb
        this.buildInherits data, cb

    buildNamespace: (data, cb) ->
        if !data.namespace?
            return;

        cb.writeLine "**Namespace:** :ref:`#{data.namespace.elements}<#{data.namespace.refId}>`", 2

    buildImplements: (data, cb) ->
        if !data.implements? || data.implements.length == 0
            return;
            
        cb.writeLine "**Implements:**#{" :ref:`#{implement.elements}<#{implement.refId}>`" for implement in data.implements}\n\n"

    buildInherits: (data, cb) ->
        if !data.inherits? || data.inherits.length == 0
            return;
            
        cb.writeLine "**Inherits:**#{" :ref:`#{inherit.elements}<#{inherit.refId}>`" for inherit in data.inherits}\n\n"

    # Build a files description
    buildDescription: (data, cb) ->
        cb.writeLine "Description"
        cb.writeLine "#{this.buildUnderlineFor 'Description', '-'}", 2
        cb.writeLine "#{this.buildStringFor data.briefDescription}", 2

    # Build a usable string from a nodes elements
    buildStringFor: (elements) ->
        return (this.toString element for element in elements).join ''
  
    writeTable: (table, cb) ->
        if(table.length == 0)
            return

        width = table[0].length - 1 
        height = table.length - 1
        
        getLargestRow = (column) ->
            _column = column
            maxSize = (table.reduce ((max, arr) -> 
                length = if arr[_column]? then arr[_column].length else 1
                Math.max max, length), -Infinity)
            if maxSize <= 10 then return 15 else return maxSize + 5

        rowsData = (getLargestRow column for column in [0..width])

        printLineContents = (row, column) ->
            printLine = (row, column, size) ->
                totalSize = size - 2
                if column == 0
                    cb.write "+"
                cb.write '-' for count in [1..totalSize]
                cb.write "+"

            printLine row, column, rowsData[column]
        
        printRowContents = (row, column) ->
            printLine = (row, column, size) ->
                content = if table[row][column]? then table[row][column] else ''
                totalSize = size - content.length - 2
                if column == 0
                    cb.write "|"
                cb.write content
                cb.write ' ' for count in [0..totalSize - 1]
                cb.write "|"

            printLine row, column, rowsData[column]

        buildLine = (row) ->
            cb.beginLine()
            printLineContents row, column for column in [0..width]
            cb.write '\n'

        buildContents = (row) ->
            cb.beginLine()
            printRowContents row, column for column in [0..width]
            cb.write '\n'

        buildRow = (row) ->
            if(row == 0)
                buildLine row
            buildContents row
            buildLine row

        buildRow row for row in [0..table.length - 1]
        cb.writeLine()

    toString: (data) ->
        if data == undefined
            return '';

        if data.type == 'text'
            return data.elements
        if data.type == 'para'
            return this.buildStringFor data.elements
        if data.type == 'ref'
            return ":ref:`#{data.elements}<#{data.refId}>`"

    # This method will create an underline for the passed value with the passed underlineCharacter
    # This method will go one farther to ensure we have enough underlineCharacter
    buildUnderlineFor: (value, underlineCharacter) ->
        count = value.length
        data = ''
        while count >= 0
            data += underlineCharacter
            count--
        return data

    # Takes an array of parameters, such as [ {name: 'a', description: 'b'}, {name: 'c', description: 'd'} ]
    # and builds a 2d array that can be used to generate a table, such as [ [ 'a', 'b' ] [ 'c', 'd' ] ]
    buildParamArray: (paramList) ->
        _this = this;
        buildParam = (param, haveDesciption) -> 
            if haveDesciption
                return ["#{_this.buildStringFor param.name}", "#{_this.buildStringFor param.description}"]
            else
                return ["#{_this.buildStringFor param.name}"]

        return (buildParam param, paramList.some((x) -> (_this.buildStringFor x.description).length > 0) for param in paramList)

    # Allows for the sorting of an array with multiple functions
    sortByMutiple: (a, b, funcs) ->
        sortBy = (func, a, b, r) ->
            r = if r then 1 else -1
            return -1 * r if (func a, b) < 1
            return +1 * r if (func a, b) > 1
            return 0
        
        return r if (r = sortBy func, a, b) for func in funcs
}