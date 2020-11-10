import { GetAquariumsRequest } from 'App/api/endpoints/aquarium/requests/getAquariumsRequest';
import { GetAquariumResponse } from 'App/api/endpoints/aquarium/responses/getAquariumResponse';
import { AquariumForGetAquariumsResponse } from 'App/api/endpoints/aquarium/responses/getAquariumsResponse';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import StatusType from 'App/types/requestStatus';

const { INITIAL } = StatusType;

export interface AquariumState {
	status: {
		getAquariums: StatusType;
		getAquarium: StatusType;
		// deleteUser: StatusType;
		// createUser: StatusType;
		// updateUser: StatusType;
	};
	error: string[];
	aquariums: AquariumForGetAquariumsResponse[];
	getAquariumsParams: GetAquariumsRequest;
	getAquariumsTotalPages: number;
	selectedAquarium: GetAquariumResponse | null;
}

export const aquariumInitialState: AquariumState = {
	status: {
		getAquariums: INITIAL,
		getAquarium: INITIAL
		// deleteUser: INITIAL,
		// createUser: INITIAL,
		// updateUser: INITIAL
	},
	error: null,
	aquariums: [],
	getAquariumsParams: defaultPageQueryParams,
	selectedAquarium: null,
	getAquariumsTotalPages: 0
};
