utils           = require('./../utils')
XML             = require("squash-xml-json")
Interface       = require('./internalbase').Interface
Class           = require('./internalbase').Class
Namespace       = require('./internalbase').Namespace
Enum            = require('./internalbase').Enum
xmlhelper       = require('./xmlhelper')

class reader

    constructor: (@sphinxInDir, @sphinxOutDir) ->

    extractStructure: (filePath) ->
        return new Promise (resolve, reject) ->
            resolve(XML.flattenXML utils.read(filePath));

    parse: (files) -> 
        _this = this;
        return new Promise (resolve, reject) ->
            Promise.all((_this.extractStructure file) for file in files)
            .then (results) ->
                resolve results

    parseClasses: ->
        _this = this;
        return this.parse utils.find @sphinxInDir, { matching: 'class*.xml' }
            .then (results) ->
                enums = (result for result in (_this.breakoutEnums result for result in results) when result?)
                return (new Class result for result in results).concat enums

    parseInterfaces: ->
        _this = this;
        return this.parse utils.find @sphinxInDir, { matching: 'interface*.xml' }
            .then (results) ->
                enums = (result for result in (_this.breakoutEnums result for result in results) when result?)
                return (new Interface result for result in results).concat enums

    parseNamespaces: ->
        _this = this;
        return this.parse utils.find @sphinxInDir, { matching: 'namespace*.xml' }
            .then (results) ->
                enums = (result for result in (_this.breakoutEnums result for result in results) when result?)
                return (new Namespace result for result in results).concat enums

    # For some reason, doxygen doesn't break enums out into their own files, so we're going
    # to break them out here
    breakoutEnums: (data) ->
        xmlHelper = new xmlhelper data
        sectionDef = xmlHelper.findChildNodeRecursiveIn xmlHelper.rootNode, 'name', 'sectiondef', 'kind', 'enum'
        if !sectionDef?
            return
        
        memberDefs = xmlHelper.findChildNodesIn sectionDef, 'name', 'memberdef'
        return new Enum xmlHelper, memberDef for memberDef in memberDefs
    
    read: ->
        return Promise.all([this.parseNamespaces(), this.parseClasses(), this.parseInterfaces()])
        .then (results) ->
            return [].concat.apply([], results)
        .then (results) ->
            return (result.get() for result in results)

module.exports = reader