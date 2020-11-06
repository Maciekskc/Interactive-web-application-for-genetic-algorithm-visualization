import React, { useEffect, useState } from 'react';

import { Link, useLocation } from 'react-router-dom';
import { Menu } from 'antd';
import { UserOutlined, FileTextOutlined } from '@ant-design/icons';
import { useTranslation } from 'react-i18next';

import './AdminNavbarContainer.less';

const AdminNavbarContainer: React.FC<{}> = () => {
	const { t } = useTranslation('page');

	const location = useLocation();

	interface NavbarItems {
		key: string;
		subkeys: string[];
	}

	const navbarItems = [
		{
			key: '/admin/users',
			subkeys: ['admin-users-fallback', '/admin/users', '/admin/users/create']
		},
		{
			key: '/admin/logs',
			subkeys: ['/admin/logs']
		}
	] as NavbarItems[];

	// ustawienie automatycznego mapowania ścieżki URL na rozwinięcie SubMenu oraz zaznaczenie Menu.Itema
	useEffect(() => {
		setLoading(true);
		const navbarItem = navbarItems.find((navbarItem) => location.pathname.includes(navbarItem.key));
		setactiveNavbarItemKey(navbarItem.key);

		if (navbarItem) {
			const subKey = navbarItem.subkeys.find((subkey) => subkey.includes(location.pathname));
			if (subKey === undefined) {
				setActiveSubKey(navbarItem.subkeys[0]);
			} else {
				setActiveSubKey(subKey);
			}
		}
		setLoading(false);
	}, [navbarItems, location.pathname]);

	const [activeNavbarItemKey, setactiveNavbarItemKey] = useState('');
	const [activeSubKey, setActiveSubKey] = useState('');
	const [loading, setLoading] = useState(true);

	const usersSubMenu = (
		<Menu.SubMenu
			key={navbarItems[0].key}
			icon={<UserOutlined />}
			title={t('AdminPage.AdminNavbarContainer.Users')}
		>
			<Menu.Item key={navbarItems[0].subkeys[1]}>
				<Link to={navbarItems[0].subkeys[1]}>{t('AdminPage.AdminNavbarContainer.UserList')}</Link>
			</Menu.Item>
			<Menu.Item key={navbarItems[0].subkeys[2]}>
				<Link to={navbarItems[0].subkeys[2]}>{t('AdminPage.AdminNavbarContainer.AddUser')}</Link>
			</Menu.Item>
			<Menu.Item key={navbarItems[0].subkeys[0]} hidden />
		</Menu.SubMenu>
	);

	const logsSubMenu = (
		<Menu.SubMenu
			key={navbarItems[1].key}
			icon={<FileTextOutlined />}
			title={t('AdminPage.AdminNavbarContainer.Logs')}
		>
			<Menu.Item key={navbarItems[1].subkeys[0]}>
				<Link to={navbarItems[1].subkeys[0]}>{t('AdminPage.AdminNavbarContainer.LogList')}</Link>
			</Menu.Item>
		</Menu.SubMenu>
	);

	return (
		!loading && (
			<Menu
				className='sidebar'
				defaultOpenKeys={[activeNavbarItemKey]}
				selectedKeys={[activeSubKey]}
				mode='inline'
			>
				{usersSubMenu}
				{logsSubMenu}
			</Menu>
		)
	);
};

export default AdminNavbarContainer;
