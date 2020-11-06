import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useSelector, useDispatch } from 'react-redux';

import { Avatar, Badge, Button, Col, Result, Row, Typography } from 'antd';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getUser } from 'App/state/admin/users/users.thunk';
import { getFish } from 'App/state/fish/fish.thunk';

interface RouteParams {
	fishId: string;
}

interface GetFishContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING } = StatusType;

const FishPageContainer: React.FC<GetFishContainerProps> = ({ match }: GetFishContainerProps) => {
	const fishId = match.params.fishId;
	const { t } = useTranslation(['page', 'common']);

	const history = useHistory();
	const dispatch = useDispatch();

	const fish = useSelector((state: RootState) => state.fish.selectedFish);
	const fishStatus = useSelector((state: RootState) => state.fish.status);
	console.log(fish);
	useEffect(() => {
		if (!fish) {
			dispatch(getFish(fishId));
		}
	}, [dispatch, fish, fishId]);

	// useEffect(() => {
	// 	return () => {
	// 		dispatch(cleanUpUserStatus());
	// 		dispatch(cleanUpSelectedUser());
	// 	};
	// }, [dispatch]);

	return fishStatus.getFish === LOADING ? (
		<LoadingScreen container='screen' />
	) : fish ? (
		<>
			{/* <Button
				style={{ marginLeft: 16 }}
				onClick={() => history.push('/admin/users')}
				icon={<ArrowLeftOutlined />}
			>
				{t('common:Actions.GoBack')}
			</Button>

			<Row justify='center'>
				<Col>
					<Avatar size={128} icon={<UserOutlined />} />
				</Col>
			</Row>
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
			<h1>Jest rybka , confirmed</h1>
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

export default FishPageContainer;
