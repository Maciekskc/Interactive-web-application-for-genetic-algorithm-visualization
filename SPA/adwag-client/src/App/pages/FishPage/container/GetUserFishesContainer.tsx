import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, RouteComponentProps } from 'react-router-dom';

import { Row, Col, Button, Table, Input, notification } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { getUsers } from 'App/state/admin/users/users.thunk';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getUserFishes } from 'App/state/fish/fish.thunk';
import { cleanUpFishStatus, getUserFishesStart } from 'App/state/fish/fish.slice';

const { LOADING, SUCCESS } = StatusType;

const GetUserFishesContainer = () => {
	const dispatch = useDispatch();
	const { t } = useTranslation();
	//todo
	const fishes = useSelector((state: RootState) => state.fish.userFishes);
	const fishesStatus = useSelector((state: RootState) => state.fish.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.fish.getUserFishesParams
	);

	useEffect(() => {
		dispatch(getUserFishes(defaultPageQueryParams));
		console.log(fishes);
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	// useEffect(() => {
	// 	if (fishesStatus.deleteUser === SUCCESS) {
	// 		notification.success({
	// 			message: t('common:Success.Success'),
	// 			description: t('AdminPage.GetUsersContainer.SuccessDescription')
	// 		});
	// 	}
	// }, [dispatch, t, usersStatus.deleteUser]);

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
					<Link to='/fish/create'>
						<Button icon={<PlusOutlined />}>Nowy Obiekt</Button>
					</Link>
				</Col>
			</Row>
			<Row className='overflow-hidden'>
				<Col span={24}>
					<Input
						allowClear
						onChange={(val) =>
							dispatch(
								getUserFishes({
									...defaultPageQueryParams,
									query: val.currentTarget.value
								})
							)
						}
					/>
					<Table
						pagination={paginationConfig}
						onChange={handleTableChange}
						loading={fishesStatus.getFishesFromAquarium === LOADING}
						//columns={renderTableColumns(fishes, dispatch, t)}
						dataSource={fishes}
						rowKey='id'
					/>
				</Col>
			</Row>
		</>
	);
};

export default GetUserFishesContainer;
