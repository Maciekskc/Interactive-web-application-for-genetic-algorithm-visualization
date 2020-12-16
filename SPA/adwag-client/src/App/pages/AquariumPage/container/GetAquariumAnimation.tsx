import React, { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';
import { HubData } from '../components/HubTransferedDataInterfaces';
import { AnimationCanvas } from '../components/AnimationCanvas';
import { RouteComponentProps, useHistory } from 'react-router';
import { Button, Col, Row } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { AnimationPopulationList } from '../components/AnimationPopulationList';

let aquariumId: string = '1';

let hubdata1: HubData;

interface RouteParams {
	aquariumId: string;
}

interface GetAquariumAnimationProps extends RouteComponentProps<RouteParams> {}

const GetAquariumAnimation: React.FC<GetAquariumAnimationProps> = ({ match }: GetAquariumAnimationProps) => {
	aquariumId = match.params.aquariumId;
	const [hubdata, sethubdata] = useState<HubData>(hubdata1);
	const hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:44303/aq').build();

	const history = useHistory();

	const startHubConnection = () => {
		hubConnection
			.start()
			.then(() => {
				hubConnection.invoke('JoinGroup', `aq-${aquariumId}`);
			})
			.catch(() => console.log('Error while trying to connect'));
	};

	const hubHandler = (data: HubData) => {
		sethubdata(data);
	};

	const startConnectionAq = () => {
		hubConnection.on(`TransferData`, (data: HubData) => hubHandler(data));
		startHubConnection();
	};

	const stopConnectionAq = () => {
		hubConnection
			.invoke('ExitGroup', `aq-${aquariumId}`)
			.then((data) => {
				console.log('Disconnected from hub');
			})
			.catch(() => console.log('Error while trying to disconnect'));
	};

	return (
		<>
			<Row justify='space-around'>
				<Button
					style={{ marginLeft: 16 }}
					onClick={() => history.push(`/aquariums/${aquariumId}`)}
					icon={<ArrowLeftOutlined />}
				>
					Wstecz
				</Button>
				<Button style={{ marginLeft: 16 }} onClick={() => history.push(`/fishes/create`)}>
					Dodaj Obiekt
				</Button>
				<Button onClick={startConnectionAq}> Rozpocznij Animacje </Button>
			</Row>
			<Row justify='end' style={{ marginTop: 10 }}>
				<Col span={16}>
					<AnimationCanvas hubdata={hubdata} />
				</Col>
				<Col span={4}>
					<AnimationPopulationList hubdata={hubdata} />
				</Col>
			</Row>
		</>
	);
};

export default GetAquariumAnimation;
