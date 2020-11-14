import React, { Dispatch } from 'react';

import { Link } from 'react-router-dom';
import { TFunction } from 'i18next';
import { AquariumForGetAquariumsResponse } from 'App/api/endpoints/aquarium/responses/getAquariumsResponse';
import { deleteAquarium } from 'App/state/aquarium/aquarium.thunk';
import { SettingFilled, ExclamationCircleOutlined } from '@ant-design/icons';
import { Dropdown, Button, Menu, Modal } from 'antd';

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
		render: (id, record) => <Link to={`/aquariums/${record.id}/animation`}>Animacja</Link>
	},
	{
		title: t('AdminPage.UsersTable.Actions'),
		render: (record: AquariumForGetAquariumsResponse) => (
			<h1>
				<Dropdown
					overlay={menuForActionDropdown(record, aquariums, dispatch, t)}
					trigger={['click']}
					placement='bottomCenter'
				>
					<Button type='link'>
						<SettingFilled />
					</Button>
				</Dropdown>
			</h1>
		)
	}
];

const menuForActionDropdown = (
	record: AquariumForGetAquariumsResponse,
	aquariums: AquariumForGetAquariumsResponse[],
	dispatch: Dispatch<any>,
	t: TFunction
) => (
	<Menu>
		<Menu.Item>
			<Button type='link'>
				<Link to={`/admin/aquariums/${record.id}/update`}>{t('common:Actions.Edit')}</Link>
			</Button>
		</Menu.Item>
		<Menu.Item>
			<Button type='link' onClick={confirmUserDelete(record.id, aquariums, dispatch, t)}>
				{t('common:Actions.Remove')}
			</Button>
		</Menu.Item>
	</Menu>
);

export function confirmUserDelete(
	aquariumId: string,
	aquariums: AquariumForGetAquariumsResponse[],
	dispatch: Dispatch<any>,
	t: TFunction
) {
	const { confirm } = Modal;

	return () => {
		confirm({
			title: `Czy na pewno chcesz usunąć akwarium #${aquariumId}?`,
			icon: <ExclamationCircleOutlined />,
			content: t('common:Warnings.ActionWillBeIrreversible'),
			okText: t('common:Actions.Yes'),
			okType: 'primary',
			cancelText: t('common:Actions.No'),
			onOk() {
				dispatch(deleteAquarium(aquariumId));
			}
		});
	};
}
