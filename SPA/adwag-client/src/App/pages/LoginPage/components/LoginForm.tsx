import React from 'react';
import { Form, Input, Checkbox, Button } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { FormProps } from 'antd/lib/form/Form';
import './LoginForm.less';
import { loginFormRules } from '../utils/loginPageFormRules';
import { useTranslation } from 'react-i18next';

interface LoginFormProps extends FormProps {}

const LoginForm: React.FC<LoginFormProps> = (props: LoginFormProps) => {
	const { t } = useTranslation(['page', 'form', 'common']);

	return (
		<Form {...props}>
			<Form.Item name='email' messageVariables={{arg: t('form:Login.Labels.Email')}} rules={loginFormRules.email()}>
				<Input prefix={<UserOutlined className='site-form-item-icon' />} placeholder={t('form:Login.Placeholders.Email')} />
			</Form.Item>
			<Form.Item name='password' messageVariables={{arg: t('form:Login.Labels.Password')}} rules={loginFormRules.password()}>
				<Input
					prefix={<LockOutlined className='site-form-item-icon' />}
					type='password'
					placeholder={t('form:Login.Placeholders.Password')}
				/>
			</Form.Item>
			<Form.Item>
				<Form.Item name='remember' valuePropName='checked' noStyle>
					<Checkbox>{t('form:Login.Labels.RememberMe')}</Checkbox>
				</Form.Item>
				<a className='login-form-forgot f-right' href='#href-id' id='href-id'>
					{t('LoginPage.LoginForm.ForgotPassword')}
				</a>
			</Form.Item>
			<Form.Item>
				<Button className='f-left login-form-button' type='primary' htmlType='submit' size='large'>
					{t('common:Actions.SignIn')}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default LoginForm;
