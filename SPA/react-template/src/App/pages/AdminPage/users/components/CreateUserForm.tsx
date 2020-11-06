import React from 'react';
import { Form, Input, Select, Button } from 'antd';
import { CreateUserRequest } from 'App/api/endpoints/admin/requests';
import { useTranslation } from 'react-i18next';
import { createUserFormRules } from '../utils/usersFormRules';
import { languages } from 'i18n';

interface CreateUserFormProps {
	onFinish: (values: CreateUserRequest) => void;
	initialValues?: CreateUserRequest;
	loading: boolean;
}

const CreateUserForm: React.FC<CreateUserFormProps> = ({ initialValues, loading, onFinish }) => {
	const { t } = useTranslation(['form', 'common']);

	const layout = {
		labelCol: { span: 8 },
		wrapperCol: { span: 8 }
	};

	const tailLayout = {
		wrapperCol: { offset: 8, span: 8 }
	};

	return (
		<Form {...layout} layout='horizontal' initialValues={initialValues} onFinish={onFinish}>
			<Form.Item
				messageVariables={{ arg: t('User.Labels.Email') }}
				label={t('User.Labels.Email')}
				name='email'
				rules={createUserFormRules.email()}
			>
				<Input placeholder={t('User.Placeholders.Email')} />
			</Form.Item>

			<Form.Item
				label={t('User.Labels.Password')}
				messageVariables={{ arg: t('User.Labels.Password') }}
				name='password'
				rules={createUserFormRules.password(t('User.Labels.Password'))}
			>
				<Input type='password' placeholder={t('User.Placeholders.Password')} />
			</Form.Item>

			<Form.Item
				label={t('User.Labels.FirstName')}
				messageVariables={{ arg: t('User.Labels.FirstName') }}
				name='firstName'
				rules={createUserFormRules.firstName()}
			>
				<Input placeholder={t('User.Placeholders.FirstName')} />
			</Form.Item>

			<Form.Item
				label={t('User.Labels.LastName')}
				messageVariables={{ arg: t('User.Labels.LastName') }}
				name='lastName'
				rules={createUserFormRules.lastName()}
			>
				<Input placeholder={t('User.Placeholders.LastName')} />
			</Form.Item>

			<Form.Item
				name='roles'
				label={t('User.Labels.Roles')}
				messageVariables={{ arg: t('User.Labels.Roles') }}
				rules={createUserFormRules.roleName()}
			>
				<Select mode='multiple' placeholder={t('User.Placeholders.SelectRoles')}>
					<Select.Option value='User'>{t('common:Roles.User')}</Select.Option>
					<Select.Option value='Administrator'>{t('common:Roles.Administrator')}</Select.Option>
				</Select>
			</Form.Item>

			<Form.Item
				name='language'
				label={t('User.Labels.EmailLanguage')}
				messageVariables={{ arg: t('User.Labels.EmailLanguage')}}
				rules={createUserFormRules.emailLanguage()}
			>
				<Select placeholder={t('User.Placeholders.EmailLanguage')}>
					{languages.map((language) => (
						<Select.Option key={language} value={language}>{language}</Select.Option>
					))}
				</Select>
			</Form.Item>

			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					{t('common:Actions.Create')}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default CreateUserForm;
