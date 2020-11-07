import React, { useEffect, useState } from 'react';

import ProtectedRoute from 'App/common/components/ProtectedRoute';

import { Route, Switch, useLocation } from 'react-router';
import Role from 'App/types/role';
import GetFishContainer from './container/GetFishContainer';
import GetFishesFromAquariumContainer from './container/GetFishesFromAquariumContainer';
import { Button, Layout, Menu } from 'antd';
import { Link } from 'react-router-dom';

const FishPageContainer: React.FC<{}> = () => {
	const Content = (
		<Switch>
			<Route path='/fish/:fishId' component={GetFishContainer} />
			<Route path='/fishes/aquarium/:aquariumId' component={GetFishesFromAquariumContainer} />
			{/* <ProtectedRoute
							acceptedRoles={[Role.ADMIN]}
							exact
							path='/admin/users/:userId/update'
							component={UpdateUserContainer}
						/>
						<ProtectedRoute acceptedRoles={[Role.ADMIN]} path='/admin/users/:userId' component={GetUserContainer} /> */}
		</Switch>
	);
	return <Layout.Content className='pt-3'>{Content}</Layout.Content>;
};

export default FishPageContainer;
