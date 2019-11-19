const path = require('path');
require('dotenv').config();

const webpack = require('@dolittle/typescript.webpack.aurelia').webpack
const config = webpack(__dirname);

module.exports = () => {
    const obj = config.apply(null, arguments);

    let headers = {
        'Portal-ID': '22222222-1a6f-40d7-b2fc-796dba92dc44',
        'Portal-Domain': 'localhost',
        'Owner-Tenant-ID': '445f8ea8-1a6f-40d7-b2fc-796dba92dc44'
    };
    obj.devServer = {
        historyApiFallback: true,
        proxy: {
            '/auth': {
                'target': 'http://localhost:5000',
                'headers': headers
            },
            '/api': {
                'target': 'http://localhost:5000',
                'headers': headers
            },
            '/Dolittle': {
                'target':'http://localhost:5000',
                'headers': headers
            }
        }
    };
    obj.resolve.alias = {
        DolittleStyles: path.resolve(__dirname, './styles')
    };
    return obj;
};

