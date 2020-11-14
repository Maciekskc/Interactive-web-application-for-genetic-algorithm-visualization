import { ICollectionResponse } from 'App/types/pagination/pagination';

export interface GetUserFishesResponse extends ICollectionResponse<FishForGetUserFishesResponse> {}

export interface FishForGetUserFishesResponse {
	id: string;
	name: string;
	aquariumId: string;
	isAlive: boolean;
}
