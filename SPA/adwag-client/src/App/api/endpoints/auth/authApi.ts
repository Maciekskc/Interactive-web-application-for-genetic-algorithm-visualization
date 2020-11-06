import appConfig from 'app.config';
import { requests } from '../../agent/agent';
import { LoginRequest, RegisterRequest } from './requests';
import { LoginResponse, RegisterResponse, RefreshTokenResponse } from './responses';

const { urlToIncludeInEmail } = appConfig;

export const AuthApi = {
	login: (body: LoginRequest): Promise<LoginResponse> => requests.post(`/auth/login`, body),

	register: (body: RegisterRequest): Promise<RegisterResponse> =>
		requests.post(`/auth/register`, { ...body, urlToIncludeInEmail }),

	refreshToken: (): Promise<RefreshTokenResponse> => requests.fetch('/auth/refresh-token', null, {})
};
