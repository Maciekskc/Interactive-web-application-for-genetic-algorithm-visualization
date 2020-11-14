import React from 'react';
import { Button, Form, Input, InputNumber } from 'antd';
import { useTranslation } from 'react-i18next';
import UpdateAquariumRequest from 'App/api/endpoints/aquarium/requests/updateAquariumRequest';
import { GetAquariumResponse } from 'App/api/endpoints/aquarium/responses/getAquariumResponse';

interface UpdateAquariumFormProps {
	onFinish: (values: UpdateAquariumRequest) => void;
	initialValues: UpdateAquariumRequest;
	loading: boolean;
}
const UpdateAquariumForm: React.FC<UpdateAquariumFormProps> = ({ initialValues, loading, onFinish }) => {
	const { t } = useTranslation(['form', 'common']);

	const layout = {
		labelCol: { span: 12 },
		wrapperCol: { span: 12 }
	};
	const tailLayout = {
		wrapperCol: { offset: 8, span: 8 }
	};
	return (
		<Form {...layout} layout='horizontal' initialValues={initialValues} onFinish={onFinish}>
			<Form.Item messageVariables={{ arg: 'width' }} label='Szerokość' name='width'>
				<InputNumber min={100} defaultValue={initialValues.width} placeholder={`${initialValues.width}`} />
			</Form.Item>

			<Form.Item messageVariables={{ arg: 'height' }} label='Wysokość' name='height'>
				<InputNumber min={50} defaultValue={initialValues.height} placeholder={`${initialValues.height}`} />
			</Form.Item>
			<Form.Item messageVariables={{ arg: 'capacity' }} label='Maxymalna Liczność Populacji' name='capacity'>
				<InputNumber
					min={2}
					width={40}
					defaultValue={initialValues.capacity}
					placeholder={`${initialValues.capacity}`}
				/>
			</Form.Item>
			<Form.Item
				messageVariables={{ arg: 'foodMaximalAmount' }}
				label='Stałaa ilość pożywienia'
				name='foodMaximalAmount'
			>
				<InputNumber
					min={1}
					defaultValue={initialValues.foodMaximalAmount}
					placeholder={`${initialValues.foodMaximalAmount}`}
				/>
			</Form.Item>
			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					{'Edytuj'}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default UpdateAquariumForm;
