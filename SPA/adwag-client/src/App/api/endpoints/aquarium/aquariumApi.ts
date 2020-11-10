import { requests } from '../../agent/agent';

import appConfig from 'app.config';
import { GetAquariumResponse } from './responses/getAquariumResponse';
import { GetAquariumsRequest } from './requests/getAquariumsRequest';
import { GetAquariumsResponse } from './responses/getAquariumsResponse';

const { urlToIncludeInEmail } = appConfig;

export const AquariumApi = {
	getAquarium: (aquariumId: string): Promise<GetAquariumResponse> => requests.get(`/aquarium/${aquariumId}`),

	getAquariums: (paramas: GetAquariumsRequest): Promise<GetAquariumsResponse> =>
		requests.get('aquarium/get-all-aquariums')

	// createUser: (body: CreateUserRequest): Promise<CreateUserResponse> =>
	// 	requests.post(`/admin/users`, { ...body, urlToIncludeInEmail }),

	// updateUser: (userId: string, body: UpdateUserRequest): Promise<UpdateUserResponse> =>
	// 	requests.put(`/admin/users/${userId}`, body),

	// deleteUser: (userId: string): Promise<HttpStatusCodeResponse> => requests.delete(`/admin/users/${userId}`)
};
