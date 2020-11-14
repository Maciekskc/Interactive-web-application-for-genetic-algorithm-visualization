import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useSelector, useDispatch } from 'react-redux';

import { Avatar, Badge, Button, Card, Col, Result, Row, Typography } from 'antd';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getAquarium } from 'App/state/aquarium/aquarium.thunk';
import { ArrowLeftOutlined, CreditCardFilled } from '@ant-design/icons';
import GetAquariumAnimation from './GetAquariumAnimation';
import { Link } from 'react-router-dom';
import { cleanUpAquariumStatus, cleanUpSelectedAquarium } from 'App/state/aquarium/aquarium.slice';
import { GetUserTabs } from 'App/pages/AdminPage/users/components/GetUserTabs';

interface RouteParams {
	aquariumId: string;
}

interface GetAquariumContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING } = StatusType;

const GetAquariumContainer: React.FC<GetAquariumContainerProps> = ({ match }: GetAquariumContainerProps) => {
	const aquariumId = match.params.aquariumId;
	const { t } = useTranslation(['page', 'common']);

	const history = useHistory();
	const dispatch = useDispatch();

	const aquarium = useSelector((state: RootState) => state.aquarium.selectedAquarium);
	const aquariumStatus = useSelector((state: RootState) => state.aquarium.status);

	useEffect(() => {
		if (!aquarium) {
			dispatch(getAquarium(aquariumId));
		}
	}, [dispatch, aquarium, aquariumId]);

	useEffect(() => {
		return () => {
			dispatch(cleanUpAquariumStatus());
			dispatch(cleanUpSelectedAquarium());
		};
	}, [dispatch]);

	return aquariumStatus.getAquarium === LOADING ? (
		<LoadingScreen container='screen' />
	) : aquarium ? (
		<>
			<Button style={{ marginLeft: 16 }} onClick={() => history.push('/aquariums')} icon={<ArrowLeftOutlined />}>
				{t('common:Actions.GoBack')}
			</Button>

			<Row justify='center'>
				<Col>
					<Button
						style={{ marginLeft: 16 }}
						onClick={() => history.push(`/aquariums/${aquariumId}/animation`)}
					>
						Przejdź do Animacji
					</Button>
				</Col>
				<Col>
					<Button style={{ marginLeft: 16 }} onClick={() => history.push(`/fishes/aquarium/${aquariumId}`)}>
						Zobacz Populacje
					</Button>
				</Col>
			</Row>
			<Row
				style={{
					marginTop: 16
				}}
				justify='center'
			>
				<Col span={18}>
					<Card>
						<p>Szerokość : {aquarium.width}</p>
						<p>Wysokość : {aquarium.height}</p>
						<p>Liczność populacji : {aquarium.currentFoodsAmount}</p>
						<p>Ilość osobników : {aquarium.currentPopulationCount}</p>
					</Card>
				</Col>
			</Row>
		</>
	) : (
		<Result
			status='404'
			title={t('common:Errors.AnErrorOccured')}
			subTitle={t('common:Errors.UserNotFound')}
			extra={
				<Button type='primary' onClick={() => history.push('/')}>
					{t('common:Actions.BackToHome')}
				</Button>
			}
		/>
	);
};

export default GetAquariumContainer;
