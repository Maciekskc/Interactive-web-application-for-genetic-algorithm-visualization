import React, { useEffect } from 'react';
import { Form, Input, Select, Button } from 'antd';
import { CreateFishRequest } from 'App/api/endpoints/fish/requests/CreateFishRequest';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { createUserFormRules } from 'App/pages/AdminPage/users/utils/usersFormRules';
import { getAquariums } from 'App/state/aquarium/aquarium.thunk';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { RootState } from 'App/state/root.reducer';
import { useDispatch, useSelector } from 'react-redux';
import Item from 'antd/lib/list/Item';

interface CreateFishFormProps {
	onFinish: (values: CreateFishRequest) => void;
	initialValues?: CreateFishRequest;
	loading: boolean;
}

const CreateFishForm: React.FC<CreateFishFormProps> = ({ initialValues, loading, onFinish }) => {
	const layout = {
		labelCol: { span: 8 },
		wrapperCol: { span: 8 }
	};

	const tailLayout = {
		wrapperCol: { offset: 8, span: 8 }
	};
	const aquariums = useSelector((state: RootState) => state.aquarium.aquariums);
	const aquariumsStatus = useSelector((state: RootState) => state.aquarium.status);

	const { pageNumber, pageSize, totalNumberOfItems } = useSelector(
		(state: RootState) => state.aquarium.getAquariumsParams
	);
	const dispatch = useDispatch();
	useEffect(() => {
		dispatch(
			getAquariums({
				...defaultPageQueryParams,
				pageNumber: 1,
				pageSize: 100,
				query: ''
			})
		);
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	return (
		<Form {...layout} layout='horizontal' initialValues={initialValues} onFinish={onFinish}>
			<Form.Item
				messageVariables={{ arg: 'name' }}
				label={'name'}
				name='name'
				//rules={createUserFormRules.email()}
			>
				<Input placeholder={'name'} />
			</Form.Item>

			<Form.Item name='aquariumId' label={'aquariumId'} messageVariables={{ arg: 'aquariumId' }}>
				<Select mode='multiple' placeholder={'aquariumId'}>
					{aquariums.map((item) => {
						<Select.Option value={item.id}>{item.id}</Select.Option>;
					})}
				</Select>
			</Form.Item>
			{/* <Form.Item
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
			</Form.Item> */}

			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					Utwórz
				</Button>
			</Form.Item>
		</Form>
	);
};

export default CreateFishForm;
