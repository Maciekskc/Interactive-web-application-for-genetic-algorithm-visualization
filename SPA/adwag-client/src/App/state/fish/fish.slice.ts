import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { FishesState, fishesInitialState } from './fish.state';
import { StatusType } from 'App/types/requestStatus';
import { GetFishResponse } from 'App/api/endpoints/fish/responses/getFishResponse';
import { GetFishesFromAquariumResponse } from 'App/api/endpoints/fish/responses/getFishesFromAquariumResponse';
import { GetUserFishesResponse } from 'App/api/endpoints/fish/responses/getUserFishesResponse';

const { FAILED, LOADING, SUCCESS } = StatusType;

export const fishesSlice = createSlice({
	name: 'fishes',
	initialState: fishesInitialState,
	reducers: {
		//pobieranie rybek z akwarium
		getFishesFromAquariumStart: (state: FishesState) => {
			state.status.getFishesFromAquarium = LOADING;
			state.error = null;
			state.fishesFromAquarium = [];
		},
		getFishesFromAquariumSuccess(state: FishesState, action: PayloadAction<GetFishesFromAquariumResponse>) {
			state.status.getFishesFromAquarium = SUCCESS;
			state.fishesFromAquarium = action.payload.data;
			state.getFishesFromAquiariumParams = action.payload;
		},
		getFishesFromAquariumFailure(state: FishesState, action: PayloadAction<string[]>) {
			state.status.getFishesFromAquarium = FAILED;
			state.error = action.payload;
		},
		//pobieranie rybek uzytkownika
		getUserFishesStart: (state: FishesState) => {
			state.status.getUserFishes = LOADING;
			state.error = null;
			state.userFishes = [];
		},
		getUserFishesSuccess(state: FishesState, action: PayloadAction<GetUserFishesResponse>) {
			state.status.getUserFishes = SUCCESS;
			state.userFishes = action.payload.data;
			state.getUserFishesParams = action.payload;
		},
		getUserFishesFailure(state: FishesState, action: PayloadAction<string[]>) {
			state.status.getUserFishes = FAILED;
			state.error = action.payload;
		},
		getFishStart: (state: FishesState) => {
			state.status.getFish = LOADING;
			state.error = null;
			state.selectedFish = null;
		},
		getFishSuccess(state: FishesState, action: PayloadAction<GetFishResponse>) {
			state.status.getFish = SUCCESS;
			state.selectedFish = action.payload;
		},
		getFishFailure(state: FishesState, action: PayloadAction<string[]>) {
			state.status.getFish = FAILED;
			state.error = action.payload;
		},
		killFishStart: (state: FishesState) => {
			state.status.killFish = LOADING;
			state.error = null;
		},
		killFishSuccess: (state: FishesState, action: PayloadAction<string>) => {
			state.status.killFish = SUCCESS;
			//state.users = state.users.filter((u) => u.id !== action.payload);
		},
		killFishFailure: (state: FishesState, action: PayloadAction<string[]>) => {
			state.status.killFish = FAILED;
			state.error = action.payload;
		},
		createFishStart: (state: FishesState) => {
			state.error = null;
			state.status.createFish = LOADING;
		},
		createFishSuccess: (state: FishesState) => {
			state.status.createFish = SUCCESS;
		},
		createFishFailure: (state: FishesState, action: PayloadAction<string[]>) => {
			state.status.createFish = FAILED;
			state.error = action.payload;
		},
		cleanUpFishStatus: (state: FishesState) => {
			state.status = fishesInitialState.status;
			state.error = fishesInitialState.error;
		},

		cleanUpSelectedFish: (state: FishesState) => {
			state.selectedFish = fishesInitialState.selectedFish;
		}
	}
});

export default fishesSlice;

export const {
	getFishStart,
	getFishSuccess,
	getFishFailure,
	getFishesFromAquariumStart,
	getFishesFromAquariumSuccess,
	getFishesFromAquariumFailure,
	getUserFishesStart,
	getUserFishesSuccess,
	getUserFishesFailure,
	killFishStart,
	killFishSuccess,
	killFishFailure,
	createFishStart,
	createFishSuccess,
	createFishFailure,
	cleanUpFishStatus,
	cleanUpSelectedFish
} = fishesSlice.actions;
