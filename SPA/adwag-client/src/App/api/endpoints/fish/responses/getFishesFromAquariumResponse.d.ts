import { ICollectionResponse } from 'App/types/pagination/pagination';

export interface GetFishesFromAquariumResponse extends ICollectionResponse<FishForGetFishesFromAquariumResponse> {}

export interface FishForGetFishesFromAquariumResponse {
	id: number;
	name: string;
	physicalStatistic: PhysicalStatsForFishForGetFishFromAquariumResponse;
	lifeParameters: LifeParametersForFishForGetFishFromAquariumResponse;
	lifeTimeStatistic: LifeTimeStatisticForFishForGetFishFromAquariumResponse;
}

export interface LifeTimeStatisticForFishForGetFishFromAquariumResponse {
	BirthDate: Date;
}

export interface LifeParametersForFishForGetFishFromAquariumResponse {
	Hunger: number;
}

export interface PhysicalStatsForFishForGetFishFromAquariumResponse {
	V: number;
}
