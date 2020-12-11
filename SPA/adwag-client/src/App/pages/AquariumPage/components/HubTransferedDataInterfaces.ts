export interface Physical_statistic {
	x: number;
	y: number;
	vx: number;
	vy: number;
	color: string;
}

export interface Fish {
	id: string;
	name: string;
	physicalStatistic: Physical_statistic;
	hungryCharge: boolean;
	predator: boolean;
}

export interface Food {
	x: number;
	y: number;
}

export interface HubData {
	aquariumWidth: number;
	aquariumHeight: number;
	fishes: Fish[];
	foods: Food[];
}
