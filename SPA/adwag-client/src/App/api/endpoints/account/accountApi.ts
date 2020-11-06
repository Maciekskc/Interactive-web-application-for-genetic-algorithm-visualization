import { requests } from '../../agent/agent';
import {
	UpdateProfileRequest,
	ChangePasswordRequest,
	ForgotPasswordRequest,
	ResetPasswordRequest,
	ConfirmEmailRequest,
	ResendConfirmationEmailRequest
} from './requests';
import { GetAccountDetailsResponse, UpdateAccountDetailsResponse } from './responses';

import appConfig from 'app.config';
import { HttpStatusCodeResponse } from 'App/types/httpResponse.d';

const { urlToIncludeInEmail } = appConfig;

export const AccountApi = {
	getAccountDetails: (): Promise<GetAccountDetailsResponse> => requests.get(`/account/details`),

	updateProfile: (body: UpdateProfileRequest): Promise<UpdateAccountDetailsResponse> =>
		requests.put(`/account/details`, body),

	changePassword: (body: ChangePasswordRequest): Promise<HttpStatusCodeResponse> =>
		requests.post(`/account/change-password`, body),

	forgotPassword: (body: ForgotPasswordRequest): Promise<HttpStatusCodeResponse> =>
		requests.post(`/account/forgot-password`, { ...body, urlToIncludeInEmail }),

	resetPassword: (body: ResetPasswordRequest): Promise<HttpStatusCodeResponse> =>
		requests.post(`/account/reset-password`, body),

	confirmEmail: (params: ConfirmEmailRequest): Promise<HttpStatusCodeResponse> =>
		requests.get('/account/confirm-email', params),

	resendConfirmationEmail: (params: ResendConfirmationEmailRequest): Promise<HttpStatusCodeResponse> =>
		requests.post('/account/resend-confirmation-email', { ...params, urlToIncludeInEmail })
};
