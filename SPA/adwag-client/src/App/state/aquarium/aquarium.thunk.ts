import {
	getAquariumsStart,
	getAquariumsSuccess,
	getAquariumsFailure,
	getAquariumStart,
	getAquariumSuccess,
	getAquariumFailure
} from './aquarium.slice';
import agent from 'App/api/agent/agent';
import { GetAquariumsRequest } from 'App/api/endpoints/aquarium/requests/getAquariumsRequest';
import { AppThunk } from '../store';

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
