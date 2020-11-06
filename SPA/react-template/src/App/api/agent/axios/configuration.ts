import axios from 'axios';
import { errorHandler } from '../errorHandling/errorHandler';

export const baseURL = `https://localhost:44303/api/`;
axios.defaults.baseURL = baseURL;

axios.interceptors.request.use(
	(config) => {
		// token interceptor goes here
		const token = localStorage.getItem('token');
		if (token)
			config.headers.Authorization = `Bearer ${token}`;
		return config;
	},
	(error) => {
		console.log(error);
		return Promise.reject(error);
	}
);

axios.interceptors.response.use(undefined, errorHandler);
