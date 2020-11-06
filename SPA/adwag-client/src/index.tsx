import React, { Suspense } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import App from './App/App';
import store from './App/state/store';
import './i18n';

import './index.less';

function render(): void {
	ReactDOM.render(
		<Provider store={store}>
			<Suspense fallback={<div>Loading...</div>}>
				<App />
			</Suspense>
		</Provider>,
		document.getElementById('root')
	);
}

render();

if (process.env.NODE_ENV === 'development' && module.hot) {
	module.hot.accept('./App/App', render);
}
