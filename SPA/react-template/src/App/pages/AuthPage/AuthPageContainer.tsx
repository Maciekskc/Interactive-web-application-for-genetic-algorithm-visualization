import React from 'react';
import { RouteChildrenProps } from 'react-router';
import { useDispatch, useSelector } from 'react-redux';

import { Button } from 'antd';

import agent from 'App/api/agent/agent';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { devalidateSession } from 'App/state/session/session.thunk';
import { StatusType } from 'App/types/requestStatus';

type MouseClickEvent = (event: React.MouseEvent<HTMLElement, MouseEvent>) => void;

interface AuthPageContainerProps extends RouteChildrenProps {
	text?: string;
}

const { LOADING } = StatusType;

const AuthPageContainer: React.FC<AuthPageContainerProps> = ({ history, text }: AuthPageContainerProps) => {
	const dispatch = useDispatch();

	const sessionStatus = useSelector((state: RootState) => state.session.status);

	const handleLogOutButtonClick: MouseClickEvent = () => {
		dispatch(
			devalidateSession(() => {
				history.push('/');
			})
		);
	};

	const handleGetAccountDetailsButtonClick = (e: React.MouseEvent<HTMLElement, MouseEvent>) => {
		e.preventDefault();
		agent.Account.getAccountDetails().then((res) => console.log(res));
	};

	return (
		<div>
			{sessionStatus.devalidateSession === LOADING && <LoadingScreen container='screen' />}
			<h1>{text}</h1>
			<Button onClick={handleLogOutButtonClick}>Wyloguj</Button>
			<Button onClick={handleGetAccountDetailsButtonClick}>Dane konta</Button>
		</div>
	);
};

export default AuthPageContainer;
