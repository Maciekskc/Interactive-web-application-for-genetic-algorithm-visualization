import { ICollectionResponse } from 'App/types/pagination/pagination';

export interface GetUserFishesResponse extends ICollectionResponse<FishForGetUserFishesResponse> {}

export interface FishForGetUserFishesResponse {
	id: number;
	name: string;
	isAlive: boolean;
}
