import { requests } from '../../agent/agent';
import { HttpStatusCodeResponse } from 'App/types/httpResponse.d';

import appConfig from 'app.config';
import { GetFishResponse } from './responses/getFishResponse';

const { urlToIncludeInEmail } = appConfig;

export const FishApi = {
	getFish: (fishId: string): Promise<GetFishResponse> => requests.get(`/fish/${fishId}`)

	// getUser: (userId: string): Promise<GetUserResponse> => requests.get(`/admin/users/${userId}`),

	// createUser: (body: CreateUserRequest): Promise<CreateUserResponse> =>
	// 	requests.post(`/admin/users`, { ...body, urlToIncludeInEmail }),

	// updateUser: (userId: string, body: UpdateUserRequest): Promise<UpdateUserResponse> =>
	// 	requests.put(`/admin/users/${userId}`, body),

	// deleteUser: (userId: string): Promise<HttpStatusCodeResponse> => requests.delete(`/admin/users/${userId}`)
};
