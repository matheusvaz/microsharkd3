/* eslint-disable @typescript-eslint/no-var-requires */

const fs = require('fs');

module.exports = {
    devServer: {
        https: {
            key: fs.readFileSync('./certs/localhost+2-key.pem'),
            cert: fs.readFileSync('./certs/localhost+2.pem'),
        },
        http2: true,
        port: 4002,
    },
};
