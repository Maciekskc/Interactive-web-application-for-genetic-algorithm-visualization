import React, { useState } from 'react';
import p5Types from 'p5';
import { Fish, Food, HubData } from './HubTransferedDataInterfaces';
import Sketch from 'react-p5';
import { Card, Col } from 'antd';
import NumberFormat from 'react-number-format';
import { useHistory } from 'react-router';

interface PropsIterface {
	hubdata: HubData;
}

export const AnimationPopulationList: React.FC<PropsIterface> = (props: PropsIterface) => {
	const history = useHistory();

	const setupCanvas = (p5: p5Types, canvasParentRef: Element) => {
		p5.createCanvas(60, 20).parent(canvasParentRef);
	};
	let fishIndex = 0;

	const drawMiniature = (p5: p5Types) => {
		try {
			p5.push();
			p5.translate(30, 10);
			p5.fill(p5.color(props.hubdata.fishes[fishIndex].physicalStatistic.color));
			p5.noStroke();
			p5.triangle(0, 0, -23, 7, -23, -7);
			props.hubdata.fishes[fishIndex].predator
				? p5.stroke(255, 0, 0)
				: props.hubdata.fishes[fishIndex].hungryCharge
				? p5.stroke(0, 0, 255)
				: p5.noStroke();
			p5.line(0, 0, -23, 5);
			p5.line(0, 0, -23, 0);
			p5.line(0, 0, -23, -5);
			p5.noStroke();
			p5.ellipse(0, 0, 30, 11);
			p5.pop();

			fishIndex++;
			if (fishIndex === props.hubdata.fishes.length) {
				fishIndex = 0;
			}
		} catch (e) {}
	};

	return (
		<>
			{props.hubdata !== undefined ? (
				<Col span={23} style={{ maxHeight: '50rem', overflow: 'auto' }}>
					{props.hubdata.fishes.map((fish) => {
						return (
							<Card
								style={{ textAlign: 'center', maxHeight: '6.5rem', marginBottom: '1rem' }}
								onClick={() => history.push(`/fishes/${fish.id}`)}
							>
								<Sketch setup={setupCanvas} draw={drawMiniature} />
								{fish.name},
								<p>
									X=
									<NumberFormat
										value={fish.physicalStatistic.x}
										displayType={'text'}
										format='#####'
									/>{' '}
									| Y=
									<NumberFormat
										value={fish.physicalStatistic.y}
										displayType={'text'}
										format='#####'
									/>
								</p>
							</Card>
						);
					})}
				</Col>
			) : (
				''
			)}
		</>
	);
};
