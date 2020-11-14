import React from 'react';

import ProtectedRoute from 'App/common/components/ProtectedRoute';

import GetLogsContainer from './containers/GetLogsContainer';
import { Switch } from 'react-router';
import Role from 'App/types/role';

const AdminPageLogsContainer: React.FC<{}> = () => {
	return (
		<Switch>
			<ProtectedRoute acceptedRoles={[Role.ADMIN]} exact path='/admin/logs' component={GetLogsContainer} />
		</Switch>
	);
};

export default AdminPageLogsContainer;
