import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RouteChildrenProps } from 'react-router';

import { PageHeader, Row, Col } from 'antd';
import { Store } from 'antd/lib/form/interface';

import LoginForm from './components/LoginForm';
import './LoginPageContainer.less';
import { LoginRequest } from 'App/api/endpoints/auth/requests';
import { authenticateUser } from 'App/state/session/session.thunk';
import { RootState } from 'App/state/root.reducer';
import LoadingScreen from 'App/common/components/LoadingScreen';
import StatusType from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';

interface LoginPageContainerProps extends RouteChildrenProps {
	name?: string;
}

const LoginPageContainer: React.FC<LoginPageContainerProps> = ({ history }: LoginPageContainerProps) => {
	type FinishFormType = (values: Store) => void;

	const { t } = useTranslation('page');

	const dispatch = useDispatch();
	const status = useSelector((state: RootState) => state.session.status.authentication);

	const formInitialValues = {
		email: 'admin@test.com',
		password: 'Admin123!'
	};

	const signInHandler: FinishFormType = (values: LoginRequest) => {
		let handleSuccess: () => void = () => {
			history.push('/auth');
		};

		dispatch(
			authenticateUser(
				{
					password: values.password,
					email: values.email
				},
				handleSuccess
			)
		);
	};

	return (
		<div className='login--container'>
			{status === StatusType.LOADING ? (
				<LoadingScreen container='screen' />
			) : (
				<Row align='middle' justify='center'>
					<Col xs={22} md={14} xl={10} xxl={8}>
						<br />

						<PageHeader title={t('LoginPage.LoginPageContainer.PageHeaderTitle')} />
						<LoginForm
							className='login-form'
							name='loginForm'
							size='large'
							onFinish={signInHandler}
							initialValues={formInitialValues}
							autoComplete='off'
						/>
					</Col>
				</Row>
			)}
		</div>
	);
};

export default LoginPageContainer;
