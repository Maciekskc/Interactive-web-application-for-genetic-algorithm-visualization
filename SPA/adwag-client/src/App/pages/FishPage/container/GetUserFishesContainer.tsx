import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

import { Row, Col, Button, Table, Input, notification, Typography } from 'antd';
import { ArrowLeftOutlined, PlusOutlined } from '@ant-design/icons';

import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getUserFishes } from 'App/state/fish/fish.thunk';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { renderUserFishesTableColumns } from '../utils/FishTable';
const { Title } = Typography;
const { LOADING, SUCCESS } = StatusType;

const GetUserFishesContainer = () => {
	const dispatch = useDispatch();
	const { t } = useTranslation();

	const fishes = useSelector((state: RootState) => state.fish.userFishes);
	const fishesStatus = useSelector((state: RootState) => state.fish.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.fish.getUserFishesParams
	);

	useEffect(() => {
		dispatch(getUserFishes(defaultPageQueryParams));
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	useEffect(() => {
		if (fishesStatus.killFish === SUCCESS) {
			notification.success({
				message: t('common:Success.Success'),
				description: t('AdminPage.GetUsersContainer.SuccessDescription')
			});
		}
	}, [dispatch, t, fishesStatus.killFish]);

	const handleTableChange = (pagination: any): any => {
		dispatch(
			getUserFishes({
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
				<Col span={4}>
					<Link to='/aquariums'>
						<Button style={{ marginLeft: 16 }} icon={<ArrowLeftOutlined />}>
							{t('common:Actions.GoBack')}
						</Button>
					</Link>
				</Col>
				<Col span={16} style={{ textAlign: 'center' }}>
					<Title level={2}>Obiekty użytkownika</Title>
				</Col>
				<Col span={4}>
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
						columns={renderUserFishesTableColumns(fishes, dispatch, t)}
						dataSource={fishes}
						rowKey='id'
					/>
				</Col>
			</Row>
		</>
	);
};

export default GetUserFishesContainer;
