import {
	// getUsersStart,
	// getUsersSuccess,
	// getUsersFailure,
	getFishStart,
	getFishSuccess,
	getFishFailure
	// deleteUserStart,
	// deleteUserSuccess,
	// deleteUserFailure,
	// createUserStart,
	// createUserSuccess,
	// createUserFailure,
	// updateUserStart,
	// updateUserSuccess,
	// updateUserFailure
} from './fish.slice';
import { AppThunk } from 'App/state/store';
import agent from 'App/api/agent/agent';

// export const getUsers = (params: GetUsersRequest): AppThunk => async (dispatch) => {
// 	dispatch(getUsersStart());
// 	agent.Admin.getUsers(params)
// 		.then((response) => dispatch(getUsersSuccess(response)))
// 		.catch((error) => dispatch(getUsersFailure(error)));
// };

export const getFish = (fishId: string): AppThunk => async (dispatch) => {
	dispatch(getFishStart());
	agent.Fish.getFish(fishId)
		.then((response) => dispatch(getFishSuccess(response)))
		.catch((error) => dispatch(getFishFailure(error)));
};

// export const deleteUser = (userId: string): AppThunk => async (dispatch) => {
// 	dispatch(deleteUserStart());
// 	agent.Admin.deleteUser(userId)
// 		.then(() => dispatch(deleteUserSuccess(userId)))
// 		.catch((error) => dispatch(deleteUserFailure(error)));
// };

// export const createUser = (userToCreate: CreateUserRequest): AppThunk => async (dispatch) => {
// 	dispatch(createUserStart());
// 	agent.Admin.createUser(userToCreate)
// 		.then(() => dispatch(createUserSuccess()))
// 		.catch((error) => dispatch(createUserFailure(error)));
// };

// export const updateUser = (userId: string, userToUpdate: UpdateUserRequest): AppThunk => async (dispatch) => {
// 	dispatch(updateUserStart());
// 	agent.Admin.updateUser(userId, userToUpdate)
// 		.then((res) => dispatch(updateUserSuccess(res)))
// 		.catch((error) => dispatch(updateUserFailure(error)));
// };
