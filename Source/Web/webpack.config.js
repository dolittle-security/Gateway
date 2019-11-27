const path = require('path');
require('dotenv').config();
const process = require('process');

const webpack = require('@dolittle/typescript.webpack.aurelia').webpack;
const upstreamConfig = webpack(__dirname);

module.exports = environment => {
    if (environment && environment.production) {
        process.env.DOLITTLE_WEBPACK_BASE_URL = '/signin/';
    }
    const config = upstreamConfig.apply(null, arguments);
    config.resolve.alias.DolittleStyles = path.resolve(__dirname, './Styles');
    config.module.rules.unshift({
        test: /environment\.json$/i,
        use: [
            {
                loader: 'app-settings-loader',
                options: { env: environment && environment.production ? 'production' : 'development' }
            }
        ]
    });
    return config;
};
