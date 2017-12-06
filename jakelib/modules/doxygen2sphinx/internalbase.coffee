xmlhelper       = require('./xmlhelper')
util            = require('util')

class Internal
    xmlHelper: undefined
    rootNode: undefined
    id: ''
    readableName: ''
    name: ''
    briefDescription: ''
    detailedDescription: ''

    constructor: (@xmlData) ->
        this.xmlHelper = new xmlhelper @xmlData
        this.rootNode = this.xmlHelper.findNodeWith 'name', 'compounddef'
        this.name = this.xmlHelper.buildRepresentation this.rootNode, 'compoundname'
        this.briefDescription = this.xmlHelper.buildRepresentation this.rootNode, 'briefdescription'
        this.detailedDescription = this.xmlHelper.buildRepresentation this.rootNode, 'detaileddescription'

        compoundName = this.xmlHelper.findChildNodeIn this.rootNode, 'name', 'compoundname'
        name = this.xmlHelper.getNode compoundName.children[0]
        this.readableName = name.t
        this.id = this.xmlHelper.buildId this.readableName

        this.namespace = ''
        if this.readableName.split '::'.length > 1
            split = this.readableName.split('::')
            this.readableName = split.splice(-1, 1)[0]
            this.namespaceString = split.slice(0, split.length-1).join '.'
            this.namespace = this.xmlHelper.buildRef "namespace#{this.namespaceString.split('.').join('_').toLowerCase()}", this.namespaceString

        this.constructSections this.xmlHelper.findNodesWith 'name', 'sectiondef'
        this.constructImplements this.xmlHelper.findNodesWith 'name', 'basecompoundref'
        this.constructInherits this.xmlHelper.findNodesWith 'name', 'basecompoundref'

        delete this.xmlHelper
        delete this.rootNode
        delete @xmlData

    cleanEnumId: (id) ->
        id = this.removeDoxyId id
        id = this.renameIdToEnum id
        return id

    removeDoxyId: (id) ->
        split = (@xmlHelper.convertId id).split('_')
        return split.splice(0, split.length - 1).join('_')

    renameIdToEnum: (id) ->
        if id.startsWith 'namespace'
            return id.replace 'namespace', 'enum'
        if id.startsWith 'class'
            return id.replace 'class', 'enum'
        
    constructImplements: (baseComponents) ->
        this.implements = (this.constructImplement baseComponent for baseComponent in baseComponents when this.isValidInterfaceComponent baseComponent)

    constructInherits: (baseComponents) ->
        this.inherits = (this.constructImplement baseComponent for baseComponent in baseComponents when this.isValidClassComponent baseComponent)

    constructImplement: (baseComponent) ->
        return this.xmlHelper.flattenRef baseComponent

    isValidInterfaceComponent: (baseComponent) ->
        if !baseComponent['@'].refid?
            return false
        return baseComponent['@'].refid.startsWith 'interface'

    isValidClassComponent: (baseComponent) ->
        if !baseComponent['@'].refid?
            return false
        return baseComponent['@'].refid.startsWith 'class'

    constructSections: (sectionDefs) ->
        this.sections = (this.constructSection sectionDef for sectionDef in sectionDefs when this.isValidSectionDef sectionDef)

    constructSection: (sectionDef) ->
        return { 
            kind: sectionDef['@'].kind
            members: this.extractMembers (this.xmlHelper.findChildNodesIn sectionDef, 'name', 'memberdef')
        } 

    isValidSectionDef: (sectionDef) -> 
        return sectionDef['@'].kind.indexOf "private", -1

    isValidMemberDef: (memberDef) -> 
        return (memberDef['@'].prot.indexOf "private", -1) && (memberDef['@'].kind.indexOf "enum", -1)

    isValidParameterDef: (parameterDef) -> 
        return parameterDef['name'] != undefined

    extractMembers: (memberDefs) ->
        return (this.extractMember memberDef for memberDef in memberDefs when this.isValidMemberDef memberDef)

    extractMember: (memberDef) ->
        definition = this.xmlHelper.getTextInNode this.xmlHelper.findChildNodeIn memberDef, 'name', 'definition'

        returnValue = this.xmlHelper.buildRepresentation memberDef, 'type'
        returnString = if returnValue.length > 0 then (returnValue[returnEntry].elements for returnEntry in [0..returnValue.length - 1]) else []
        returnString = returnString.join('')

        if(returnString.includes '< ')
            returnString = returnString.replace('< ', '<')
        if(returnString.includes ' >')
            returnString = returnString.replace(' >', '>')

        functionName = definition.replace("#{returnString} ", '')

        if functionName.split(' ')[0] == 'static'
            functionName = functionName.replace('static ', '')

        functionName = functionName.split('.').splice(-1)[0]

        return {
            prot: memberDef['@'].prot
            static: memberDef['@'].static
            kind: memberDef['@'].kind
            id: this.xmlHelper.convertId memberDef['@'].id
            return: returnValue
            definition: definition
            name: functionName
            arguments: this.xmlHelper.getTextInNode (this.xmlHelper.findChildNodeIn memberDef, 'name', 'argsstring')
            briefDescription: this.xmlHelper.buildRepresentation memberDef, 'briefdescription'
            briefArguments: this.extractBriefArgumentList this.xmlHelper.findChildNodesIn memberDef, 'name', 'param'
            detailedParameterList: this.extractDetailedParameterList this.xmlHelper.findChildNodeRecursiveIn memberDef, 'name', 'parameterlist', 'kind', 'param'
            detailedExceptionList: this.extractDetailedExceptionList this.xmlHelper.findChildNodeRecursiveIn memberDef, 'name', 'parameterlist', 'kind', 'exception'
        }

    extractBriefArgumentList: (argumentDefs) ->
        return (this.extractBriefArgument argumentDef for argumentDef in argumentDefs)

    extractBriefArgument: (argumentDef) ->
        return {
            type: this.xmlHelper.buildRepresentation argumentDef, 'type'
            name: this.xmlHelper.buildRepresentation argumentDef, 'declname'
            defaultValue: this.xmlHelper.buildRepresentation argumentDef, 'defval'
        }

    extractDetailedParameterList: (parameterDefs) ->
        if(parameterDefs == null)
            return []

        return (this.extractDetailedParameter parameterDef for parameterDef in this.xmlHelper.getChildren parameterDefs when this.isValidParameterDef parameterDef)

    extractDetailedExceptionList: (exceptionDefs) ->
        if(exceptionDefs == null)
            return []

        return (this.extractDetailedParameter exceptionDef for exceptionDef in exceptionDefs when this.isValidParameterDef exceptionDef)

    extractDetailedParameter: (parameterDef) ->
        return { 
            name: this.xmlHelper.buildRepresentation (this.xmlHelper.findChildNodeIn parameterDef, 'name', 'parameternamelist'), 'parametername'
            description: this.xmlHelper.buildRepresentation parameterDef, 'parameterdescription'
         }

