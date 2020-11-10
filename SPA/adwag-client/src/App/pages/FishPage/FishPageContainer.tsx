import React, { useEffect, useState } from 'react';

import ProtectedRoute from 'App/common/components/ProtectedRoute';

import { Route, Switch, useLocation } from 'react-router';
import Role from 'App/types/role';
import GetFishContainer from './container/GetFishContainer';
import GetFishesFromAquariumContainer from './container/GetFishesFromAquariumContainer';
import { Button, Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';
import GetUserFishesContainer from './container/GetUserFishesContainer';
import CreateFishContainer from './container/CreateFishContainer';

const FishPageContainer: React.FC<{}> = () => {
	const Content = (
		<Switch>
			<Route exact path='/fishes/create' component={CreateFishContainer} />
			<Route exact path='/fishes/:fishId' component={GetFishContainer} />
			<Route path='/fishes/aquarium/:aquariumId' component={GetFishesFromAquariumContainer} />
			<ProtectedRoute
				exact
				path='fishes/user-fishes/all'
				component={GetUserFishesContainer}
				acceptedRoles={[Role.USER]}
				others={{ text: 'This is visible only for users' }}
			/>
		</Switch>
	);
	return <Layout.Content className='pt-3'>{Content}</Layout.Content>;
};

export default FishPageContainer;
