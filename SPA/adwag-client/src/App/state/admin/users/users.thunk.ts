import {
	getUsersStart,
	getUsersSuccess,
	getUsersFailure,
	getUserStart,
	getUserSuccess,
	getUserFailure,
	deleteUserStart,
	deleteUserSuccess,
	deleteUserFailure,
	createUserStart,
	createUserSuccess,
	createUserFailure,
	updateUserStart,
	updateUserSuccess,
	updateUserFailure,
} from './users.slice';
import { GetUsersRequest, CreateUserRequest, UpdateUserRequest } from 'App/api/endpoints/admin/requests';
import { AppThunk } from 'App/state/store';
import agent from 'App/api/agent/agent';

export const getUsers = (params: GetUsersRequest): AppThunk => async (dispatch) => {
	dispatch(getUsersStart());
	agent.Admin.getUsers(params)
		.then((response) => dispatch(getUsersSuccess(response)))
		.catch((error) => dispatch(getUsersFailure(error)));
};

export const getUser = (userId: string): AppThunk => async (dispatch) => {
	dispatch(getUserStart());
	agent.Admin.getUser(userId)
		.then((response) => dispatch(getUserSuccess(response)))
		.catch((error) => dispatch(getUserFailure(error)));
};

export const deleteUser = (userId: string): AppThunk => async (dispatch) => {
	dispatch(deleteUserStart());
	agent.Admin.deleteUser(userId)
		.then(() => dispatch(deleteUserSuccess(userId)))
		.catch((error) => dispatch(deleteUserFailure(error)));
};

export const createUser = (userToCreate: CreateUserRequest): AppThunk => async (dispatch) => {
	dispatch(createUserStart());
	agent.Admin.createUser(userToCreate)
		.then(() => dispatch(createUserSuccess()))
		.catch((error) => dispatch(createUserFailure(error)));
};

export const updateUser = (userId: string, userToUpdate: UpdateUserRequest): AppThunk => async (dispatch) => {
	dispatch(updateUserStart());
	agent.Admin.updateUser(userId, userToUpdate)
		.then((res) => dispatch(updateUserSuccess(res)))
		.catch((error) => dispatch(updateUserFailure(error)));
};