import {
	getFishesFromAquariumStart,
	getFishesFromAquariumSuccess,
	getFishesFromAquariumFailure,
	getUserFishesStart,
	getUserFishesSuccess,
	getUserFishesFailure,
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
import { GetFishesFromAquariumRequest } from 'App/api/endpoints/fish/requests/getFishesFromAquariumRequest';
import { GetUserFishesRequest } from 'App/api/endpoints/fish/requests/getUserFishesRequest';

export const getFishesFromAquarium = (params: GetFishesFromAquariumRequest, aquariumId: string): AppThunk => async (
	dispatch
) => {
	dispatch(getFishesFromAquariumStart());
	agent.Fish.getFishesFromAquarium(params, aquariumId)
		.then((response) => dispatch(getFishesFromAquariumSuccess(response)))
		.catch((error) => dispatch(getFishesFromAquariumFailure(error)));
};

export const getUserFishes = (params: GetUserFishesRequest): AppThunk => async (dispatch) => {
	dispatch(getUserFishesStart());
	agent.Fish.getUserFishes(params)
		.then((response) => dispatch(getUserFishesSuccess(response)))
		.catch((error) => dispatch(getUserFishesFailure(error)));
};

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