class Enum extends Internal

    constructor: (@xmlHelper, @memberDef) ->
        this.id = this.cleanEnumId @xmlHelper.convertId @memberDef['@'].id
        this.readableName = (@xmlHelper.buildRepresentation @memberDef, 'name')[0].elements
        this.briefDescription = this.xmlHelper.buildRepresentation @memberDef, 'briefdescription'
        this.enumMembers = this.buildEnumMembers this.xmlHelper.findNodesWith 'name', 'enumvalue'

        namespaceString = @memberDef['@'].id
        namespaceString = namespaceString.split('_1_1').join('.')
        namespaceString = namespaceString.split('_')[0]
        if namespaceString.startsWith 'namespace'
            namespaceString = namespaceString.replace 'namespace', ''
        if namespaceString.startsWith 'class'
            namespaceString = namespaceString.replace 'class', ''

        this.namespaceString = namespaceString
        this.namespace = this.xmlHelper.buildRef "namespace#{namespaceString.split('.').join('_').toLowerCase()}", namespaceString

        delete @xmlHelper
        delete @memberDef

    buildEnumMembers: (enumValues) ->
        return (this.buildEnumMember enumValue for enumValue in enumValues)

    buildEnumMember: (enumValue) ->
        return {
            name: (@xmlHelper.buildRepresentation enumValue, 'name'),
            description: this.xmlHelper.buildRepresentation enumValue, 'briefdescription'
        };

    get: ->
        return {
            id: this.id
            type: 'enum'
            namespace: this.namespace
            briefDescription: this.briefDescription
            readableName: this.readableName
            enumMembers: this.enumMembers
        }

class Interface extends Internal

    constructor: (@xmlData) ->
        super @xmlData

    get: ->
        return {
            id: this.id
            type: 'interface'
            name: this.name
            namespace: this.namespace
            namespaceString: this.namespaceString
            readableName: this.readableName
            briefDescription: this.briefDescription
            detailedDescription: this.detailedDescription
            sections: this.sections
        }

class Namespace extends Internal

    constructor: (@xmlData) ->
        super @xmlData

    get: ->
        return { 
            id: this.id
            type: 'namespace'
            name: this.name
            namespace: this.namespace
            namespaceString: this.namespaceString
            readableName: this.readableName
            briefDescription: this.briefDescription
            detailedDescription: this.detailedDescription
            sections: this.sections
        }

class Class extends Internal
    sections: []

    constructor: (@xmlData) ->
        super @xmlData

    get: ->
        return { 
            id: this.id
            type: 'class'
            name: this.name
            namespace: this.namespace
            namespaceString: this.namespaceString
            implements: this.implements
            inherits: this.inherits
            readableName: this.readableName
            briefDescription: this.briefDescription
            detailedDescription: this.detailedDescription
            sections: this.sections
        }

module.exports.Interface = Interface
module.exports.Enum = Enum
module.exports.Class = Class
module.exports.Namespace = Namespace