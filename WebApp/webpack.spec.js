var Webpack = require('webpack');
var Path = require('path');

module.exports = {
	devtool: 'inline-source-map',

	resolve: {
		extensions: ['.ts', '.js']
	},

	module: {
		rules: [
          { test: /\.ts$/, include: /ClientApp/, loader: 'ts-loader', query: { silent: true } },
            { test: /\.html$/, loader: 'raw-loader' },
            { test: /\.css$/, loader: 'to-string-loader!css' },
            { test: /\.(png|jpg|jpeg|gif|svg)$/, loader: 'url-loader', query: { limit: 25000 } },
            {
            	test: /\.scss$/,
            	include: /ClientApp/,
            	exclude: /node_modules/,
            	use: [{ loader: 'raw-loader' }, { loader: 'sass-loader' }]
            },
            { include: /ClientApp/, loader: 'angular-router-loader' }
		]
	},
	plugins: [
	  new Webpack.ContextReplacementPlugin(
	    // The (\\|\/) piece accounts for path separators in *nix and Windows
	    /angular(\\|\/)core(\\|\/)(esm(\\|\/)src|src)(\\|\/)linker/,
	    root('./ClientApp/app'), // location of your src
	    {} // a map of your routes
	  )
	]
}

function root(__path) {
    return Path.join(__dirname, __path);
}
