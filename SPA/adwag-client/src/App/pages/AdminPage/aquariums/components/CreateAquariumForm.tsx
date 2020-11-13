import React from 'react';
import { Button, Form, Input, InputNumber } from 'antd';
import { useTranslation } from 'react-i18next';
import CreateAquariumRequest from 'App/api/endpoints/aquarium/requests/createAquariumRequest';

interface CreateAquariumFormProps {
	onFinish: (values: CreateAquariumRequest) => void;
	initialValues?: CreateAquariumRequest;
	loading: boolean;
}
const CreateAquariumForm: React.FC<CreateAquariumFormProps> = ({ initialValues, loading, onFinish }) => {
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
				<InputNumber min={100} defaultValue={1080} placeholder='1080' />
			</Form.Item>

			<Form.Item messageVariables={{ arg: 'height' }} label='Wysokość' name='height'>
				<InputNumber min={50} defaultValue={720} placeholder='720' />
			</Form.Item>
			<Form.Item messageVariables={{ arg: 'capacity' }} label='Maxymalna Liczność Populacji' name='capacity'>
				<InputNumber min={2} width={40} defaultValue={15} placeholder='15' />
			</Form.Item>
			<Form.Item
				messageVariables={{ arg: 'foodMaximalAmount' }}
				label='Stałaa ilość pożywienia'
				name='foodMaximalAmount'
			>
				<InputNumber min={1} defaultValue={7} placeholder={'7'} />
			</Form.Item>
			<Form.Item {...tailLayout}>
				<Button block loading={loading} type='primary' htmlType='submit'>
					{t('common:Actions.Create')}
				</Button>
			</Form.Item>
		</Form>
	);
};

export default CreateAquariumForm;
