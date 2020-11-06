import { GetAccountDetailsResponse } from 'App/api/endpoints/account/responses';
import { LoginResponse } from 'App/api/endpoints/auth/responses';
import { StatusType } from 'App/types/requestStatus';

const { INITIAL } = StatusType;

export interface SessionState {
	status: {
		authentication: StatusType;
		getUserDetails: StatusType;
		devalidateSession: StatusType;
	};
	info: LoginResponse | null;
	isCorrectRole: boolean;
	error: string[];
	user: GetAccountDetailsResponse | null;
}

export const sessionInitialState: SessionState = {
	status: {
		authentication: INITIAL,
		getUserDetails: INITIAL,
		devalidateSession: INITIAL,
	},
	isCorrectRole: false,
	info: null,
	error: null,
	user: null
};
