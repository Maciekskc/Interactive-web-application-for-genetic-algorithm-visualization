import {
	getFishesFromAquariumStart,
	getFishesFromAquariumSuccess,
	getFishesFromAquariumFailure,
	getUserFishesStart,
	getUserFishesSuccess,
	getUserFishesFailure,
	getFishStart,
	getFishSuccess,
	getFishFailure,
	killFishStart,
	killFishSuccess,
	killFishFailure,
	createFishStart,
	createFishSuccess,
	createFishFailure
} from './fish.slice';
import { AppThunk } from 'App/state/store';
import agent from 'App/api/agent/agent';
import { GetFishesFromAquariumRequest } from 'App/api/endpoints/fish/requests/getFishesFromAquariumRequest';
import { GetUserFishesRequest } from 'App/api/endpoints/fish/requests/getUserFishesRequest';
import { CreateFishRequest } from 'App/api/endpoints/fish/requests/CreateFishRequest';

export const getFishesFromAquarium = (params: GetFishesFromAquariumRequest, aquariumId: string): AppThunk => async (
	dispatch
) => {
	dispatch(getFishesFromAquariumStart());
	console.log(params);
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

export const killFish = (fishId: string): AppThunk => async (dispatch) => {
	dispatch(killFishStart());
	agent.Fish.killFish(fishId)
		.then(() => dispatch(killFishSuccess(fishId)))
		.catch((error) => dispatch(killFishFailure(error)));
};

export const createFish = (fishToCreate: CreateFishRequest): AppThunk => async (dispatch) => {
	dispatch(createFishStart());
	agent.Fish.createFish(fishToCreate)
		.then(() => dispatch(createFishSuccess()))
		.catch((error) => dispatch(createFishFailure(error)));
};
