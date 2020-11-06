import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useDispatch, useSelector } from 'react-redux';

import { Button, Col, notification, PageHeader, Result, Row } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';

import LoadingScreen from 'App/common/components/LoadingScreen';

import UpdateUserForm from '../components/UpdateUserForm';
import { getUser, updateUser } from 'App/state/admin/users/users.thunk';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { cleanUpSelectedUser, cleanUpUserStatus } from 'App/state/admin/users/users.slice';
import { UpdateUserRequest } from 'App/api/endpoints/admin/requests';
import { useTranslation } from 'react-i18next';

interface RouteParams {
	userId: string;
}

interface UpdateUserContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING, SUCCESS } = StatusType;

const UpdateUserContainer: React.FC<UpdateUserContainerProps> = ({ match }) => {
	const userId = match.params.userId;
	const history = useHistory();
	const dispatch = useDispatch();
	const user = useSelector((state: RootState) => state.admin.users.selectedUser);
	const { t } = useTranslation(['page', 'common']);

	const usersStatus = useSelector((state: RootState) => state.admin.users.status);
	useEffect(() => {
		if (!user) {
			dispatch(getUser(userId));
		}
	}, [dispatch, user, userId]);

	useEffect(() => {
		return () => {
			dispatch(cleanUpUserStatus());
			dispatch(cleanUpSelectedUser());
		};
	}, [dispatch]);

	useEffect(() => {
		if (usersStatus.updateUser === SUCCESS) {
			notification.success({
				message: t('common:Success.Success'),
				description: t('AdminPage.UpdateUserContainer.SuccessDescription')
			});
		}
	}, [dispatch, t, usersStatus.updateUser]);

	const handleFormSubmit = (values: UpdateUserRequest) => {
		if (user) {
			dispatch(updateUser(user.id, values));
		}
	};

	return usersStatus.getUser === LOADING ? (
		<LoadingScreen container='screen' />
	) : user ? (
		<React.Fragment>
			<Button
				style={{ marginLeft: 16 }}
				onClick={() => history.push('/admin/users')}
				icon={<ArrowLeftOutlined />}
			>
				{t('common:Actions.GoBack')}
			</Button>
			<Row align='middle' justify='center'>
				<Col span={24}>
					<Row justify='center'>
						<Col>
							<PageHeader title={t('AdminPage.UpdateUserContainer.PageHeaderTitle')} />
						</Col>
					</Row>
					<UpdateUserForm
						initialValues={{
							email: user.email,
							firstName: user.firstName,
							lastName: user.lastName,
							roles: user.roles
						}}
						onFinish={handleFormSubmit}
						loading={usersStatus.updateUser === LOADING}
					/>
				</Col>
			</Row>
		</React.Fragment>
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

export default UpdateUserContainer;
