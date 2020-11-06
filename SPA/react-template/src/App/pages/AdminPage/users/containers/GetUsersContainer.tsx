import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

import { Row, Col, Button, Table, Input, notification } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

import { renderTableColumns } from '../utils/UsersTable';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { getUsers } from 'App/state/admin/users/users.thunk';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { cleanUpUserStatus } from 'App/state/admin/users/users.slice';
import { useTranslation } from 'react-i18next';

const { LOADING, SUCCESS } = StatusType;

const GetUsersContainer = () => {
	const dispatch = useDispatch();
	const { t } = useTranslation();

	const users = useSelector((state: RootState) => state.admin.users.users);
	const usersStatus = useSelector((state: RootState) => state.admin.users.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.admin.users.getUsersParams
	);

	useEffect(() => {
		dispatch(getUsers(defaultPageQueryParams));

		return () => {
			dispatch(cleanUpUserStatus());
		};
	}, [dispatch]);

	useEffect(() => {
		if(usersStatus.deleteUser === SUCCESS) {
			notification.success({
				message: t('common:Success.Success'),
				description: t('AdminPage.GetUsersContainer.SuccessDescription')
			})
		}
	}, [dispatch, t, usersStatus.deleteUser])

	const handleTableChange = (pagination: any): any => {
		dispatch(
			getUsers({
				...defaultPageQueryParams,
				pageNumber: pagination.current || 1,
				pageSize: pagination.pageSize || 10,
				query: ''
			})
		);
	};

	const paginationConfig = {
		pageSize,
		current: pageNumber,
		total: totalNumberOfItems,
		showSizeChanger: true
	};

	return (
		<>
			<Row>
				<Col span={23}>
					<Link to='/admin/users/create'>
						<Button icon={<PlusOutlined />}>{t('AdminPage.GetUsersContainer.NewUserButton')}</Button>
					</Link>
				</Col>
			</Row>
			<Row className='overflow-hidden'>
				<Col span={24}>
					<Input
						allowClear
						onChange={(val) =>
							dispatch(
								getUsers({
									...defaultPageQueryParams,
									query: val.currentTarget.value
								})
							)
						}
					/>
					<Table
						pagination={paginationConfig}
						onChange={handleTableChange}
						loading={usersStatus.getUsers === LOADING}
						columns={renderTableColumns(users, dispatch, t)}
						dataSource={users}
						rowKey='id'
					/>
				</Col>
			</Row>
		</>
	);
};

export default GetUsersContainer;
