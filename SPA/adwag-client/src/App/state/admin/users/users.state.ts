import { GetUsersRequest } from 'App/api/endpoints/admin/requests';
import { GetUserResponse } from 'App/api/endpoints/admin/responses';
import { UserForGetUsersResponse } from 'App/api/endpoints/admin/responses/getUsersResponse';
import defaultPageQueryParams from 'App/common/utils/defaultPageQueryParams';
import { StatusType } from 'App/types/requestStatus';

const { INITIAL } = StatusType;

export interface AdminUsersState {
	status: {
		getUsers: StatusType;
		getUser: StatusType;
		deleteUser: StatusType;
		createUser: StatusType;
		updateUser: StatusType;
	};
	error: string[];
	users: UserForGetUsersResponse[];
	getUsersParams: GetUsersRequest;
	getUsersTotalPages: number;
	selectedUser: GetUserResponse | null;
}

export const adminUsersInitialState: AdminUsersState = {
	status: {
		getUsers: INITIAL,
		getUser: INITIAL,
		deleteUser: INITIAL,
		createUser: INITIAL,
		updateUser: INITIAL
	},
	error: null,
	users: [],
	getUsersParams: defaultPageQueryParams,
	selectedUser: null,
	getUsersTotalPages: 0
};
