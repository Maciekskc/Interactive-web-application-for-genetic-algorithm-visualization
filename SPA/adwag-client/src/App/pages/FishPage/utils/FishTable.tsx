import React, { Dispatch } from 'react';

import { Link } from 'react-router-dom';
import { TFunction } from 'i18next';
import { FishForGetFishesFromAquariumResponse } from 'App/api/endpoints/fish/responses/getFishesFromAquariumResponse';
import { FishForGetUserFishesResponse } from 'App/api/endpoints/fish/responses/getUserFishesResponse';
import { default as NumberFormat } from 'react-number-format';

export const renderFishesFromAquariumTableColumns = (
	aquariums: FishForGetFishesFromAquariumResponse[],
	dispatch: Dispatch<any>,
	t: TFunction
) => [
	{
		title: 'Id',
		dataIndex: 'id',
		key: 'id',
		render: (id, record) => <Link to={`/fishes/${record.id}`}>{id}</Link>
	},
	{ title: 'Nazwa', dataIndex: 'name' },
	{
		title: 'Urodzony',
		dataIndex: 'birthDate',
		render: (record, object) => <>{object.lifeTimeStatistic.birthDate}</>
	},
	{
		title: 'Najedzenie',
		dataIndex: 'hunger',
		render: (record, object) => (
			<>
				<NumberFormat value={object.lifeParameters.hunger} displayType={'text'} format='####' /> / 5.0
			</>
		)
	},
	{
		title: 'Prędkość',
		dataIndex: 'v',
		render: (record, object) => (
			<>
				<NumberFormat value={object.physicalStatistic.v} displayType={'text'} format='#####' />
			</>
		)
	}
];

export const renderUserFishesTableColumns = (
	aquariums: FishForGetUserFishesResponse[],
	dispatch: Dispatch<any>,
	t: TFunction
) => [
	{
		title: 'Id',
		dataIndex: 'id',
		key: 'id',
		render: (id, record) => <Link to={`/aquariums/${record.id}`}>{id}</Link>
	},
	{ title: 'Szerokość', dataIndex: 'width' },
	{ title: 'Wysokość', dataIndex: 'height' },
	{ title: 'Ilość Jedzenia', dataIndex: 'foodMaximalAmount' },
	{
		title: 'Populacja',
		dataIndex: 'currentPopulationCount',
		key: 'currentPopulationCount',
		render: (currentPopulationCount, record) => (
			<Link to={`/fishes/aquarium/${record.id}`}>{currentPopulationCount}</Link>
		)
	},
	{
		title: 'Animacja',
		render: (id, record) => <Link to={`/aquariums/${record.id}/animation`}>Animacja</Link>
	}
];
