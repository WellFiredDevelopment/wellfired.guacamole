writerutil      = require('./../writerutil')

class ClassWriter
    constructor: () ->

    write: (data, cb) ->
        writerutil.buildHeader data, cb
        writerutil.buildDescription data, cb
        this.buildMemberData data, cb
        this.buildDetailedMemberData data, cb

    buildMemberData: (data, cb) ->
        if !data.sections?  
            return ""

        (this.buildMemberSectionTable section, cb for section in data.sections)

    buildDetailedMemberData: (data, cb) ->
        if !data.sections? 
            return ""
        
        if data.sections.length == 0
            return ""

        cb.writeLine "Breakdown"
        cb.writeLine "#{writerutil.buildUnderlineFor "Breakdown", '-'}", 2

        (this.buildDetailedSection section, cb for section in data.sections)

    buildMemberSectionTable: (section, cb) ->
        methodType = writerutil.memberTypeToReadable section.kind
        
        returnType = (a, b) -> return a.return.length - b.return.length
        name = (a, b) -> return a.length - b.name
        section.members.sort (a, b) -> writerutil.sortByMutiple a, b, [ returnType, name ]

        _this = this
        extract = (member) ->
            return [ (writerutil.buildStringFor member.return), ":ref:`#{member.name}<#{member.id}>` #{_this.buildArgumentListString member}" ]

        memberTable = (extract member for member in  section.members)

        if(memberTable.length == 0)
            return ''

        cb.writeLine "#{methodType}"
        cb.writeLine "#{writerutil.buildUnderlineFor methodType, '-'}", 2
        writerutil.writeTable memberTable, cb

    buildDetailedSection: (section, cb) ->

        returnType = (a, b) -> return a.return.length - b.return.length
        name = (a, b) -> return a.length - b.name
        section.members.sort (a, b) -> writerutil.sortByMutiple a, b, [ returnType, name ]

        (this.buildDetailedMember member, cb for member in section.members)

    buildDetailedMember: (member, cb) ->
        cb.writeLine ".. _#{member.id}:", 2
        cb.writeLine "- #{writerutil.buildStringFor member.return} **#{member.name}** #{this.buildArgumentListString member}", 2

        cb.increaseIndent()
        this.writeDescription member, cb
        this.writeParams member, cb
        this.writeExceptions member, cb
        cb.decreaseIndent()
    
    writeDescription: (member, cb) ->
        descriptionString = "#{writerutil.buildStringFor member.briefDescription}"
        if descriptionString.length == 0
            return

        cb.writeLine "**Description**", 2
        cb.increaseIndent()
        cb.writeLine "#{descriptionString}", 2
        cb.decreaseIndent()
    
    writeParams: (member, cb) ->
        if member.detailedParameterList.length == 0
            return;

        cb.writeLine "**Parameters**", 2
        cb.increaseIndent()
        writerutil.writeTable (writerutil.buildParamArray member.detailedParameterList), cb
        cb.decreaseIndent()
    
    writeExceptions: (member, cb) ->
        if member.detailedExceptionList.length == 0
            return;
            
        cb.writeLine "**Exception**", 2
        cb.increaseIndent()
        writerutil.writeTable (writerutil.buildParamArray member.detailedExceptionList), cb
        cb.decreaseIndent()

    buildArgumentList: (member) ->
        _this = this
        buildArgument = (argument) ->
            if argument.defaultValue.length > 0
                return "#{writerutil.buildStringFor argument.type} #{writerutil.buildStringFor argument.name} = #{writerutil.buildStringFor argument.defaultValue}"

            return "#{writerutil.buildStringFor argument.type} #{writerutil.buildStringFor argument.name}"

        argumentsString = (buildArgument briefArgument for briefArgument in member.briefArguments).join(', ')

    buildArgumentListString: (member) ->
        if member.kind == 'event' || member.kind == 'variable'
            return '';
        if member.kind == 'function'
            return "**(** #{this.buildArgumentList member} **)**"
        if member.kind == 'property'
            return "**{** get; set; **}**"

        throw 'Unhandled Arg Type'

module.exports = ClassWriter