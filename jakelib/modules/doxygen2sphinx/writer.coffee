utils           = require('./../utils')
util            = require('util')
ContentBuffer   = require('./contentbuffer').ContentBuffer
writerutil      = require('./writerutil')
ClassWriter     = require('./writers/classwriter')
EnumWriter      = require('./writers/enumwriter')

class Writer
    constructor: (@sphinxOutDir, @data, @projectName) ->
        this.graph = {}
        this.injectNamespaceEntry data.namespaceString.split('.'), this.graph for data in @data when (data.namespaceString? && data.namespaceString.length > 0)

    injectNamespaceEntry: (data, graph) -> 
        if data.length == 0
            return
        
        entry = data[0]
        if !(graph.hasOwnProperty(entry))
            graph[entry] = {}
    
        data.shift()
        this.injectNamespaceEntry data, graph[entry]

    write: ->
        this.apiDir = @sphinxOutDir + '/api'
        utils.createDir @sphinxOutDir
        utils.createDir this.apiDir
        this.createGraph this.apiDir + '/namespaces', this.graph
        this.dumpIndex()
        this.writeData data for data in @data

    createGraph: (root, graph) ->
        utils.createDir root
        for k, v of graph
            this.createGraph "#{root}/#{k}", v 

    # Gets the writer associated with a class type
    getWriter: (type) ->
        if type == 'class' then return new ClassWriter
        if type == 'interface' then return new ClassWriter
        if type == 'namespace' then return new ClassWriter
        if type == 'enum' then return new EnumWriter

    dumpIndex: ->
        apiInclude = ""
        apiInclude = apiInclude.concat "   classes/index.rst\n"
        apiInclude = apiInclude.concat "   interfaces/index.rst\n"
        apiInclude = apiInclude.concat "   namespaces/index.rst\n"
        apiInclude = apiInclude.concat "   enums/index.rst\n\n"

        this.dumpIndexAt @sphinxOutDir + '/api', "#{@projectName} API", 'api', apiInclude
        this.dumpIndexAt @sphinxOutDir + '/api/classes', 'Classes', 'class', "   class_*"
        this.dumpIndexAt @sphinxOutDir + '/api/interfaces', 'Interfaces', 'interface', "   interface_*"
        this.dumpIndexAt @sphinxOutDir + '/api/namespaces', 'Namespaces', 'namespace', "   namespace_*"
        this.dumpIndexAt @sphinxOutDir + '/api/enums', 'Enums', 'enum', "   enum_*"

    dumpIndexAt: (path, header, apiName, include) ->
        utils.createDir path

        raw = ""
        raw = raw.concat "#{header}\n"
        raw = raw.concat "#{writerutil.buildUnderlineFor header, '='}\n\n"

        raw = raw.concat ".. toctree::\n"
        raw = raw.concat "   :maxdepth: 1\n"
        raw = raw.concat "   :name: toc-#{apiName}-ref\n"
        raw = raw.concat "   :glob:\n\n"
        raw = raw.concat "#{include}"

        utils.write "#{path}/index.rst", raw

    validToWrite: (data) ->
        invalidBriefDescription = "#{writerutil.buildStringFor data.briefDescription}".length == 0
        invalidDetailedDescription = data.detailedDescription? && "#{writerutil.buildStringFor data.detailedDescription}".length == 0
        invalidSections = !data.sections? || data.sections.length == 0

        if invalidBriefDescription && invalidDetailedDescription && invalidSections
            return false
            
        return true

    writeData: (data) ->
        if !this.validToWrite data
            return

        rstPath = "#{this.apiDir}/#{writerutil.dataTypeToPath data.type}/#{data.type}_#{data.id}.rst"

        utils.createDir "#{this.apiDir}/#{writerutil.dataTypeToPath data.type}"
        utils.touch rstPath

        cb = new ContentBuffer
        writer = this.getWriter data.type
        writer.write data, cb
        utils.write rstPath, cb.buffer

module.exports = Writer