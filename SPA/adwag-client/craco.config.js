const CracoLessPlugin = require('craco-less');

module.exports = {
	plugins: [
		{
			plugin: CracoLessPlugin,
			options: {
				lessLoaderOptions: {
					lessOptions: {
						modifyVars: { '@primary-color': '#79a3b1' },
						javascriptEnabled: true
					}
				}
			}
		}
	]
};
