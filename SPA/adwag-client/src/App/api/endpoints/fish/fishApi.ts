import { requests } from '../../agent/agent';
import { HttpStatusCodeResponse } from 'App/types/httpResponse.d';

import appConfig from 'app.config';
import { GetFishResponse } from './responses/getFishResponse';
import { GetFishesFromAquariumRequest } from './requests/getFishesFromAquariumRequest';
import { GetFishesFromAquariumResponse } from './responses/getFishesFromAquariumResponse';
import { GetUserFishesRequest } from './requests/getUserFishesRequest';
import { GetUserFishesResponse } from './responses/getUserFishesResponse';
import { CreateFishRequest } from './requests/CreateFishRequest';

const { urlToIncludeInEmail } = appConfig;

export const FishApi = {
	getFish: (fishId: string): Promise<GetFishResponse> => requests.get(`/fish/${fishId}`),

	getFishesFromAquarium: (
		params: GetFishesFromAquariumRequest,
		aquariumId: string
	): Promise<GetFishesFromAquariumResponse> => requests.get(`/fish/aquarium/${aquariumId}`, params),

	getUserFishes: (params: GetUserFishesRequest): Promise<GetUserFishesResponse> =>
		requests.get(`/fish/get-user-fishes`, params),

	createFish: (body: CreateFishRequest): Promise<CreateFishRequest> => requests.post(`/fish/create`, { ...body }),

	// updateUser: (userId: string, body: UpdateUserRequest): Promise<UpdateUserResponse> =>
	// 	requests.put(`/admin/users/${userId}`, body),

	killFish: (fishId: string): Promise<HttpStatusCodeResponse> => requests.post(`/fish/kill/${fishId}`, null)
};
