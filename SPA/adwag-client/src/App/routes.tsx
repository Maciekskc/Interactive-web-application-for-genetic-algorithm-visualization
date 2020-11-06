import React from 'react';
import { Switch, Route, Redirect } from 'react-router';

import ProtectedRoute from './common/components/ProtectedRoute';
import { default as NotFoundPage } from './pages/NotFoundPage/NotFoundPageContainer';
import { default as HomePage } from './pages/HomePage/HomePageContainer';
import { default as LoginPage } from './pages/LoginPage/LoginPageContainer';
import { default as AuthPage } from './pages/AuthPage/AuthPageContainer';
import { default as AdminPage } from './pages/AdminPage/AdminPageContainer';
import { default as ResetPasswordPage } from './pages/ResetPasswordPage/ResetPasswordPageContainer';
import Role from './types/role';

const Routes: React.FC = () => {
	return (
		<Switch>
			<Route exact path='/' component={HomePage} />
			<Route exact path='/sign-in' component={LoginPage} />

			// todo rejestracja 
			{/* <Route exact path='/sign-up' component={RegisterPage} /> */}
			
			<Route exact path='/reset-password' component={ResetPasswordPage} />
			<ProtectedRoute
				path='/auth'
				exact
				component={AuthPage}
				acceptedRoles={[Role.ADMIN]}
				others={{ text: 'This is visible only for admins' }}
			/>
			<ProtectedRoute
				path='/user'
				component={AuthPage}
				acceptedRoles={[Role.ADMIN, Role.USER]}
				others={{ text: 'This is visible for all logged in users' }}
			/>
			<ProtectedRoute acceptedRoles={[Role.ADMIN]} path='/admin' component={AdminPage} />
			<Route path='/404' component={NotFoundPage} />
			<Redirect to='/404' />
		</Switch>
	);
};

export default Routes;