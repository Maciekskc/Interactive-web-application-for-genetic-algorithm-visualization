import { ICollectionResponse } from 'App/types/pagination/pagination';

export interface GetUsersResponse extends ICollectionResponse<UserForGetUsersResponse> {}

export interface UserForGetUsersResponse {
	id: string;
	firstName: string;
	lastName: string;
	email: string;
	emailConfirmed: boolean;
	isDeleted: boolean;
	lockoutEnabled: true;
	lockoutEnd: string;
	accessFailedCount: number;
	phoneNumber: string;
	phoneNumberConfirmed: boolean;
	twoFactorEnabled: boolean;
	userName: string;
	roles: Role[];
}
