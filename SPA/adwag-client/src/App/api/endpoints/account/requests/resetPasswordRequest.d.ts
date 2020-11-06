export interface ResetPasswordRequest {
	userId: string;
	passwordResetCode: string;
	newPassword: string;
	confirmNewPassword: string;
}
