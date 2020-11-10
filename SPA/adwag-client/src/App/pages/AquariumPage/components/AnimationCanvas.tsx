import React from 'react';
import p5Types from 'p5';
import { Fish, Food, HubData } from './HubTransferedDataInterfaces';
import Sketch from 'react-p5';

interface PropsIterface {
	hubdata: HubData;
}

export const AnimationCanvas: React.FC<PropsIterface> = (props: PropsIterface) => {
	const x = 1080;
	const y = 720;
	const setup = (p5: p5Types, canvasParentRef: Element) => {
		p5.createCanvas(x, y).parent(canvasParentRef);
	};

	const draw = (p5: p5Types) => {
		p5.background(139, 199, 211);
		try {
			props.hubdata.foods.map((item: Food) => {
				p5.fill(p5.color(255, 127, 80));
				let scaledFoodPositionX: number = (item.x * x) / props.hubdata.aquariumWidth;
				let scaledFoodPositionY: number = (item.y * y) / props.hubdata.aquariumHeight;
				p5.circle(scaledFoodPositionX, scaledFoodPositionY, 5);
			});

			props.hubdata.fishes.map((item: Fish) => {
				p5.push();
				p5.fill(p5.color(item.physicalStatistic.color));
				let scaledFishPositionX: number = (item.physicalStatistic.x * x) / props.hubdata.aquariumWidth;
				let scaledFishPositionY: number = (item.physicalStatistic.y * y) / props.hubdata.aquariumHeight;
				p5.translate(scaledFishPositionX, scaledFishPositionY);
				p5.rotate(Math.atan2(item.physicalStatistic.vy, item.physicalStatistic.vx));
				p5.ellipse(0, 0, 30, 10);
				p5.pop();
			});
		} catch (e) {}
	};

	return <Sketch setup={setup} draw={draw} />;
};
