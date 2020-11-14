export interface GetFishResponse {
	id: string;
	name: string;
	isAlive: boolean;
	AquariumId: number;

	physicalStatistic: PhysicalStatisticForGetFishResponse;
	lifeParameters: LifeParametersForGetFishResponse;
	setOfMutations: SetOfMutationsForGetFishResponse;
	lifeTimeStatistic: LifeTimeStatisticForGetFishResponse;

	parent1: ParentOfFishForGetFishResponse | null;
	parent2: ParentOfFishForGetFishResponse | null;
}

export interface PhysicalStatisticForGetFishResponse {
	x: number;
	y: number;
	v: number;
	vx: number;
	vy: number;
	color: string;
	visionAngle: number;
	visionRange: number;
}

export interface SetOfMutationsForGetFishResponse {
	predator: boolean;
	hungryCharge: boolean;
}

export interface LifeParametersForGetFishResponse {
	hunger: number;
	lastHungerUpdate: Date;
	vitality: number;
}

export interface LifeTimeStatisticForGetFishResponse {
	birthDate: Date;
	deathDate: Date;

	foodCollected: number;
	distanceSwimmed: number;
	descendants: number;
}

export interface ParentOfFishForGetFishResponse {
	id: string;
	name: string;
	isAlive: boolean;
	color: string;
}
