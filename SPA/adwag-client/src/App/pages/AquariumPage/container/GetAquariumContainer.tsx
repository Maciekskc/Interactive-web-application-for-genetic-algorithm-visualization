import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useSelector, useDispatch } from 'react-redux';

import { Avatar, Badge, Button, Col, Result, Row, Typography } from 'antd';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getAquarium } from 'App/state/aquarium/aquarium.thunk';
import { ArrowLeftOutlined } from '@ant-design/icons';

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
	console.log(aquarium);
	useEffect(() => {
		if (!aquarium) {
			dispatch(getAquarium(aquariumId));
		}
	}, [dispatch, aquarium, aquariumId]);

	// useEffect(() => {
	// 	return () => {
	// 		dispatch(cleanUpUserStatus());
	// 		dispatch(cleanUpSelectedUser());
	// 	};
	// }, [dispatch]);

	return aquariumStatus.getAquarium === LOADING ? (
		<LoadingScreen container='screen' />
	) : aquarium ? (
		<>
			<Button style={{ marginLeft: 16 }} onClick={() => history.push('/aquariums')} icon={<ArrowLeftOutlined />}>
				{t('common:Actions.GoBack')}
			</Button>

			<Row justify='center'>
				<Col>Tutaj wrzucimy podgląd akwarium</Col>
			</Row>
			{/*			
			<Row justify='center'>
				<Col>
					<Badge style={{ color: gold[5] }} count={user.lockoutEnabled ? <LockOutlined /> : 0}>
						<Typography.Text delete={user.isDeleted} strong style={{ fontSize: '1.5rem' }}>
							{user.firstName} {user.lastName}
						</Typography.Text>
					</Badge>
				</Col>
			</Row>
			<Row justify='center'>
				<Col>
					<Badge
						status={user.emailConfirmed ? 'success' : 'default'}
						title={
							user.emailConfirmed
								? t('AdminPage.GetUserContainer.StatusConfirmed')
								: t('AdminPage.GetUserContainer.StatusUnConfirmed')
						}
					>
						<Typography.Text type='secondary'>{user.email}</Typography.Text>
					</Badge>
				</Col>
			</Row>
			<GetUserTabs user={user} /> */}
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
