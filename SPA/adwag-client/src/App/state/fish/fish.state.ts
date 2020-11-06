import { GetUsersRequest } from 'App/api/endpoints/admin/requests';
import { GetUserResponse } from 'App/api/endpoints/admin/responses';
import { UserForGetUsersResponse } from 'App/api/endpoints/admin/responses/getUsersResponse';
import { GetFishResponse } from 'App/api/endpoints/fish/responses/getFishResponse';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { StatusType } from 'App/types/requestStatus';

const { INITIAL } = StatusType;

export interface FishesState {
	status: {
		// getUsers: StatusType;
		getFish: StatusType;
		// deleteUser: StatusType;
		// createUser: StatusType;
		// updateUser: StatusType;
	};
	error: string[];
	//fishes: FishForGetFishesResponse[];
	//getFishesParams: GetFishesRequest;
	getFishesTotalPages: number;
	selectedFish: GetFishResponse | null;
}

export const fishesInitialState: FishesState = {
	status: {
		// getUsers: INITIAL,
		getFish: INITIAL
		// deleteUser: INITIAL,
		// createUser: INITIAL,
		// updateUser: INITIAL
	},
	error: null,
	//users: [],
	// getUsersParams: defaultPageQueryParams,
	getFishesTotalPages: 0,
	selectedFish: null
};
