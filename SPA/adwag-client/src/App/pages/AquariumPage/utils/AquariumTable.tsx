import React, { Dispatch } from 'react';

import { Link } from 'react-router-dom';
import { TFunction } from 'i18next';
import { AquariumForGetAquariumsResponse } from 'App/api/endpoints/aquarium/responses/getAquariumsResponse';

export const renderTableColumns = (
	aquariums: AquariumForGetAquariumsResponse[],
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
		render: (id, record) => <Link to={`/aquariums/${record.id}`}>Animacja</Link>
	}
];
