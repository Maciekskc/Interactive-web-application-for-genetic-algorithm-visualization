import {
	getAquariumsStart,
	getAquariumsSuccess,
	getAquariumsFailure,
	getAquariumStart,
	getAquariumSuccess,
	getAquariumFailure,
	createAquariumFailure,
	createAquariumStart,
	createAquariumSuccess,
	updateAquariumFailure,
	updateAquariumStart,
	updateAquariumSuccess,
	deleteAquariumFailure,
	deleteAquariumStart,
	deleteAquariumSuccess
} from './aquarium.slice';
import agent from 'App/api/agent/agent';
import { GetAquariumsRequest } from 'App/api/endpoints/aquarium/requests/getAquariumsRequest';
import { AppThunk } from '../store';
import CreateAquariumRequest from 'App/api/endpoints/aquarium/requests/createAquariumRequest';
import UpdateAquariumRequest from 'App/api/endpoints/aquarium/requests/updateAquariumRequest';

export const getAquariums = (params: GetAquariumsRequest): AppThunk => async (dispatch) => {
	dispatch(getAquariumsStart());
	agent.Aquarium.getAquariums(params)
		.then((response) => dispatch(getAquariumsSuccess(response)))
		.catch((error) => dispatch(getAquariumsFailure(error)));
};

export const getAquarium = (aquariumId: string): AppThunk => async (dispatch) => {
	dispatch(getAquariumStart());
	agent.Aquarium.getAquarium(aquariumId)
		.then((response) => dispatch(getAquariumSuccess(response)))
		.catch((error) => dispatch(getAquariumFailure(error)));
};

export const deleteAquarium = (aquariumId: string): AppThunk => async (dispatch) => {
	dispatch(deleteAquariumStart());
	agent.Aquarium.deleteAquarium(aquariumId)
		.then(() => dispatch(deleteAquariumSuccess()))
		.catch((error) => dispatch(deleteAquariumFailure(error)));
};

export const createAquarium = (aquariumToCreate: CreateAquariumRequest): AppThunk => async (dispatch) => {
	dispatch(createAquariumStart());
	agent.Aquarium.createAquarium(aquariumToCreate)
		.then(() => dispatch(createAquariumSuccess()))
		.catch((error) => dispatch(createAquariumFailure(error)));
};

export const updateAquarium = (aquariumId: string, aquariumToUpdate: UpdateAquariumRequest): AppThunk => async (
	dispatch
) => {
	dispatch(updateAquariumStart());
	agent.Aquarium.updateAquarium(aquariumId, aquariumToUpdate)
		.then((response) => dispatch(updateAquariumSuccess(response)))
		.catch((error) => dispatch(updateAquariumFailure(error)));
};
