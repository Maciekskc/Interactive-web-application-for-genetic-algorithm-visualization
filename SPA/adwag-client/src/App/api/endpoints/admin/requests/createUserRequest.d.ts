export interface CreateUserRequest {
	firstName: string;
	lastName: string;
	email: string;
	password: string;
	roles: Role[];
	language: string;
}
