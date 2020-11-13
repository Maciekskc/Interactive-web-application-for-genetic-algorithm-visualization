import { ArrowLeftOutlined } from '@ant-design/icons';
import { Button, Col, notification, PageHeader, Row } from 'antd';
import { CreateFishRequest } from 'App/api/endpoints/fish/requests/CreateFishRequest';
import { cleanUpFishStatus } from 'App/state/fish/fish.slice';
import { createFish } from 'App/state/fish/fish.thunk';
import { RootState } from 'App/state/root.reducer';
import StatusType from 'App/types/requestStatus';
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useHistory } from 'react-router';
import CreateFishForm from '../components/CreateFishForm';

const { LOADING, SUCCESS } = StatusType;

export const CreateFishContainer = () => {
	const dispatch = useDispatch();
	const history = useHistory();

	let fishesStatus = useSelector((state: RootState) => state.fish.status);

	const handleFormSubmit = (values: CreateFishRequest) => {
		dispatch(createFish(values));
	};

	useEffect(() => {
		return () => {
			dispatch(cleanUpFishStatus());
		};
	}, [dispatch]);

	useEffect(() => {
		if (fishesStatus.createFish === SUCCESS) {
			notification.success({
				message: 'Sukces',
				description: 'Rybka została dodana'
			});
		}
	}, [dispatch, fishesStatus.createFish]);

	return (
		<React.Fragment>
			<Row className='mb-5'>
				<Col>
					<Button
						style={{ marginLeft: 16 }}
						block
						onClick={() => history.push(`/aquariums/`)}
						icon={<ArrowLeftOutlined />}
					>
						{'Wstecz'}
					</Button>
				</Col>
			</Row>
			<Row justify='center'>
				<Col span={24}>
					<Row justify='center'>
						<Col>
							<PageHeader title={'Tworzenie obiektu'} />
						</Col>
					</Row>
					<CreateFishForm loading={fishesStatus.createFish === LOADING} onFinish={handleFormSubmit} />
				</Col>
			</Row>
		</React.Fragment>
	);
};

export default CreateFishContainer;
