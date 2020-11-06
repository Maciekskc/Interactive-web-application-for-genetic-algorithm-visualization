import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';

import { AccountApi } from '../endpoints/account/accountApi';
import { AdminApi } from '../endpoints/admin/adminApi';
import { LogsApi } from '../endpoints/logs/logsApi';
import { AuthApi } from '../endpoints/auth/authApi';
import { baseURL } from './axios/configuration';

const responseBodyAxios = (response: AxiosResponse) => {
	if (response.data && 'data' in response.data && Object.keys(response.data).length === 1) {
		return response.data.data;
	}

	return response.data;
};

const responseBodyFetch = (response: Response) => response.json();

export const requests = {
	get: (url: string, params?: {}) =>
		axios
			.get(url, {
				params
			})
			.then(responseBodyAxios),
	post: (url: string, body: {}, config?: AxiosRequestConfig | undefined) =>
		axios.post(url, body, config).then(responseBodyAxios),
	put: (url: string, body: {}, config?: AxiosRequestConfig | undefined) =>
		axios.put(url, body, config).then(responseBodyAxios),
	delete: (url: string) => axios.delete(url).then(responseBodyAxios),
	fetch: (url: string, body: BodyInit | null, config?: RequestInit | undefined) =>
		fetch(`${baseURL}${url}`, { ...config, body, method: 'post' }).then(responseBodyFetch),
	download: (url: string, fileName: string) =>
		axios({
			url: url,
			method: 'GET',
			responseType: 'blob'
		}).then((response) => {
			const url = window.URL.createObjectURL(new Blob([response.data]));
			const link = document.createElement('a');
			link.href = url;
			link.setAttribute('download', fileName);
			document.body.appendChild(link);
			link.click();
			document.body.removeChild(link);
		})
};

export default {
	Account: AccountApi,
	Auth: AuthApi,
	Logs: LogsApi,
	Admin: AdminApi
};
