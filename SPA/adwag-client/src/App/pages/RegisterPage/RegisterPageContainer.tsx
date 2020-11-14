import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RouteChildrenProps } from 'react-router';

import { PageHeader, Row, Col, notification } from 'antd';
import { Store } from 'antd/lib/form/interface';

import RegisterForm from './components/RegisterForm';
import { RegisterRequest } from 'App/api/endpoints/auth/requests';
import { RootState } from 'App/state/root.reducer';
import StatusType from 'App/types/requestStatus';
import { register } from 'App/state/session/session.thunk';

interface RegisterPageContainerProps extends RouteChildrenProps {
	name?: string;
}

const RegisterPageContainer: React.FC<RegisterPageContainerProps> = ({ history }: RegisterPageContainerProps) => {
	type FinishFormType = (values: Store) => void;

	const dispatch = useDispatch();
	const status = useSelector((state: RootState) => state.session.status.register);

	const registerHandler: FinishFormType = (values: RegisterRequest) => {
		let handleSuccess: () => void = () => {
			history.push('/sign-in');
			notification.success({
				message: 'Sukces',
				description: 'Twoje konto zostało utworzone'
			});
		};
		dispatch(register(values, handleSuccess));
	};

	return (
		<div className='register--container'>
			<Row align='middle' justify='center'>
				<Col xs={22} md={14} xl={10} xxl={8}>
					<br />

					<PageHeader title={'Rejestracja'} />
					<RegisterForm
						preserve
						className='register-form'
						name='registerForm'
						size='large'
						onFinish={registerHandler}
						autoComplete='off'
						loading={status === StatusType.LOADING}
					/>
				</Col>
			</Row>
		</div>
	);
};

export default RegisterPageContainer;
