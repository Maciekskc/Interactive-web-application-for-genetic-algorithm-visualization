import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, RouteComponentProps, useHistory } from 'react-router-dom';

import { Row, Col, Button, Table, Input, Typography } from 'antd';
import { ArrowLeftOutlined, PlusOutlined } from '@ant-design/icons';

import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getFishesFromAquarium } from 'App/state/fish/fish.thunk';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { renderFishesFromAquariumTableColumns } from '../utils/FishTable';
const { Title } = Typography;

interface RouteParams {
	aquariumId: string;
}

interface GetFishesFromAquariumContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING } = StatusType;

const GetFishesFromAquariumContainer: React.FC<GetFishesFromAquariumContainerProps> = ({
	match
}: GetFishesFromAquariumContainerProps) => {
	const aquariumId = match.params.aquariumId;
	const dispatch = useDispatch();
	const history = useHistory();
	const { t } = useTranslation();

	const fishes = useSelector((state: RootState) => state.fish.fishesFromAquarium);
	const fishesStatus = useSelector((state: RootState) => state.fish.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.fish.getFishesFromAquiariumParams
	);

	useEffect(() => {
		dispatch(getFishesFromAquarium(defaultPageQueryParams, aquariumId));
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	const handleTableChange = (pagination: any): any => {
		dispatch(
			getFishesFromAquarium(
				{
					pageNumber: pagination.current || 1,
					pageSize: pagination.pageSize || 10,
					query: '',
					orderBy: ''
				},
				aquariumId
			)
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
					<Button
						style={{ marginLeft: 16 }}
						onClick={() => history.push('/aquariums')}
						icon={<ArrowLeftOutlined />}
					>
						{t('common:Actions.GoBack')}
					</Button>
				</Col>
				<Col span={20} style={{ textAlign: 'center' }}>
					<Title level={2}>Populacja w akwarium #{aquariumId}</Title>
				</Col>
			</Row>
			<Row>
				<Col span={23}>
					<Link to='/fishes/create'>
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
								getFishesFromAquarium(
									{
										...defaultPageQueryParams,
										query: val.currentTarget.value
									},
									aquariumId
								)
							)
						}
					/>
					<Table
						pagination={paginationConfig}
						onChange={handleTableChange}
						loading={fishesStatus.getFishesFromAquarium === LOADING}
						columns={renderFishesFromAquariumTableColumns(fishes, dispatch, t)}
						dataSource={fishes}
						rowKey='id'
					/>
				</Col>
			</Row>
		</>
	);
};

export default GetFishesFromAquariumContainer;
