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

	const backGroundImageSetup = (p5: p5Types) => {
		p5.loadImage(
			'C:/Users/macie/OneDrive/Pulpit/Uczelnia/Praca inżynierska/Praca/SPA/adwag-client/src/App/pages/AquariumPage/images/aquarium.jpg',
			(img) => {
				p5.image(img, 0, 0);
			}
		);
	};

	const draw = (p5: p5Types) => {
		p5.background(139, 199, 211);
		try {
			//backGroundImageSetup(p5);
			props.hubdata.foods.map((item: Food) => {
				p5.fill(p5.color(255, 127, 80));
				let scaledFoodPositionX: number = (item.x * x) / props.hubdata.aquariumWidth;
				let scaledFoodPositionY: number = (item.y * y) / props.hubdata.aquariumHeight;
				p5.circle(scaledFoodPositionX, scaledFoodPositionY, 5);
			});

			props.hubdata.fishes.map((item: Fish) => {
				p5.push();
				p5.noStroke();
				let scaledFishPositionX: number = (item.physicalStatistic.x * x) / props.hubdata.aquariumWidth;
				let scaledFishPositionY: number = (item.physicalStatistic.y * y) / props.hubdata.aquariumHeight;
				p5.translate(scaledFishPositionX, scaledFishPositionY);
				p5.rotate(Math.atan2(item.physicalStatistic.vy, item.physicalStatistic.vx));

				p5.fill(p5.color(item.physicalStatistic.color));
				p5.noStroke();
				p5.triangle(0, 0, -20, 7, -20, -7);

				const mutationLinesColor = item.predator
					? p5.stroke(255, 0, 0)
					: item.hungryCharge
					? p5.stroke(0, 0, 255)
					: p5.noStroke();
				const mutationLine1 = item.predator || item.hungryCharge ? p5.line(0, 0, -20, 3) : null;
				const mutationLine2 = item.predator || item.hungryCharge ? p5.line(0, 0, -20, 0) : null;
				const mutationLine3 = item.predator || item.hungryCharge ? p5.line(0, 0, -20, -3) : null;

				p5.noStroke();
				p5.fill(p5.color(item.physicalStatistic.color));
				p5.ellipse(0, 0, 30, 10);
				p5.pop();
			});
		} catch (e) {}
	};

	return <Sketch setup={setup} draw={draw} />;
};
