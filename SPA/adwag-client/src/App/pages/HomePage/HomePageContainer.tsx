import React from 'react';
import { useSelector } from 'react-redux';

import { Button } from 'antd';
import { RootState } from 'App/state/root.reducer';

const HomePageContainer: React.FC<{}> = () => {
	type MouseClickEvent = (event: React.MouseEvent<HTMLElement, MouseEvent>) => void;

	const state = useSelector((state: RootState) => state);

	const logState: MouseClickEvent = () => {
		console.log(state);
	};

	const getAllCokies: MouseClickEvent = () => {
		console.log(document.cookie.split(';'));
	};

	return (
		<div>
			<h1>Strona główna</h1>
			<Button onClick={logState}>Log Redux State</Button>
			<Button onClick={getAllCokies}>Log Document Cookies</Button>
		</div>
	);
};

export default HomePageContainer;
