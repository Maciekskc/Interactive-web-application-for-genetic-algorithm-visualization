import { requests } from '../../agent/agent';

import appConfig from 'app.config';
import { GetAquariumResponse } from './responses/getAquariumResponse';
import { GetAquariumsRequest } from './requests/getAquariumsRequest';
import { GetAquariumsResponse } from './responses/getAquariumsResponse';
import { HttpStatusCodeResponse } from 'App/types/httpResponse';
import CreateAquariumRequest from './requests/createAquariumRequest';
import UpdateAquariumRequest from './requests/updateAquariumRequest';

const { urlToIncludeInEmail } = appConfig;

export const AquariumApi = {
	getAquarium: (aquariumId: string): Promise<GetAquariumResponse> => requests.get(`/aquarium/${aquariumId}`),

	getAquariums: (params: GetAquariumsRequest): Promise<GetAquariumsResponse> =>
		requests.get('/aquarium/get-all-aquariums', params),

	createAquarium: (body: CreateAquariumRequest): Promise<GetAquariumResponse> =>
		requests.post(`/aquarium/create`, body),

	updateAquarium: (aquariumId: string, body: UpdateAquariumRequest): Promise<GetAquariumResponse> =>
		requests.put(`/aquarium/${aquariumId}/edit`, body),

	deleteAquarium: (aquariumId: string): Promise<HttpStatusCodeResponse> =>
		requests.delete(`/aquarium/${aquariumId}/remove`)
};
