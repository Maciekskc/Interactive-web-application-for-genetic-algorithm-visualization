import React from 'react';
import { Form, Input, Button } from 'antd';
import { FormProps } from 'antd/lib/form/Form';
import { registerFormRules } from '../utils/registerPageFormRules';
import { useTranslation } from 'react-i18next';
import i18n from '../../../../i18n';
import Role from 'App/types/role';

interface RegisterFormProps extends FormProps {
	loading: boolean;
}

const RegisterForm: React.FC<RegisterFormProps> = (props: RegisterFormProps) => {
	const { t } = useTranslation(['page', 'form', 'common']);
	const { loading } = props;

	return (
		<Form {...props}>
			<Form.Item name='firstName' messageVariables={{ arg: 'Imie' }} rules={registerFormRules.firstName()}>
				<Input placeholder={'Imie'} />
			</Form.Item>
			<Form.Item name='lastName' messageVariables={{ arg: 'Nazwisko' }} rules={registerFormRules.lastName()}>
				<Input placeholder={'Nazwisko'} />
			</Form.Item>
			<Form.Item name='email' messageVariables={{ arg: 'E-mail' }} rules={registerFormRules.email()}>
				<Input placeholder={'E-mail'} />
			</Form.Item>
			<Form.Item
				name='password'
				messageVariables={{ arg: 'Hasło' }}
				rules={registerFormRules.password(t('form:Register.Labels.Password'))}
			>
				<Input type='password' placeholder={'Hasło'} />
			</Form.Item>
			<Form.Item
				name='confirmPassword'
				messageVariables={{ arg: 'Nazwisko' }}
				rules={registerFormRules.confirmPassword(t('form:Register.Labels.Password'))}
			>
				<Input type='password' placeholder={'Potwierdź hasło'} />
			</Form.Item>
			<Form.Item initialValue={i18n.language} name='language' style={{ display: 'none' }}></Form.Item>
			<Form.Item initialValue={[Role.USER]} name='roles' style={{ display: 'none' }}></Form.Item>
			<Form.Item>
				<Button
					loading={loading}
					className='f-left register-form-button'
					type='primary'
					htmlType='submit'
					size='large'
				>
					{'Zarejestruj'}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default RegisterForm;
