import { Col, notification, PageHeader, Row } from 'antd';
import CreateAquariumRequest from 'App/api/endpoints/aquarium/requests/createAquariumRequest';
import { cleanUpAquariumStatus } from 'App/state/aquarium/aquarium.slice';
import { createAquarium } from 'App/state/aquarium/aquarium.thunk';
import { RootState } from 'App/state/root.reducer';
import StatusType from 'App/types/requestStatus';
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import CreateAquariumForm from '../components/CreateAquariumForm';

const { LOADING, SUCCESS } = StatusType;

export const CreateAquariumContainer = () => {
	const dispatch = useDispatch();

	let aquariumsStatus = useSelector((state: RootState) => state.aquarium.status);

	const handleFormSubmit = (values: CreateAquariumRequest) => {
		console.log(values);
		dispatch(createAquarium(values));
	};

	useEffect(() => {
		return () => {
			dispatch(cleanUpAquariumStatus());
		};
	}, [dispatch]);

	useEffect(() => {
		if (aquariumsStatus.createAquarium === SUCCESS) {
			notification.success({
				message: 'Sukces',
				description: 'Akwarium zostało stworzone'
			});
		}
	}, [dispatch, aquariumsStatus.createAquarium]);

	return (
		<React.Fragment>
			<Row justify='center'>
				<Col span={24}>
					<Row justify='center'>
						<Col>
							<PageHeader title={'Tworzenie akwarium'} />
						</Col>
					</Row>
					<CreateAquariumForm
						loading={aquariumsStatus.createAquarium === LOADING}
						onFinish={handleFormSubmit}
					/>
				</Col>
			</Row>
		</React.Fragment>
	);
};

export default CreateAquariumContainer;
