import { ICollectionResponse } from 'App/types/pagination/pagination';

export interface GetAquariumsResponse extends ICollectionResponse<AquariumForGetAquariumsResponse> {}

export interface AquariumForGetAquariumsResponse {
	id: string;
	width: number;
	height: number;

	capacity: number;
	foodMaximalAmount: number;
	currentPopulationCount: number;
	currentFoodsAmount: number;
}
