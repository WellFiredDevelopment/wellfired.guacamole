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

    replace: (path, pattern, replacement) ->
        contents = jetpack.read(path)
        contents = contents.replace pattern, replacement
        jetpack.write(path, contents, { atomic: true })

    touch: (path) ->
        touch.sync(path)

    writeJson: (path, json) ->
        jetpack.write path, json

}
