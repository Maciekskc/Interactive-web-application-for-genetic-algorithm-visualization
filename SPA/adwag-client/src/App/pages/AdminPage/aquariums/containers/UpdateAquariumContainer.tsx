import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useDispatch, useSelector } from 'react-redux';

import { Button, Col, notification, PageHeader, Result, Row } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';

import LoadingScreen from 'App/common/components/LoadingScreen';

import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getAquarium, updateAquarium } from 'App/state/aquarium/aquarium.thunk';
import UpdateAquariumRequest from 'App/api/endpoints/aquarium/requests/updateAquariumRequest';
import { cleanUpAquariumStatus, cleanUpSelectedAquarium } from 'App/state/aquarium/aquarium.slice';
import UpdateAquariumForm from '../components/UpdateAquariumForm';

interface RouteParams {
	aquariumId: string;
}

interface UpdateAquariumContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING, SUCCESS } = StatusType;

const UpdateAquariumContainer: React.FC<UpdateAquariumContainerProps> = ({ match }) => {
	const aquariumId = match.params.aquariumId;
	const history = useHistory();
	const dispatch = useDispatch();
	const aquarium = useSelector((state: RootState) => state.aquarium.selectedAquarium);
	const { t } = useTranslation(['page', 'common']);

	const aquariumsStatus = useSelector((state: RootState) => state.aquarium.status);
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

	useEffect(() => {
		if (aquariumsStatus.updateAquarium === SUCCESS) {
			notification.success({
				message: t('common:Success.Success'),
				description: 'Akwarium zostało edytowane'
			});

			history.push('/admin/aquariums');
		}
	}, [dispatch, t, aquariumsStatus.updateAquarium]);

	const handleFormSubmit = (values: UpdateAquariumRequest) => {
		if (aquarium) {
			dispatch(updateAquarium(aquariumId, values));
		}
	};

	return aquariumsStatus.getAquarium === LOADING ? (
		<LoadingScreen container='screen' />
	) : aquarium ? (
		<React.Fragment>
			<Button
				style={{ marginLeft: 16 }}
				onClick={() => history.push('/admin/aquariums')}
				icon={<ArrowLeftOutlined />}
			>
				{t('common:Actions.GoBack')}
			</Button>
			<Row align='middle' justify='center'>
				<Col span={24}>
					<Row justify='center'>
						<Col>
							<PageHeader title={'Aktualizacja Akwarium'} />
						</Col>
					</Row>
					<UpdateAquariumForm
						initialValues={{
							width: aquarium.width,
							height: aquarium.height,
							foodMaximalAmount: aquarium.foodMaximalAmount,
							capacity: aquarium.capacity
						}}
						onFinish={handleFormSubmit}
						loading={aquariumsStatus.updateAquarium === LOADING}
					/>
				</Col>
			</Row>
		</React.Fragment>
	) : (
		<Result
			status='404'
			title={t('common:Errors.AnErrorOccured')}
			subTitle={t('common:Errors.AquariumNotFound')}
			extra={
				<Button type='primary' onClick={() => history.push('/')}>
					{t('common:Actions.BackToHome')}
				</Button>
			}
		/>
	);
};

export default UpdateAquariumContainer;
