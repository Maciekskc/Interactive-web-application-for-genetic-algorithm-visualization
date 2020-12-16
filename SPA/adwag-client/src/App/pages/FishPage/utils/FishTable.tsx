import React, { Dispatch } from 'react';

import { Link } from 'react-router-dom';
import { TFunction } from 'i18next';
import { FishForGetFishesFromAquariumResponse } from 'App/api/endpoints/fish/responses/getFishesFromAquariumResponse';
import { FishForGetUserFishesResponse } from 'App/api/endpoints/fish/responses/getUserFishesResponse';
import { default as NumberFormat } from 'react-number-format';
import { killFish } from 'App/state/fish/fish.thunk';
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { Button, Modal, Tag } from 'antd';

const hungaryTagColor = (hungaryLevel: number) => {
	if (hungaryLevel === 4) {
		return 'lime';
	}
	if (hungaryLevel > 3) {
		return 'green';
	}
	if (hungaryLevel > 2) {
		return 'yellow';
	}
	if (hungaryLevel > 1) {
		return 'orange';
	}
	return 'red';
};

const formatBirthDate = (birthDate: Date) => {
	return new Date(birthDate).toLocaleString();
};

export const renderFishesFromAquariumTableColumns = (
	fishes: FishForGetFishesFromAquariumResponse[],
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
		render: (record, object) => <>{formatBirthDate(object.lifeTimeStatistic.birthDate)}</>
	},
	{
		title: 'Najedzenie',
		dataIndex: 'hunger',
		render: (record, object) => (
			<>
				<Tag
					style={{ width: '4rem', textAlign: 'center' }}
					color={hungaryTagColor(object.lifeParameters.hunger)}
				>
					{object.lifeParameters.hunger} / 5.0
				</Tag>
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
	fishes: FishForGetUserFishesResponse[],
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
	{ title: 'Akwarium', dataIndex: 'aquariumId' },
	{
		title: 'Żyje',
		dataIndex: 'isAlive',
		render: (record, object) => (
			<Tag style={{ width: '4rem', textAlign: 'center' }} color={record === true ? 'green' : 'red'}>
				{record === true ? 'TAK' : 'NIE'}
			</Tag>
		)
	},
	{
		title: t('AdminPage.UsersTable.Actions'),
		render: (record: FishForGetUserFishesResponse) =>
			record.isAlive === true ? (
				<Button type='link' onClick={confirmKillFish(record.id, fishes, dispatch, t)}>
					Zabij rybkę
				</Button>
			) : (
				<h4>Brak dostępnych akcji</h4>
			)
	}
];

export function confirmKillFish(
	fishId: string,
	fishes: FishForGetUserFishesResponse[],
	dispatch: Dispatch<any>,
	t: TFunction
) {
	const { confirm } = Modal;

	return () => {
		const fishToKill = fishes.find((f) => f.id === fishId);
		confirm({
			title: `Czy na pewno chcesz zabić obiekt? ${fishToKill?.name}}?`,
			icon: <ExclamationCircleOutlined />,
			content: "t('common:Warnings.ActionWillBeIrreversible')",
			okText: t('common:Actions.Yes'),
			okType: 'primary',
			cancelText: t('common:Actions.No'),
			onOk() {
				dispatch(killFish(fishId));
			}
		});
	};
}
