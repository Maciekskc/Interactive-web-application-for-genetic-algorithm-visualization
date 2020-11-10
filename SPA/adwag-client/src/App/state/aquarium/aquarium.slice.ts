import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { StatusType } from 'App/types/requestStatus';
import { aquariumInitialState, AquariumState } from './aquarium.state';
import { GetAquariumsResponse } from 'App/api/endpoints/aquarium/responses/getAquariumsResponse';
import { GetAquariumResponse } from 'App/api/endpoints/aquarium/responses/getAquariumResponse';

const { FAILED, LOADING, SUCCESS } = StatusType;

export const aquariumsSlice = createSlice({
	name: 'aquariums',
	initialState: aquariumInitialState,
	reducers: {
		//pobieranie rybek z akwarium
		getAquariumsStart: (state: AquariumState) => {
			state.status.getAquariums = LOADING;
			state.error = null;
			state.aquariums = [];
		},
		getAquariumsSuccess(state: AquariumState, action: PayloadAction<GetAquariumsResponse>) {
			state.status.getAquariums = SUCCESS;
			state.aquariums = action.payload.data;
			state.getAquariumsParams = action.payload;
		},
		getAquariumsFailure(state: AquariumState, action: PayloadAction<string[]>) {
			state.status.getAquariums = FAILED;
			state.error = action.payload;
		},
		getAquariumStart: (state: AquariumState) => {
			state.status.getAquarium = LOADING;
			state.error = null;
			state.selectedAquarium = null;
		},
		getAquariumSuccess(state: AquariumState, action: PayloadAction<GetAquariumResponse>) {
			state.status.getAquarium = SUCCESS;
			state.selectedAquarium = action.payload;
		},
		getAquariumFailure(state: AquariumState, action: PayloadAction<string[]>) {
			state.status.getAquarium = FAILED;
			state.error = action.payload;
		},
		cleanUpAquariumStatus: (state: AquariumState) => {
			state.status = aquariumInitialState.status;
			state.error = aquariumInitialState.error;
		},

		cleanUpSelectedAquarium: (state: AquariumState) => {
			state.selectedAquarium = aquariumInitialState.selectedAquarium;
		}
	}
});

export default aquariumsSlice;

export const {
	getAquariumFailure,
	getAquariumStart,
	getAquariumSuccess,
	getAquariumsFailure,
	getAquariumsStart,
	getAquariumsSuccess,
	cleanUpAquariumStatus,
	cleanUpSelectedAquarium
} = aquariumsSlice.actions;
