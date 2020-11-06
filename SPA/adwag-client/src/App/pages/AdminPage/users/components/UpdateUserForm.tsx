import React from 'react';

import { Input, Select, Button, Form } from 'antd';

import { updateUserFormRules } from '../utils/usersFormRules';
import Role from 'App/types/role';
import { UpdateUserRequest } from 'App/api/endpoints/admin/requests';
import { useTranslation } from 'react-i18next';

interface UpdateUserFormProps {
	initialValues: {
		email: string;
		firstName: string;
		lastName: string;
		roles: Role[];
	};
	onFinish: (values: UpdateUserRequest) => void;
	loading: boolean;
}
const UpdateUserForm: React.FC<UpdateUserFormProps> = ({ initialValues, loading, onFinish }: UpdateUserFormProps) => {
	const {t} = useTranslation(['form', 'common']);

	const layout = {
		labelCol: { span: 8 },
		wrapperCol: { span: 8 }
	};

	const tailLayout = {
		wrapperCol: { offset: 8, span: 8 }
	};	
	
	return (
		<Form {...layout} initialValues={initialValues} onFinish={onFinish}>
			<Form.Item label={t('User.Labels.FirstName')} messageVariables={{arg: t('User.Labels.FirstName')}} name='firstName' rules={updateUserFormRules.firstName()}>
				<Input placeholder={t('User.Placeholders.FirstName')} />
			</Form.Item>

			<Form.Item label={t('User.Labels.LastName')} messageVariables={{arg: t('User.Labels.LastName')}} name='lastName' rules={updateUserFormRules.lastName()}>
				<Input placeholder={t('User.Placeholders.LastName')} />
			</Form.Item>

			<Form.Item name='roles' label={t('User.Labels.Roles')} messageVariables={{arg: t('User.Labels.Roles')}} rules={updateUserFormRules.roles()}>
				<Select mode='multiple' placeholder={t('User.Placeholders.SelectRoles')}>
					<Select.Option value='User'>{t('common:Roles.User')}</Select.Option>
					<Select.Option value='Administrator'>{t('common:Roles.Administrator')}</Select.Option>
				</Select>
			</Form.Item>
			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					{t('common:Actions.Save')}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default UpdateUserForm;
