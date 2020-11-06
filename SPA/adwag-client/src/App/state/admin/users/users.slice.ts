import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { adminUsersInitialState, AdminUsersState } from './users.state';
import { StatusType } from 'App/types/requestStatus';
import { GetUserResponse, GetUsersResponse, UpdateUserResponse } from 'App/api/endpoints/admin/responses';

const { FAILED, LOADING, SUCCESS } = StatusType;

export const adminUsersSlice = createSlice({
	name: 'admin-users',
	initialState: adminUsersInitialState,
	reducers: {
		getUsersStart: (state: AdminUsersState) => {
			state.status.getUsers = LOADING;
			state.error = null;
			state.users = [];
		},
		getUsersSuccess(state: AdminUsersState, action: PayloadAction<GetUsersResponse>) {
			state.status.getUsers = SUCCESS;
			state.users = action.payload.data;
			state.getUsersParams = action.payload;
		},
		getUsersFailure(state: AdminUsersState, action: PayloadAction<string[]>) {
			state.status.getUsers = FAILED;
			state.error = action.payload;
		},
		getUserStart: (state: AdminUsersState) => {
			state.status.getUser = LOADING;
			state.error = null;
			state.selectedUser = null;
		},
		getUserSuccess: (state: AdminUsersState, action: PayloadAction<GetUserResponse>) => {
			state.status.getUser = SUCCESS;
			state.selectedUser = action.payload;
		},
		getUserFailure: (state: AdminUsersState, action: PayloadAction<string[]>) => {
			state.status.getUser = FAILED;
			state.error = action.payload;
		},
		deleteUserStart: (state: AdminUsersState) => {
			state.status.deleteUser = LOADING;
			state.error = null;
		},
		deleteUserSuccess: (state: AdminUsersState, action: PayloadAction<string>) => {
			state.status.deleteUser = SUCCESS;
			state.users = state.users.filter((u) => u.id !== action.payload);
		},
		deleteUserFailure: (state: AdminUsersState, action: PayloadAction<string[]>) => {
			state.status.deleteUser = FAILED;
			state.error = action.payload;
		},
		createUserStart: (state: AdminUsersState) => {
			state.error = null;
			state.status.createUser = LOADING;
		},
		createUserSuccess: (state: AdminUsersState) => {
			state.status.createUser = SUCCESS;
		},
		createUserFailure: (state: AdminUsersState, action: PayloadAction<string[]>) => {
			state.status.createUser = FAILED;
			state.error = action.payload;
		},
		updateUserStart: (state: AdminUsersState) => {
			state.status.updateUser = LOADING;
			state.error = null;
		},
		updateUserSuccess: (state: AdminUsersState, action: PayloadAction<UpdateUserResponse>) => {
			state.status.updateUser = SUCCESS;
			const user = state.users.find((u) => u.id === action.payload.id);

			if (user) {
				const { firstName, lastName, roles } = action.payload;

				user.firstName = firstName;
				user.lastName = lastName;

				user.roles = roles;
			}
		},
		updateUserFailure: (state: AdminUsersState, action: PayloadAction<string[]>) => {
			state.status.updateUser = FAILED;
			state.error = action.payload;
		},

		cleanUpUserStatus: (state: AdminUsersState) => {
			state.status = adminUsersInitialState.status;
			state.error = adminUsersInitialState.error;
		},

		cleanUpSelectedUser: (state: AdminUsersState) => {
			state.selectedUser = adminUsersInitialState.selectedUser;
		}
	}
});

export default adminUsersSlice;

export const {
	getUsersStart,
	getUsersSuccess,
	getUsersFailure,
	getUserStart,
	getUserSuccess,
	getUserFailure,
	deleteUserStart,
	deleteUserSuccess,
	deleteUserFailure,
	createUserStart,
	createUserSuccess,
	createUserFailure,
	updateUserStart,
	updateUserSuccess,
	updateUserFailure,
	cleanUpUserStatus,
	cleanUpSelectedUser
} = adminUsersSlice.actions;
