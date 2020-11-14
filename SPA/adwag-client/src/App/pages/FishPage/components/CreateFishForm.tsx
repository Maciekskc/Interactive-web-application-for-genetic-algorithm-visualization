import React, { useEffect } from 'react';
import { Form, Input, Select, Button } from 'antd';
import { CreateFishRequest } from 'App/api/endpoints/fish/requests/CreateFishRequest';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { getAquariums } from 'App/state/aquarium/aquarium.thunk';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { RootState } from 'App/state/root.reducer';
import { useDispatch, useSelector } from 'react-redux';
import StatusType from 'App/types/requestStatus';

interface CreateFishFormProps {
	onFinish: (values: CreateFishRequest) => void;
	initialValues?: CreateFishRequest;
	loading: boolean;
}

const { LOADING, SUCCESS } = StatusType;

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
				pageSize: 20,
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
				<Select placeholder={'aquariumId'} loading={aquariumsStatus.getAquariums === LOADING}>
					{aquariums.map((item) => (
						<Select.Option key={item.id} value={item.id}>
							{item.id}
						</Select.Option>
					))}
				</Select>
			</Form.Item>

			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					Utwórz
				</Button>
			</Form.Item>
		</Form>
	);
};

export default CreateFishForm;
