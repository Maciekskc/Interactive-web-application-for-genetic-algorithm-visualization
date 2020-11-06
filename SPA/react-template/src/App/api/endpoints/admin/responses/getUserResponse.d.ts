export interface GetUserResponse {
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
