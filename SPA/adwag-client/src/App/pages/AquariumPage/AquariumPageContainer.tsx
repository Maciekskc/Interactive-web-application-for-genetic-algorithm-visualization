import React, { useEffect, useState } from 'react';

import { Button, Layout, Menu } from 'antd';
import GetAquariumContainer from './container/GetAquariumContainer';
import GetAquariumsContainer from './container/GetAquariumsContainer';
import { Route, Switch } from 'react-router';

const AquariumPageContainer: React.FC<{}> = () => {
	const Content = (
		<Switch>
			<Route exact path='/aquariums/:aquariumId' component={GetAquariumContainer} />
			<Route path='/aquariums' component={GetAquariumsContainer} />
		</Switch>
	);
	return (
		<>
			{/* <Layout.Sider width={256} className='bg-site'>
				<NavbarContainer />
			</Layout.Sider> */}
			<Layout.Content className='pt-3'>{Content}</Layout.Content>
		</>
	);
};

export default AquariumPageContainer;
