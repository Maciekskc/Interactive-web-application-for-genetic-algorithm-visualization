import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { AdminLogsState, adminLogsInitialState } from './logs.state';
import { GetLogsFilesResponse } from 'App/api/endpoints/logs/responses';
import { StatusType } from 'App/types/requestStatus';

const { FAILED, LOADING, SUCCESS } = StatusType;

export const adminLogsSlice = createSlice({
	name: 'admin-logs',
	initialState: adminLogsInitialState,
	reducers: {
		getLogsFilesStart: (state: AdminLogsState) => {
			state.status.getLogsFiles = LOADING;
			state.error = null;
			state.logs = [];
		},
		getLogsFilesSuccess(state: AdminLogsState, action: PayloadAction<GetLogsFilesResponse>) {
			state.status.getLogsFiles = SUCCESS;
			state.logs = action.payload.logs;
		},
		getLogsFilesFailure(state: AdminLogsState, action: PayloadAction<string[]>) {
			state.status.getLogsFiles = FAILED;
			state.error = action.payload;
		},

		getLogsFileContentStart: (state: AdminLogsState) => {
			state.status.getLogsFileContent = LOADING;
			state.error = null;
		},
		getLogsFileContentSuccess(state: AdminLogsState, action: PayloadAction<string[]>) {
			state.status.getLogsFileContent = SUCCESS;
			state.logContent = action.payload;
		},
		getLogsFileContentFailure(state: AdminLogsState, action: PayloadAction<string[]>) {
			state.status.getLogsFileContent = FAILED;
			state.error = action.payload;
		},

		downloadLogsFileStart: (state: AdminLogsState) => {
			state.status.downloadLogsFile = LOADING;
			state.error = null;
		},
		downloadLogsFileSuccess(state: AdminLogsState) {
			state.status.downloadLogsFile = SUCCESS;
		},
		downloadLogsFileFailure(state: AdminLogsState, action: PayloadAction<string[]>) {
			state.status.downloadLogsFile = FAILED;
			state.error = action.payload;
		},

		downloadAllLogsFilesStart: (state: AdminLogsState) => {
			state.status.downloadAllLogsFiles = LOADING;
			state.error = null;
		},
		downloadAllLogsFilesSuccess(state: AdminLogsState) {
			state.status.downloadAllLogsFiles = SUCCESS;
		},
		downloadAllLogsFilesFailure(state: AdminLogsState, action: PayloadAction<string[]>) {
			state.status.downloadAllLogsFiles = FAILED;
			state.error = action.payload;
		},

		cleanUpLogStatusStart: (state: AdminLogsState) => {
			state.error = null;
			state.status = adminLogsInitialState.status;
		}
	}
});

export default adminLogsSlice;

export const {
	getLogsFilesStart,
	getLogsFilesSuccess,
	getLogsFilesFailure,

	getLogsFileContentStart,
	getLogsFileContentSuccess,
	getLogsFileContentFailure,

	downloadLogsFileStart,
	downloadLogsFileSuccess,
	downloadLogsFileFailure,

	downloadAllLogsFilesStart,
	downloadAllLogsFilesSuccess,
	downloadAllLogsFilesFailure,

	cleanUpLogStatusStart
} = adminLogsSlice.actions;
