EventEmitter    = require('events').EventEmitter
utils           = require('./utils')
targz           = require('targz');
md5             = require('md5-node');


class unitypackage

    emitter = new EventEmitter

    extract: (inFile, outDir) ->
        if utils.exists outDir
            emitter.emit 'error', "outDir #{outDir} already exists"
            return

        if !utils.exists inFile
            emitter.emit 'error', "inFile #{inFile} doesn't exist"
            return

        handler = (err) -> 
            emitter.emit err if err?
            emitter.emit 'success' if !err?

        targz.decompress({src: inFile, dest: outDir}, handler);


    build: (inDir, outFile) ->
        if !utils.exists inDir
            emitter.emit 'error', "inDir #{inDir} doesn't exists"
            return
            
        if utils.exists outFile
            emitter.emit 'error', "outFile #{outFile} already exist"
            return

        tmpDir = inDir + "Tmp"
        buildTmpDir inDir, tmpDir

        handler = (err) -> 
            utils.delete(tmpDir)
            emitter.emit err if err?
            emitter.emit 'success' if !err?

        targz.compress({src: tmpDir, dest: outFile}, handler)
    

    buildTmpDir = (inDir, tmpDir) ->

        topFolder = utils.getTopDir inDir;
        resDir = utils.removeTopDir inDir

        if utils.exists tmpDir
            emitter.emit 'error', "tmpDir #{tmpDir} already exists"
            return

        # Map the directory structure.
        utils.copy inDir, tmpDir + '/' + topFolder, { clean: true }

        result = utils.inspectTree tmpDir, { checksum: 'md5', relativePath: true }

        utils.delete tmpDir + '/' + topFolder

        flattenedChildren = []

        utils.flattenChildren result, flattenedChildren

        flattenedChildren.shift()

        if(utils.exists inDir + '.meta')
            inspectedMeta = utils.inspect inDir + '.meta', { checksum: 'md5', relativePath: true }
            inspectedMeta.relativePath = './' + inspectedMeta.name
            flattenedChildren.push inspectedMeta

        createInternalMD5 flattenedChildren

        filteredChildren = filterChildren flattenedChildren
        metaChildren = filterChildrenForMeta flattenedChildren
        
        for child in filteredChildren
            utils.createDir tmpDir + '/' + child.internalmd5

        # Step 0 - Clean relativePath
        for child in filteredChildren
            child.cleanRelativePath = utils.removeFirstCharacter child.relativePath
            child.cleanRelativePath = utils.removeFirstCharacter child.cleanRelativePath
        for child in metaChildren
            child.cleanRelativePath = utils.removeFirstCharacter child.relativePath
            child.cleanRelativePath = utils.removeFirstCharacter child.cleanRelativePath

        # Step 1 - Sort out pathname
        for child in filteredChildren
            child.pathname = 'Assets' + '/' + child.cleanRelativePath

        # Step 2 - Write out pathname
        for child in filteredChildren
            utils.writeJson tmpDir + '/' + child.internalmd5 + '/pathname', child.pathname

        # Step 3 - Write out missing meta files
        for child in filteredChildren
            if !metaChildren.some((metaChild) -> metaChild.relativePath.slice(0, -5) == child.relativePath)
                utils.writeJson tmpDir + '/' + child.internalmd5 + '/asset.meta', createMetaFile child.internalmd5

        # Step 4 - Sort out source and destination paths for assets
        for child in filteredChildren
            child.sourcePath = resDir + child.cleanRelativePath
            child.destinationPath = tmpDir + '/' + child.internalmd5 + '/asset'

        # Step 5 - Sort out source and destination paths for meta files
        for metaChild in metaChildren
            metaPath = metaChild.relativePath

            for child in filteredChildren
                if child.relativePath + '.meta' == metaPath
                    metaChild.internalmd5 = child.internalmd5

            metaChild.sourcePath = resDir + metaChild.cleanRelativePath
            metaChild.destinationPath = tmpDir + '/' + metaChild.internalmd5 + '/asset.meta'

        utils.writeJson tmpDir + '/info.json', filteredChildren
        utils.writeJson tmpDir + '/meta.json', metaChildren

        # step 6 - Copy Assets
        for child in filteredChildren
            if(child.type == "file")
                utils.copy child.sourcePath, child.destinationPath
        
        # step 7 - Copy Metas file and use our generated GUID
        for child in metaChildren
            if(child.type == "file")
                utils.copy child.sourcePath, child.destinationPath
                content = utils.read child.destinationPath
                content = utils.replaceGuid content, child.internalmd5
                utils.write child.destinationPath, content


    filterChildrenForMeta = (children) -> 
        isMetaFile = (child) -> utils.endsWith child.name, '.meta'
        return filter children, isMetaFile


    filterChildren = (children) -> 
        isNotHiddenFile = (child) -> !utils.startsWith child.name, '.'
        isNotMetaFile = (child) -> !utils.endsWith child.name, '.meta'
        children = filter children, isNotHiddenFile
        return filter children, isNotMetaFile

    createMetaFile = (guid) -> [
        "fileFormatVersion: 2"
        "guid: " + guid
        "timeCreated: 1514950942"
        "licenseType: Free"
        "DefaultImporter:"
        "  userData:"
        "  assetBundleName:"
        "  assetBundleVariant:"
        ].join("\n");

    filter = (list, func) -> x for x in list when func(x)


    createInternalMD5 = (list) ->
        for x in list
            do (x) -> x.internalmd5 = md5(x.relativePath)


    on: (name, cb) ->
        emitter.on name, cb


module.exports = unitypackage