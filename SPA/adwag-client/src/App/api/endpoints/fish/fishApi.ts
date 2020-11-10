import { requests } from '../../agent/agent';
import { HttpStatusCodeResponse } from 'App/types/httpResponse.d';

import appConfig from 'app.config';
import { GetFishResponse } from './responses/getFishResponse';
import { GetFishesFromAquariumRequest } from './requests/getFishesFromAquariumRequest';
import { GetFishesFromAquariumResponse } from './responses/getFishesFromAquariumResponse';
import { GetUserFishesRequest } from './requests/getUserFishesRequest';
import { GetUserFishesResponse } from './responses/getUserFishesResponse';

const { urlToIncludeInEmail } = appConfig;

export const FishApi = {
	getFish: (fishId: string): Promise<GetFishResponse> => requests.get(`/fish/${fishId}`),

	getFishesFromAquarium: (
		paramas: GetFishesFromAquariumRequest,
		aquariumId: string
	): Promise<GetFishesFromAquariumResponse> => requests.get(`/fish/aquarium/${aquariumId}`),

	getUserFishes: (paramas: GetUserFishesRequest): Promise<GetUserFishesResponse> => requests.get(`/get-user-fishes`)

	// createUser: (body: CreateUserRequest): Promise<CreateUserResponse> =>
	// 	requests.post(`/admin/users`, { ...body, urlToIncludeInEmail }),

	// updateUser: (userId: string, body: UpdateUserRequest): Promise<UpdateUserResponse> =>
	// 	requests.put(`/admin/users/${userId}`, body),

	// deleteUser: (userId: string): Promise<HttpStatusCodeResponse> => requests.delete(`/admin/users/${userId}`)
};
