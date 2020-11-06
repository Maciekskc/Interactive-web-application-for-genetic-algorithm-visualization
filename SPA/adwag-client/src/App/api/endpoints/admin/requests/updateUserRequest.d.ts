export interface UpdateUserRequest {
	userId: string;
	firstName: string;
	lastName: string;
	emailConfirmed: boolean;
	phoneConfirmed: boolean;
	isDeleted: boolean;
	lockoutEnabled: boolean;
	lockoutEnd: string;
	roles: Role[];
}
