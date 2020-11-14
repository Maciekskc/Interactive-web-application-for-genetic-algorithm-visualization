import React from 'react';

import ProtectedRoute from 'App/common/components/ProtectedRoute';

import { Switch } from 'react-router';
import Role from 'App/types/role';
import GetAquariumsContainer from './containers/GetAquariumsContainer';
import CreateAquariumContainer from './containers/CreateAquariumContainer';
import UpdateAquariumContainer from './containers/UpdateAquariumContainer';

const AdminPageAquariumContainer: React.FC<{}> = () => {
	return (
		<Switch>
			<ProtectedRoute
				acceptedRoles={[Role.ADMIN]}
				exact
				path='/admin/aquariums'
				component={GetAquariumsContainer}
			/>
			<ProtectedRoute
				acceptedRoles={[Role.ADMIN]}
				exact
				path='/admin/aquariums/create'
				component={CreateAquariumContainer}
			/>
			<ProtectedRoute
				acceptedRoles={[Role.ADMIN]}
				exact
				path='/admin/aquariums/:aquariumId/update'
				component={UpdateAquariumContainer}
			/>
		</Switch>
	);
};

export default AdminPageAquariumContainer;
