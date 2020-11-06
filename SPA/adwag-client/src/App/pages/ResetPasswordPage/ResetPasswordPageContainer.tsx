import React from 'react';
import { RouteChildrenProps } from 'react-router';

import { Form, Input, Button, Row, Col } from 'antd';
import queryString from 'querystring';

import agent from 'App/api/agent/agent';
import PageTitle from 'App/common/components/PageTitle';
import { RedoOutlined } from '@ant-design/icons';
import { ResetPasswordRequest } from 'App/api/endpoints/account/requests';

interface ResetPasswordPageProps extends RouteChildrenProps {}

const ResetPasswordPage: React.FC<ResetPasswordPageProps> = ({ location }: ResetPasswordPageProps) => {
	const rules = [
		{
			required: true,
			message: 'Proszę wpisać ponownie nowe hasło!'
		},
		({ getFieldValue }) => ({
			validator(rule, value) {
				if (!value || getFieldValue('newPassword') === value) {
					return Promise.resolve();
				}
				return Promise.reject('Wartości obu pól muszą być takie same!');
			}
		})
	];

	const handleSubmit = (values: ResetPasswordRequest) => {
		const { passwordResetCode, userId } = queryString.parse(location.search.substring(1));

		if (typeof passwordResetCode === 'string' && typeof userId === 'string') {
			values.passwordResetCode = passwordResetCode;
			values.userId = userId;

			console.log(values);
			agent.Account.resetPassword(values)
				.then((res) => console.log(res))
				.catch((err) => console.log(err));
		}
	};

	return (
		<Row align='middle' justify='center'>
			<Col xs={22} sm={18} md={14} xl={12} xxl={8}>
				<PageTitle title='Reset hasła' icon={<RedoOutlined />} />
				<Form layout='horizontal' onFinish={handleSubmit}>
					<Form.Item label='Nowe hasło' hasFeedback name='newPassword' dependencies={['password']} required>
						<Input.Password />
					</Form.Item>
					<Form.Item
						label='Powtórz hasło'
						name='confirmNewPassword'
						dependencies={['newPassword']}
						hasFeedback
						rules={rules}
					>
						<Input.Password />
					</Form.Item>
					<Row align='middle' justify='center'>
						<Col span={16}>
							<Button className='w-100' type='primary' htmlType='submit'>
								Prześlij
							</Button>
						</Col>
					</Row>
				</Form>
			</Col>
		</Row>
	);
};

export default ResetPasswordPage;
