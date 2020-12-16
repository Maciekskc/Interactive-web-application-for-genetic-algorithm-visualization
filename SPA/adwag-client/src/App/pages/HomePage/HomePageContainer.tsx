import React from 'react';
import { useSelector } from 'react-redux';

import { Button } from 'antd';
import { RootState } from 'App/state/root.reducer';
import Title from 'antd/lib/typography/Title';
import './HomePageContainer.less';

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
		<div className='landing-page-style'>
			<Title level={1}>Witamy w ADWAG!</Title>
			<h2>Adwag, to interaktywna aplikacja webowa do wizualizacji algorytmu genetycznego,</h2>
			<Title level={4} style={{ paddingBottom: '3rem' }}>
				Aby przyjrzeć się bliżej jak działa algorytm populacyjny, przejdź do zakładki akwarium i wybierz
				populacje, którą chciałbyś obserwować.
			</Title>
		</div>
	);
};

export default HomePageContainer;
