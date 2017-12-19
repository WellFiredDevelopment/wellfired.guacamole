jetpack = require('fs-jetpack')
touch   = require('touch')

module.exports = {

    append: (path, value) ->
        jetpack.append path, value

    clean: (path, pattern) ->
        p = pattern || '**/*';

        if !jetpack.exists(path)
            return

        jetpack.find(path, { matching: p })
           .forEach(jetpack.remove)

    copy: (srcPath, dstPath, options) ->

        info = jetpack.inspect(srcPath)

        if info.type == 'file'
            opt = { overwrite: options.clean if options? }
            jetpack.copy srcPath, dstPath, opt
            return

        opt = {
            overwrite: options.clean if options?,
            matching:  options.pattern if options?
        }

        src = jetpack.cwd(srcPath)
        dst = jetpack.dir(dstPath)

        src.copy('.', dst.path(), opt)

    delete: (path) ->
        jetpack.remove(path)

    exists: (path) ->
        jetpack.exists(path)

    find: (path, opts) ->
        jetpack.find(path, opts)

    createDir: (path, opts) ->
        jetpack.dir(path, opts)

    getConfig: (c, debug, release) ->
        envConfig = process.env.config

        if c == 'd' or c == 'debug'
            return debug

        if c == 'r' or c == 'release'
            return release

        if envConfig == 'd' || envConfig == 'debug'
            return debug

        return release

    readJson: (path) ->
        jetpack.read path, 'json'

    read: (path) ->
        jetpack.read path

    replace: (path, pattern, replacement) ->
        contents = jetpack.read(path)
        contents = contents.replace pattern, replacement
        jetpack.write(path, contents, { atomic: true })

    touch: (path) ->
        touch.sync(path)

    writeJson: (path, json) ->
        jetpack.write path, json

    write: (path, text) ->
        jetpack.write path, text

    startsWith: (st, c) ->
        st.slice(0, c.length) == c

    removeFirstCharacter: (st) ->
        return st.slice(1, st.length);

    endsWith: (st, c) ->
        c == '' or st.slice(-c.length) == c

    inspectTree: (path, opts) ->
        return jetpack.inspectTree path, opts

    inspect: (path, opts) ->
        return jetpack.inspect path, opts

    flattenChildren: (obj, array) ->
        array.push obj

        if(!obj.children?)
            return array

        for child in obj.children
            do (child) ->
                module.exports.flattenChildren child, array
        
        if(obj.children)
            delete obj.children

    replaceGuid: (content, newGuid) ->
        indexOfGuidText = content.indexOf('guid')
        indexOfNextNewline = content.indexOf('\n', indexOfGuidText)
        pt1 = content.slice(0, indexOfGuidText)
        pt2 = content.slice(indexOfNextNewline + 1, content.length)
        return pt1 + "guid: " + newGuid + "\n" + pt2

    getTopDir: (dir) ->
        results = dir.split('/');
        return results[results.length - 1];

    removeTopDir: (dir) ->
        top = module.exports.getTopDir dir
        lastIndexOf = dir.lastIndexOf top
        return dir.slice 0, lastIndexOf
}
