var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var path = require('path');
var webpack = require('webpack');
var nodeExternals = require('webpack-node-externals');
var merge = require('webpack-merge');
var allFilenamesExceptJavaScript = /\.(?!js(\?|$))([^.]+(\?|$))/;

// Configuration in common to both client-side and server-side bundles
var sharedConfig = {
    resolve: { extensions: ['.js', '.ts'] },
    output: {
        filename: '[name].js',
        publicPath: '/dist/' // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                include: /ClientApp/,
                exclude: [/\.(spec|e2e)\.ts$/],
                use: [{ loader: 'ts-loader' }, { loader: 'angular2-template-loader' }]
            },
            {
                test: /\.(html|css)$/, loader: 'raw-loader'
            },
            {
                test: /\.(png|jpg|jpeg|gif|svg)$/,
                loader: 'url-loader',
                query: { limit: 25000 }
            },
            {
                test: /\.scss$/,
                include: /ClientApp/,
                exclude: /node_modules/,
                use: [{ loader: 'raw-loader' }, { loader: 'sass-loader' }]
            },
            {
                include: /ClientApp/,
                loader: 'angular-router-loader'
            }
        ]
    }
};

// Configuration for client-side bundle suitable for running in browsers
var clientBundleConfig = merge(sharedConfig, {
    entry: { 'main-client': './ClientApp/boot-client.ts' },
    output: { path: path.join(__dirname, './wwwroot/dist') },
    devtool: isDevBuild ? 'inline-source-map' : null,
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
var serverBundleConfig = merge(sharedConfig, {
    entry: { 'main-server': './ClientApp/boot-server.ts' },
    output: {
        libraryTarget: 'commonjs',
        path: path.join(__dirname, './ClientApp/dist')
    },
    target: 'node',
    devtool: 'inline-source-map',
    externals: [nodeExternals({ whitelist: [allFilenamesExceptJavaScript] })] // Don't bundle .js files from node_modules
});

module.exports = [clientBundleConfig, serverBundleConfig];
