import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RouteComponentProps } from 'react-router-dom';

import { Row, Col, Table, Input, Typography } from 'antd';

import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { getAquariums } from 'App/state/aquarium/aquarium.thunk';
import { renderTableColumns } from '../utils/AquariumTable';
const { Title } = Typography;

interface RouteParams {
	aquariumId: string;
}

const { LOADING } = StatusType;

const GetAquariumsContainer = () => {
	const dispatch = useDispatch();
	const { t } = useTranslation();

	const aquariums = useSelector((state: RootState) => state.aquarium.aquariums);
	const aquariumsStatus = useSelector((state: RootState) => state.aquarium.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.aquarium.getAquariumsParams
	);

	useEffect(() => {
		dispatch(getAquariums(defaultPageQueryParams));
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	const handleTableChange = (pagination: any): any => {
		dispatch(
			getAquariums({
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
			<Row justify='center'>
				<Title level={2}>Lista Akwarium</Title>
			</Row>
			<Row className='overflow-hidden'>
				<Col span={24}>
					<Input
						allowClear
						onChange={(val) =>
							dispatch(
								getAquariums({
									...defaultPageQueryParams,
									query: val.currentTarget.value
								})
							)
						}
					/>
					<Table
						pagination={paginationConfig}
						onChange={handleTableChange}
						loading={aquariumsStatus.getAquariums === LOADING}
						columns={renderTableColumns(aquariums, dispatch, t)}
						dataSource={aquariums}
						rowKey='id'
					/>
				</Col>
			</Row>
		</>
	);
};

export default GetAquariumsContainer;
