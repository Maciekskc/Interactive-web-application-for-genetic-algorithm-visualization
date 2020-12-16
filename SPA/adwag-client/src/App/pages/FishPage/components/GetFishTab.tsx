import { Row, Col, Card, Typography, Divider, Tag } from 'antd';
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
		fish.isAlive
			? new Date(fish.lifeTimeStatistic.deathDate).valueOf() -
			  new Date(fish.lifeTimeStatistic.birthDate).valueOf()
			: new Date().valueOf() - new Date(fish.lifeTimeStatistic.birthDate).valueOf()
	).toLocaleTimeString();

	const dateFormat = (date: Date) => {
		return new Date(date).toLocaleDateString() + ', ' + new Date(date).toLocaleTimeString();
	};

	const hungaryTagColor = (hungaryLevel: number) => {
		if (hungaryLevel === 4) {
			return 'lime';
		}
		if (hungaryLevel > 3) {
			return 'green';
		}
		if (hungaryLevel > 2) {
			return 'yellow';
		}
		if (hungaryLevel > 1) {
			return 'orange';
		}
		return 'red';
	};

	const setupFish = (p5: p5Types, canvasParentRef: Element) => {
		p5.createCanvas(600, 230).parent(canvasParentRef);
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
			<Card className='card-style' style={{ width: '70rem', height: '47rem', marginTop: '2rem' }}>
				<Row>
					<Col span={12} style={{ textAlign: 'center' }}>
						<Title level={2}>ID: {fish.id}</Title>
						<Sketch setup={setupFish} draw={drawFish} />
						<Title level={2} style={{ color: fish.physicalStatistic.color }}>
							Kolor: {fish.physicalStatistic.color}
						</Title>
					</Col>
					<Col span={12}>
						<Row style={{ justifyContent: 'center', margin: '16px' }}>
							<Title level={2}>{fish.name}</Title>
						</Row>
						<Row justify='space-between'>
							<Col span={10} style={{ width: '15rem', height: '20rem' }}>
								<Card
									className='card-style'
									title='Statystyki'
									style={{ width: '15rem', height: '15rem', boxShadow: '1px 2px 2px #9E9E9E' }}
								>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											Kąt widzenia:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.visionAngle}°
										</Col>
									</Row>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={20} style={{ textAlign: 'left' }}>
											Zasięg widzenia:
										</Col>
										<Col span={4} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.visionRange}
										</Col>
									</Row>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											Prędkość:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.v}
										</Col>
									</Row>
								</Card>
							</Col>
							<Col span={10} style={{ width: '12rem', height: '20rem' }}>
								<Card
									className='card-style'
									title='Pozycja'
									style={{ width: '12rem', height: '15rem', boxShadow: '1px 2px 2px #9E9E9E' }}
								>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											X:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.x}
										</Col>
									</Row>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											Y:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.y}
										</Col>
									</Row>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											Vx:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.vx}
										</Col>
									</Row>
									<Row justify='space-between' style={{ marginBottom: '0.5rem' }}>
										<Col span={12} style={{ textAlign: 'left' }}>
											Vy:
										</Col>
										<Col span={12} style={{ textAlign: 'right' }}>
											{fish.physicalStatistic.vy}
										</Col>
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
						<Card className='card-style' title='Parametry' style={{ width: '30rem' }}>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									NAJEDZENIE:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									<Tag
										style={{ width: '4rem', textAlign: 'center' }}
										color={hungaryTagColor(fish.lifeParameters.hunger)}
									>
										{fish.lifeParameters.hunger} / 5.0
									</Tag>
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									CZAS ŻYCIA:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									{timeAlive}
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									ZEBRANE POŻYWIENIE:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									{fish.lifeTimeStatistic.foodCollected}
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									DRAPIEŻNIK:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									<Tag color={fish.setOfMutations.predator === true ? 'green' : 'red'}>
										{fish.setOfMutations.predator === true ? 'TAK' : 'NIE'}
									</Tag>
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									SZARŻA:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									<Tag color={fish.setOfMutations.hungryCharge === true ? 'green' : 'red'}>
										{fish.setOfMutations.hungryCharge === true ? 'TAK' : 'NIE'}
									</Tag>
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={12} style={{ textAlign: 'left' }}>
									ŻYJE:
								</Col>
								<Col span={12} style={{ textAlign: 'right' }}>
									<Tag color={fish.isAlive === true ? 'green' : 'red'}>
										{fish.isAlive === true ? 'TAK' : 'NIE'}
									</Tag>
								</Col>
							</Row>
							<Row justify='space-between'>
								<Col span={16} style={{ textAlign: 'left' }}>
									OSTATNIE ZJEDZONE POŻYWIENIE:
								</Col>
								<Col span={8} style={{ textAlign: 'right' }}>
									{dateFormat(fish.lifeParameters.lastHungerUpdate)}
								</Col>
							</Row>
						</Card>
					</Col>
					<Col span={12} style={{ width: '35rem', height: '20rem' }}>
						<Card
							className='card-style'
							title='Rodzice'
							style={{ width: '30rem', height: '16.25rem', boxShadow: '1px 2px 2px #9E9E9E' }}
						>
							<Row justify='space-around'>
								<Col
									span={10}
									style={{
										textAlign: 'center',
										padding: '1.5rem'
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
										padding: '1.5rem'
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
