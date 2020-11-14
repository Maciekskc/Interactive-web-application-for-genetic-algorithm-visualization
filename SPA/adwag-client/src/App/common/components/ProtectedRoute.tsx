// Thrid party imports
import React from 'react';
import { connect, DispatchProp } from 'react-redux';
import { Route, Redirect, RouteChildrenProps } from 'react-router-dom';

// Routes

// Local imports
import LoadingScreen from './LoadingScreen';
import agent from 'App/api/agent/agent';
import { RootState } from 'App/state/root.reducer';
import { authenticationSuccess, getUserDetailsSuccess } from 'App/state/session/session.slice';
import { mapStateToProps } from 'App/state/utils/connect';
import Role from 'App/types/role';

// Additional styling

// Component
interface ProtectedRouteState {
	isLoading: boolean;
	isUserAuthorized: boolean;
	isUserAuthenticated: boolean;
}

interface OwnProps {
	component: React.FC<any> | React.ComponentType<any>;
	path: string;
	exact?: boolean;
	acceptedRoles?: Role[];
	others?: any;
}

interface ProtectedRouteProps extends DispatchProp, RootState, OwnProps {}

class ProtectedRoute extends React.Component<ProtectedRouteProps, ProtectedRouteState> {
	constructor(props: ProtectedRouteProps) {
		super(props);

		let isUserAuthorized = this.checkIfUserIsAuthorized();
		let isUserAuthenticated = this.checkIfUserIsAuthenticated();
		this.state = {
			isLoading: !isUserAuthorized || !isUserAuthenticated,
			isUserAuthorized,
			isUserAuthenticated
		};
	}

	componentDidMount() {
		if (!this.state.isUserAuthorized || !this.state.isUserAuthenticated) {
			// [DEV]: usunac token z local Storage | przed wypusczeniem do produkcji
			const token = localStorage.getItem('token');

			if (token) {
				this.props.dispatch(authenticationSuccess({ token, refreshToken: '' }));

				agent.Account.getAccountDetails().then((res) => {
					this.props.dispatch(getUserDetailsSuccess(res));
					this.setState({
						isLoading: false,
						isUserAuthenticated: this.checkIfUserIsAuthenticated(),
						isUserAuthorized: this.checkIfUserIsAuthorized()
					});
				});
			} else {
				this.setState({
					isLoading: false
				});
			}
		}
	}

	checkIfUserIsAuthorized(): boolean {
		if (this.props.session.user && this.props.session.user.roles) {
			let allowed: boolean = false;
			this.props.acceptedRoles.forEach((acceptedRole) => {
				allowed = this.props.session.user.roles.some((userRole) => acceptedRole === userRole);
				if (allowed) {
					return allowed;
				}
			});
			return allowed;
		}
	}

	checkIfUserIsAuthenticated(): boolean {
		return !!(this.props.session.info && this.props.session.info.token);
	}

	render() {
		const { component: Component, others, ...rest } = this.props;
		return (
			<Route
				{...rest}
				render={(props: RouteChildrenProps) => {
					if (this.state.isLoading) {
						return <LoadingScreen container='screen' />;
					} else if (this.state.isUserAuthorized && this.state.isUserAuthenticated) {
						return <Component {...others} {...props} />;
					} else {
						return (
							<Redirect
								to={{
									pathname: '/signin',
									state: props.location
								}}
							/>
						);
					}
				}}
			/>
		);
	}
}

export default connect<RootState, DispatchProp, OwnProps>(mapStateToProps)(ProtectedRoute);
