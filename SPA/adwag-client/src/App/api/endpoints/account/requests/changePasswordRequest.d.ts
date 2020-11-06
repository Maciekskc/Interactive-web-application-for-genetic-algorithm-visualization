export interface ChangePasswordRequest {
	newPassword: string;
	currentPassword: string;
	confirmNewPassword: string;
}
