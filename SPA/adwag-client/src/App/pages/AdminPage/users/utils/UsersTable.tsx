import React, { Dispatch } from 'react';

import { Link } from 'react-router-dom';
import { Tag, Button, Modal, Dropdown, Menu } from 'antd';
import { ExclamationCircleOutlined, SettingFilled } from '@ant-design/icons';

import { deleteUser } from 'App/state/admin/users/users.thunk';
import { UserForGetUsersResponse } from 'App/api/endpoints/admin/responses/getUsersResponse';
import { TFunction } from 'i18next';
import Role from 'App/types/role';

export const renderTableColumns = (users: UserForGetUsersResponse[], dispatch: Dispatch<any>, t: TFunction) => [
	{
		title: t('form:User.Labels.FirstName'),
		dataIndex: 'firstName',
		render: (firstName, record) => <Link to={`/admin/users/${record.id}`}>{firstName}</Link>
	},
	{ title: t('form:User.Labels.LastName'), dataIndex: 'lastName' },
	{ title: t('form:User.Labels.Email'), dataIndex: 'email' },
	{ title: t('form:User.Labels.EmailConfirmed'), dataIndex: 'emailConfirmed', render: (emailConfirmed: boolean) => (
		<>{emailConfirmed ? t('common:Actions.Yes') : t('common:Actions.No')}</>
	) },
	{
		title: t('form:User.Labels.Roles'),
		dataIndex: 'roles',
		render: (roles: string[]) => (
			<>
				{roles.map((role: string) => {
					const roleTranslationKey = `common:Roles.${role}`;
					const color = role === Role.ADMIN ? 'blue' : 'volcano';
					return (
						<Tag key={role} color={color}>
							{t(roleTranslationKey)}
						</Tag>
					);
				})}
			</>
		)
	},
	{
		title: t('AdminPage.UsersTable.Actions'),
		render: (record: UserForGetUsersResponse) => (
			<h1>
				<Dropdown
					overlay={menuForActionDropdown(record, users, dispatch, t)}
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
	record: UserForGetUsersResponse,
	users: UserForGetUsersResponse[],
	dispatch: Dispatch<any>,
	t: TFunction) => (
	<Menu>
		<Menu.Item>
			<Button type='link'>
				<Link to={`/admin/users/${record.id}/update`}>{t('common:Actions.Edit')}</Link>
			</Button>
		</Menu.Item>
		<Menu.Item>
			<Button type='link' onClick={confirmUserDelete(record.id, users, dispatch, t)}>
				{t('common:Actions.Remove')}
			</Button>
		</Menu.Item>
	</Menu>
);

export function confirmUserDelete(userId: string, users: UserForGetUsersResponse[], dispatch: Dispatch<any>, t: TFunction) {
	const { confirm } = Modal;

	return () => {
		const userToDelete = users.find((u) => u.id === userId);
		confirm({
			title: `${t('AdminPage.UsersTable.ConfirmUserDeletionTitle')} ${userToDelete?.firstName} ${userToDelete?.lastName}?`,
			icon: <ExclamationCircleOutlined />,
			content: t('common:Warnings.ActionWillBeIrreversible'),
			okText: t('common:Actions.Yes'),
			okType: 'primary',
			cancelText: t('common:Actions.No'),
			onOk() {
				dispatch(deleteUser(userId));
			}
		});
	};
}
