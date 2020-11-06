export interface GetLogsFilesResponse {
	logs: LogForGetLogsFilesResponse[];
}

export interface LogForGetLogsFilesResponse {
	name: string;
	sizeInKb: number;
	date: Date;
}
