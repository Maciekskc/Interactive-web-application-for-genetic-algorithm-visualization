import React, { useEffect } from 'react';
import { useHistory } from 'react-router';
import { useDispatch, useSelector } from 'react-redux';

import { Button, Row, Col, notification, PageHeader } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';

import CreateUserForm from '../components/CreateUserForm';
import { createUser } from 'App/state/admin/users/users.thunk';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { cleanUpUserStatus } from 'App/state/admin/users/users.slice';
import { CreateUserRequest } from 'App/api/endpoints/admin/requests';
import { useTranslation } from 'react-i18next';

const { LOADING, SUCCESS } = StatusType;

export const CreateUserContainer = () => {
	const dispatch = useDispatch();
	const history = useHistory();
	const { t } = useTranslation(['page', 'common']);

	let usersStatus = useSelector((state: RootState) => state.admin.users.status);

	const handleFormSubmit = (values: CreateUserRequest) => {
		dispatch(createUser(values));
	};

	useEffect(() => {
		return () => {
			dispatch(cleanUpUserStatus());
		};
	}, [dispatch]);

	useEffect(() => {
		if (usersStatus.createUser === SUCCESS) {
			notification.success({
				message: t('common:Success.Success'),
				description: t('AdminPage.CreateUserContainer.SuccessDescription')
			});
		}
	}, [dispatch, t, usersStatus.createUser]);

	return (
		<React.Fragment>
			<Row className='mb-5'>
				<Col>
					<Button
						style={{ marginLeft: 16 }}
						block
						onClick={() => history.push('/admin/users')}
						icon={<ArrowLeftOutlined />}
					>
						{t('common:Actions.GoBack')}
					</Button>
				</Col>
			</Row>
			<Row justify='center'>
				<Col span={24}>
					<Row justify='center'>
						<Col>
							<PageHeader title={t('AdminPage.CreateUserContainer.PageHeaderTitle')} />
						</Col>
					</Row>
					<CreateUserForm loading={usersStatus.createUser === LOADING} onFinish={handleFormSubmit} />
				</Col>
			</Row>
		</React.Fragment>
	);
};

export default CreateUserContainer;
