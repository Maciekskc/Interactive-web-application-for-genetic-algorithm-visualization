import { combineReducers } from '@reduxjs/toolkit';

import adminLogsSlice from './admin/logs/logs.slice';
import adminUsersSlice from './admin/users/users.slice';

import sessionSlice from './session/session.slice';

const rootReducer = combineReducers({
	admin: combineReducers({
		users: adminUsersSlice.reducer,
		logs: adminLogsSlice.reducer
	}),
	session: sessionSlice.reducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
