import { AppThunk } from 'App/state/store';
import {
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
} from './logs.slice';
import agent from 'App/api/agent/agent';

export const getLogsFiles = (): AppThunk => async (dispatch) => {
	dispatch(getLogsFilesStart());
	agent.Logs.getLogsFiles()
		.then((response) => dispatch(getLogsFilesSuccess(response)))
		.catch((error) => dispatch(getLogsFilesFailure(error)));
};

export const getLogsFileContent = (logName: string): AppThunk => async (dispatch) => {
	dispatch(getLogsFileContentStart());
	agent.Logs.getLogsFileContent(logName)
		.then((response) => dispatch(getLogsFileContentSuccess(response)))
		.catch((error) => dispatch(getLogsFileContentFailure(error)));
};

export const downloadLogsFile = (logName: string): AppThunk => async (dispatch) => {
	dispatch(downloadLogsFileStart());
	agent.Logs.downloadLogsFile(logName)
		.then(() => dispatch(downloadLogsFileSuccess()))
		.catch((error) => dispatch(downloadLogsFileFailure(error)));
};

export const downloadAllLogsFiles = (zipName: string): AppThunk => async (dispatch) => {
	dispatch(downloadAllLogsFilesStart());
	agent.Logs.downloadAllLogsFiles(zipName)
		.then(() => dispatch(downloadAllLogsFilesSuccess()))
		.catch((error) => dispatch(downloadAllLogsFilesFailure(error)));
};

export const cleanUpLogStatus = (): AppThunk => async (dispatch) => {
	dispatch(cleanUpLogStatusStart());
};
