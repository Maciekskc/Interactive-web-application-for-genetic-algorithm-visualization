import { requests } from '../../agent/agent';
import { GetLogsFilesResponse } from './responses';

export const LogsApi = {
	getLogsFiles: (): Promise<GetLogsFilesResponse> => requests.get(`/logs`),

	getLogsFileContent: (logName: string): Promise<string[]> => requests.get(`/logs/${logName}`),

	downloadLogsFile: (logName: string): Promise<any> =>
		requests.download(`/logs/download/${logName}`, `${logName}.txt`),

	downloadAllLogsFiles: (zipName: string): Promise<any> => requests.download(`/logs/download/all`, `${zipName}.zip`)
};
