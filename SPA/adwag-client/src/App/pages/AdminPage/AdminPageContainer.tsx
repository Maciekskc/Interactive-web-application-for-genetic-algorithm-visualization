import React from 'react';

import { Layout } from 'antd';

import { default as AdminNavbar } from './containers/AdminNavbarContainer';
import { default as AdminPageUsers } from './users/AdminPageUsersContainer';
import { default as AdminPageLogs } from './logs/AdminPageLogsContainer';
import { default as AdminPageAquariums } from './aquariums/AdminPageUsersContainer';

const AdminPageContainer: React.FC<{}> = () => {
	const Content = (
		<>
			<AdminPageUsers />
			<AdminPageLogs />
			<AdminPageAquariums />
		</>
	);
	return (
		<div className='d-flex'>
			<Layout.Sider width={256} className='bg-site'>
				<AdminNavbar />
			</Layout.Sider>
			<Layout.Content className='pt-3'>{Content}</Layout.Content>
		</div>
	);
};

export default AdminPageContainer;
