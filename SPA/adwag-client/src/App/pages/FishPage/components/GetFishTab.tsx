import { Row, Col, Card, Typography, Divider } from 'antd';
import { GetFishResponse } from 'App/api/endpoints/fish/responses/getFishResponse';
import { cleanUpSelectedFish } from 'App/state/fish/fish.slice';
import React from 'react';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router';
import { Link } from 'react-router-dom';
import p5Types from 'p5';
import Sketch from 'react-p5';

const { Title } = Typography;

interface GetFishTabsProps {
	fish: GetFishResponse;
}
export const GetFishTabs: React.FC<GetFishTabsProps> = ({ fish }) => {
	const dispatch = useDispatch();
	const timeAlive = new Date(
		(fish.isAlive ? new Date(fish.lifeTimeStatistic.deathDate).valueOf() : new Date().valueOf()) -
			new Date(fish.lifeTimeStatistic.birthDate).valueOf()
	)
		.toTimeString()
		.substr(0, 8);

	const setupFish = (p5: p5Types, canvasParentRef: Element) => {
		p5.createCanvas(600, 300).parent(canvasParentRef);
	};
	const drawFish = (p5: p5Types) => {
		try {
			p5.push();
			p5.translate(250, 150);
			p5.fill(p5.color(fish.physicalStatistic.color));
			p5.noStroke();
			p5.triangle(0, 0, -200, 70, -200, -70);

			const mutationLinesColor = fish.setOfMutations.predator
				? p5.stroke(255, 0, 0)
				: fish.setOfMutations.hungryCharge
				? p5.stroke(0, 0, 255)
				: p5.noStroke();
			const mutationLine1 =
				fish.setOfMutations.predator || fish.setOfMutations.hungryCharge ? p5.line(0, 0, -200, 30) : null;
			const mutationLine2 =
				fish.setOfMutations.predator || fish.setOfMutations.hungryCharge ? p5.line(0, 0, -200, 0) : null;
			const mutationLine3 =
				fish.setOfMutations.predator || fish.setOfMutations.hungryCharge ? p5.line(0, 0, -200, -30) : null;

			p5.noStroke();
			p5.ellipse(0, 0, 300, 100);
			p5.pop();
		} catch (e) {}
	};

	const setupParent = (p5: p5Types, canvasParentRef: Element) => {
		p5.createCanvas(150, 70).parent(canvasParentRef);
	};
	const drawParent1 = (p5: p5Types) => {
		try {
			p5.push();
			p5.translate(55, 30);
			p5.fill(p5.color(fish.parent1.color));
			p5.noStroke();
			p5.triangle(0, 0, -70, 21, -70, -21);
			p5.ellipse(0, 0, 100, 33);
			p5.pop();
		} catch (e) {}
	};
	const drawParent2 = (p5: p5Types) => {
		try {
			p5.push();
			p5.translate(55, 30);
			p5.fill(p5.color(fish.parent2.color));
			p5.noStroke();
			p5.triangle(0, 0, -70, 21, -70, -21);
			p5.ellipse(0, 0, 100, 33);
			p5.pop();
		} catch (e) {}
	};

	return (
		<Row
			style={{
				padding: '90 0 0  0'
			}}
			justify='center'
		>
			<Card style={{ width: '70rem', height: '47rem', boxShadow: '1px 2px 2px #9E9E9E' }}>
				<Row>
					<Col span={12} style={{ textAlign: 'center' }}>
						<Title level={2}>ID: {fish.id}</Title>
						<Sketch setup={setupFish} draw={drawFish} />;
					</Col>
					<Col span={12}>
						<Row style={{ justifyContent: 'center', margin: '16px' }}>
							<Title level={2}>{fish.name}</Title>
						</Row>
						<Row justify='space-between'>
							<Col span={10} style={{ width: '15rem', height: '20rem' }}>
								<Card
									title='Statystyki'
									style={{ width: '15rem', height: '15rem', boxShadow: '1px 2px 2px #9E9E9E' }}
								>
									<p>Kąt widzenia: {fish.physicalStatistic.visionAngle}°</p>
									<p>Zasięg widzenia:{fish.physicalStatistic.visionRange}</p>
									<p>Prędkość: {fish.physicalStatistic.v}</p>
								</Card>
							</Col>
							<Col span={10} style={{ width: '12rem', height: '20rem' }}>
								<Card
									title='Pozycja'
									style={{ width: '12rem', height: '15rem', boxShadow: '1px 2px 2px #9E9E9E' }}
								>
									<Row justify='space-between'>
										<p>X: {fish.physicalStatistic.x}</p>
										<p>Y:{fish.physicalStatistic.y}</p>
									</Row>
									<Row justify='space-between'>
										<p>vx: {fish.physicalStatistic.vx}</p>
										<p>vy: {fish.physicalStatistic.y}</p>
									</Row>
								</Card>
							</Col>
						</Row>
					</Col>
				</Row>
				<Row justify='space-between'>
					<Col
						span={12}
						title='Pozycja'
						style={{ width: '35rem', height: '20rem', justifyContent: 'center' }}
					>
						<Card
							title='Parametry'
							style={{ width: '30rem', height: '18rem', boxShadow: '1px 2px 2px #9E9E9E' }}
						>
							<p>NAJEDZENIE: {fish.lifeParameters.hunger}</p>
							<p>CZAS ŻYCIA: {timeAlive}</p>
							<p>ZEBRANE POŻYWIENIE: {fish.lifeTimeStatistic.foodCollected}</p>
							<p>DRAPIEŻNIK: {fish.setOfMutations.predator === true ? 'TAK' : 'NIE'}</p>
							<p>SZARŻA: {fish.setOfMutations.hungryCharge === true ? 'TAK' : 'NIE'}</p>
							<p>ŻYJE: {fish.isAlive === true ? 'TAK' : 'NIE'}</p>
						</Card>
					</Col>
					<Col span={12} style={{ width: '35rem', height: '20rem' }}>
						<Card
							title='Rodzice'
							style={{ width: '30rem', height: '18rem', boxShadow: '1px 2px 2px #9E9E9E' }}
						>
							<Row justify='space-around'>
								<Col
									span={10}
									style={{
										textAlign: 'center',
										padding: '2.5rem'
									}}
								>
									{fish.parent1 ? (
										<>
											<Link
												to={`/fishes/${fish.parent1.id}`}
												onClick={() => dispatch(cleanUpSelectedFish())}
											>
												<Sketch setup={setupParent} draw={drawParent1} />
											</Link>
											<p>{fish.parent1.name}</p>
										</>
									) : (
										<p>Brak Rodzica1</p>
									)}
								</Col>
								<Divider type='vertical' />
								<Col
									span={10}
									style={{
										textAlign: 'center',
										padding: '2.5rem'
									}}
								>
									{fish.parent2 ? (
										<>
											<Link
												to={`/fishes/${fish.parent2.id}`}
												onClick={() => dispatch(cleanUpSelectedFish())}
											>
												<Sketch setup={setupParent} draw={drawParent2} />
											</Link>
											<p>{fish.parent2.name}</p>
										</>
									) : (
										<p>Brak Rodzica2</p>
									)}
								</Col>
							</Row>
						</Card>
					</Col>
				</Row>
			</Card>
		</Row>
	);
};
