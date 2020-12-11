import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useSelector, useDispatch } from 'react-redux';

import Histogram from 'react-chart-histogram';
import { Button, Card, Col, Result, Row } from 'antd';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getAquarium } from 'App/state/aquarium/aquarium.thunk';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { cleanUpAquariumStatus, cleanUpSelectedAquarium } from 'App/state/aquarium/aquarium.slice';
import { render } from 'react-dom';

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

	const histogramLabels = ['0.5', '1', '1.5', '2', '2.5', '3', '3.5', '4'];
	const histogramOptions = {
		fillColor: '#FFFFFF',
		strokeColor: '#0000FF',
		legend: 'Histogram poziomu najedzenia populacji'
	};

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
						<p>Maksymalna liczność populacji : {aquarium.currentFoodsAmount}</p>
						<p>Ilość osobników : {aquarium.currentPopulationCount}</p>
					</Card>
				</Col>
			</Row>
			<Row style={{ marginTop: 16 }} justify='space-around'>
				<Card style={{ marginLeft: '15rem' }}>
					<h1 style={{ textAlign: 'center' }}>Poziom najedzenia populacji</h1>
					<Histogram
						xLabels={histogramLabels}
						yValues={aquarium.hungaryHistogramData}
						width='800'
						height='300'
						options={histogramOptions}
					/>
				</Card>
				<Card
					style={{
						textAlign: 'center',
						marginTop: '6rem',
						height: '9rem',
						boxShadow: ' 3px black',
						marginRight: '25rem'
					}}
				>
					<h3>Średni poziom najedzenia</h3>
					<h2>{aquarium.hungaryAvarage}</h2>
				</Card>
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
