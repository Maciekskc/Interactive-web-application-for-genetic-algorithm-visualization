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
	birthDate: Date;
}

export interface LifeParametersForFishForGetFishFromAquariumResponse {
	hunger: number;
}

export interface PhysicalStatsForFishForGetFishFromAquariumResponse {
	v: number;
}
