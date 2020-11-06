export interface GetFishResponse {
	Name: string;
	IsAlive: boolean;
	AquariumId: number;

	PhysicalStatistic: PhysicalStatisticForGetFishResponse;
	LifeParameters: LifeParametersForGetFishResponse;
	SetOfMutations: SetOfMutationsForGetFishResponse;
	LifeTimeStatistic: LifeTimeStatisticForGetFishResponse;

	Parent1: ParentOfFishForGetFishResponse | null;
	Parent2: ParentOfFishForGetFishResponse | null;
}

export interface PhysicalStatisticForGetFishResponse {
	X: number;
	Y: number;
	V: number;
	Vx: number;
	Vy: number;
	Color: string;
	VisionAngle: number;
	VisionRange: number;
}

export interface SetOfMutationsForGetFishResponse {
	Predator: boolean;
	HungryCharge: boolean;
}

export interface LifeParametersForGetFishResponse {
	Hunger: number;
	LastHungerUpdate: Date;
	Vitality: number;
}

export interface LifeTimeStatisticForGetFishResponse {
	BirthDate: Date;
	DeathDate: Date;

	FoodCollected: number;
	DistanceSwimmed: number;
	Descendants: number;
}

export interface ParentOfFishForGetFishResponse {
	Id: number;
	Name: string;
	IsAlive: boolean;
	Color: string;
}
