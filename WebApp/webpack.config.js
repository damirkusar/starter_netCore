var isDevBuild = process.argv.indexOf('--env.dev') > 0;
var isProdBuild = process.argv.indexOf('--env.prod') > 0;
var isAotBuild = process.argv.indexOf('aot') > 0;
console.log("webpack.config isDevBuild", isDevBuild);
console.log("webpack.config isProdBuild", isProdBuild);
console.log("webpack.config isAotBuild", isAotBuild);

//var AotPlugin = require('@ultimate/aot-loader').AotPlugin;
var AotPlugin = require('@ngtools/webpack').AotPlugin;
var path = require('path');
var webpack = require('webpack');
var nodeExternals = require('webpack-node-externals');
var merge = require('webpack-merge');
var allFilenamesExceptJavaScript = /\.(?!js(\?|$))([^.]+(\?|$))/;

function GetAotPlugin() {
    var aotPlugin = new AotPlugin({
        tsConfigPath: './tsconfig.json'
    });

    // Windows 10 Fix
    aotPlugin._compilerHost._resolve = function (path_to_resolve) {
        path_1 = require("path");
        path_to_resolve = aotPlugin._compilerHost._normalizePath(path_to_resolve);
        if (path_to_resolve[0] == '.') {
            return aotPlugin._compilerHost._normalizePath(path_1.join(aotPlugin._compilerHost.getCurrentDirectory(), path_to_resolve));
        }
        else if (path_to_resolve[0] == '/' || path_to_resolve.match(/^\w:\//)) {
            return path_to_resolve;
        }
        else {
            return aotPlugin._compilerHost._normalizePath(path_1.join(aotPlugin._compilerHost._basePath, path_to_resolve));
        }
    };

    return aotPlugin;
}

// Configuration in common to both client-side and server-side bundles
var SharedConfig = {};
var Config = {
    resolve: { extensions: ['.js', '.ts'] },
    output: {
        filename: '[name].js',
        publicPath: '/dist/' // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                exclude: [/\.(spec|e2e)\.ts$/],
                use: [{ loader: '@ngtools/webpack' }]
            },
            { test: /\.css$/, loader: ['to-string-loader', 'css-loader'] },
            { test: /\.html$/, loader: 'html-loader' },
            { test: /\.scss$/, loaders: ['to-string-loader', 'css-loader', 'sass-loader'] },
            { test: /\.(png|jpg|jpeg|gif|svg)$/, loader: 'url-loader', query: { limit: 25000 } },
            { include: /ClientApp/, loader: 'angular-router-loader' }
        ]
    },
    plugins: [
        GetAotPlugin()
    ]
};

if (isAotBuild) {
    console.log("webpack.config isAotBuild");
    SharedConfig = merge({
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    exclude: [/\.(spec|e2e)\.ts$/],
                    use: [{ loader: '@ngtools/webpack' }]
                }
            ]
        },
        plugins: [
            GetAotPlugin()
        ]
    }, Config);
} else {
    console.log("webpack.config isNormalBuild");
    SharedConfig = merge({
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    exclude: [/\.(spec|e2e)\.ts$/],
                    loaders: ['ts-loader', 'angular2-template-loader']
                }
            ]
        }
    }, Config);
}

// Configuration for client-side bundle suitable for running in browsers
var ClientBundleConfig = merge(Config, {
    entry: { 'main-client': './ClientApp/boot-client.ts' },
    output: { path: path.join(__dirname, './wwwroot/dist') },
    devtool: 'inline-source-map',
    plugins: [
        new webpack.DllReferencePlugin({
            context: __dirname,
            manifest: require('./wwwroot/dist/vendor-manifest.json')
        })
    ].concat(isDevBuild ? [] : [
        // Plugins that apply in production builds only
        //new webpack.optimize.OccurrenceOrderPlugin(),
        new webpack.optimize.UglifyJsPlugin()
    ])
});

// Configuration for server-side (prerendering) bundle suitable for running in Node
//var serverBundleConfig = merge(sharedConfig, {
//    entry: { 'main-server': './ClientApp/boot-server.ts' },
//    output: {
//        libraryTarget: 'commonjs',
//        path: path.join(__dirname, './ClientApp/dist')
//    },
//    target: 'node',
//    devtool: isDevBuild ? 'eval' : 'source-map',
//    externals: [nodeExternals({ whitelist: [allFilenamesExceptJavaScript] })] // Don't bundle .js files from node_modules
//});

module.exports = [ClientBundleConfig];
