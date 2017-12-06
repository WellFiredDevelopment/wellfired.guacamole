class xmlhelper

    constructor: (@squashedXmlData) ->
        this.squashedXmlData = @squashedXmlData
        this.rootNode = this.findNodeWith 'name', 'compounddef'

    findNodeWith: (nodeKey, nodeValue) ->
        for key, node of this.squashedXmlData when node[nodeKey] == nodeValue
            return node

    findNodesWith: (nodeKey, nodeValue) ->
        nodes = []
        for key, node of this.squashedXmlData when node[nodeKey] == nodeValue
            nodes.push node
        return nodes

    findChildNodeIn: (parent, nodeKey, nodeValue) ->
        childNodes = this.getChildren parent
        for key, node of childNodes when node[nodeKey] == nodeValue
            return node

    findChildNodesIn: (parent, nodeKey, nodeValue) ->
        nodes = []
        childNodes = this.getChildren parent
        for key, node of childNodes when node[nodeKey] == nodeValue
            nodes.push node
        return nodes

    findChildNodeRecursiveIn: (parent, nodeKey, nodeValue, argumentKey, argumentValue) ->
        if parent[nodeKey] == nodeValue && parent['@'][argumentKey] == argumentValue
            return parent
        else if parent.children != undefined
            result = null;
            children = this.getChildren parent
            for child in children
                result = this.findChildNodeRecursiveIn child, nodeKey, nodeValue, argumentKey, argumentValue
                if result != null
                    return result;

        return null;

    buildId: (name) ->
        return name.split("::").join("_").toLowerCase();

    convertId: (name) ->
        return name.split("_1_1").join("_").toLowerCase();

    getAttribute: (node, argumentKey) -> 
        return node['@'][argumentKey]

    getChildren: (parent) ->
        return if parent.children? then (this.getNode child for child in parent.children) else undefined

    getNode: (nodeId) ->
        return this.squashedXmlData[nodeId]

    getTextInNode: (node) ->
        if !node?
            return ''

        child = this.getNode node.children[0]
        if child?
            return this.cleanText child.t 
        else 
            return ''

    cleanText: (text) ->
        if /^\s*$/.test text then return '' else return text

    flattenText: (node) -> 
        return { type: 'text', elements: this.cleanText(node.t) }

    flattenPara: (node) ->
        children = this.getChildren node
        return { type: 'para', elements: (this.flattenNode child for child in children) }

    flattenRef: (node) ->
        return { type: 'ref', refId: (this.convertId node['@']['refid']), elements: (this.getNode node.children[0]).t }

    buildRef: (id, text) ->
        return { type: 'ref', refId: id, elements: text }

    flattenNode: (node) ->
        if node.t?
            return this.flattenText node
        if node.name == 'para'
            return this.flattenPara node 
        if node.name == 'ref'
            return this.flattenRef node

    elementsToFlatData: (nodes) ->
        return (this.flattenNode node for node in nodes)

    buildRepresentation: (parent, nodeValue) ->
        nameNode = this.findChildNodeIn parent, 'name', nodeValue
        if(nameNode == undefined || nameNode.children == null)
            return []
        children = this.getChildren nameNode
        return this.elementsToFlatData children

module.exports = xmlhelper