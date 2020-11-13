import { GetUsersRequest } from 'App/api/endpoints/admin/requests';
import { GetUserResponse } from 'App/api/endpoints/admin/responses';
import { UserForGetUsersResponse } from 'App/api/endpoints/admin/responses/getUsersResponse';
import { GetFishesFromAquariumRequest } from 'App/api/endpoints/fish/requests/getFishesFromAquariumRequest';
import { GetUserFishesRequest } from 'App/api/endpoints/fish/requests/getUserFishesRequest';
import { FishForGetFishesFromAquariumResponse } from 'App/api/endpoints/fish/responses/getFishesFromAquariumResponse';
import { GetFishResponse } from 'App/api/endpoints/fish/responses/getFishResponse';
import { FishForGetUserFishesResponse } from 'App/api/endpoints/fish/responses/getUserFishesResponse';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { StatusType } from 'App/types/requestStatus';

const { INITIAL } = StatusType;

export interface FishesState {
	status: {
		getFishesFromAquarium: StatusType;
		getFish: StatusType;
		getUserFishes: StatusType;
		createFish: StatusType;
		killFish: StatusType;
		// updateUser: StatusType;
	};
	error: string[];
	fishesFromAquarium: FishForGetFishesFromAquariumResponse[];
	userFishes: FishForGetUserFishesResponse[];
	getFishesFromAquiariumParams: GetFishesFromAquariumRequest;
	getUserFishesParams: GetUserFishesRequest;
	getFishesTotalPages: number;
	selectedFish: GetFishResponse | null;
}

export const fishesInitialState: FishesState = {
	status: {
		getFishesFromAquarium: INITIAL,
		getFish: INITIAL,
		getUserFishes: INITIAL,
		createFish: INITIAL,
		killFish: INITIAL
		// updateUser: INITIAL
	},
	error: null,
	fishesFromAquarium: [],
	userFishes: [],
	getFishesFromAquiariumParams: defaultPageQueryParams,
	getUserFishesParams: defaultPageQueryParams,
	getFishesTotalPages: 0,
	selectedFish: null
};
