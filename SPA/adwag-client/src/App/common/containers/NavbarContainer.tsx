import React from 'react';
import { Menu } from 'antd';
import { Link, useHistory, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'App/state/root.reducer';
import { useTranslation } from 'react-i18next';
import Role from 'App/types/role';
import { CheckOutlined, LogoutOutlined, SettingOutlined, TranslationOutlined } from '@ant-design/icons';
import i18n, { fullLanguages, languages } from 'i18n';
import './NavbarContainer.less';
import { devalidateSession } from 'App/state/session/session.thunk';

const NavbarContainer: React.FC<{}> = () => {
	// const userIsLoggedIn = useSelector<RootState>(
	// 	(state: RootState) => !!(state.session.info && state.session.info.token)
	// );
	const dispatch = useDispatch();
	const history = useHistory();
	const user = useSelector((state: RootState) => state.session.user);
	const { t } = useTranslation('page');

	const location = useLocation();

	const logout = () => {
		dispatch(
			devalidateSession(() => {
				history.push('/');
			})
		);
	};

	const changeLanguage = (values: any) => {
		i18n.changeLanguage(values.key);
	};

	const home = (
		<>
			{/* <Menu.Item key='/'>
				<Link to='/'>{t('Common.NavbarContainer.Home')}</Link>
			</Menu.Item>
			<Menu.Item key='/fishes'>
				<Link to='/fishes'>Rybka</Link>
			</Menu.Item> */}
			<Menu.Item key='/aquariums'>
				<Link to='/aquariums'>Akwaria</Link>
			</Menu.Item>
		</>
	);

	const accountMenu = (
		<Menu.SubMenu key='/account' style={{ float: 'right' }} title={`${user?.firstName} ${user?.lastName}`}>
			<Menu.Item key='/account/edit-profile' icon={<SettingOutlined />}>
				{t('Common.NavbarContainer.Settings')}
			</Menu.Item>
			<Menu.SubMenu key='languages' title={t('Common.NavbarContainer.Language')} icon={<TranslationOutlined />}>
				{languages.map((language) => (
					<Menu.Item
						key={language}
						onClick={changeLanguage}
						icon={i18n.language === language && <CheckOutlined />}
					>
						{fullLanguages.find((lang) => lang.key === language).value}
					</Menu.Item>
				))}
			</Menu.SubMenu>
			<Menu.Divider />

			<Menu.Item key='logout' onClick={logout} icon={<LogoutOutlined />}>
				{t('Common.NavbarContainer.Logout')}
			</Menu.Item>
		</Menu.SubMenu>
	);

	if (user) {
		if (user.roles.includes(Role.ADMIN)) {
			// admin
			return (
				<Menu mode='horizontal' selectedKeys={[location.pathname]} className='menu-padding'>
					{home}
					<Menu.Item key='/admin/users'>
						<Link to='/admin/users'>{t('Common.NavbarContainer.Admin')}</Link>
					</Menu.Item>
					{accountMenu}
				</Menu>
			);
		} else if (user.roles.includes(Role.USER)) {
			// user
			return (
				<Menu mode='horizontal' selectedKeys={[location.pathname]} className='menu-padding'>
					{home}
					<Menu.Item key='/fishes/user-fishes/all'>
						<Link to='/fishes/user-fishes/all'>Rybki</Link>
					</Menu.Item>
					{accountMenu}
				</Menu>
			);
		}
	} else {
		// niezalogowany
		return (
			<Menu mode='horizontal' selectedKeys={[location.pathname]} className='menu-padding'>
				{home}
				<Menu.Item key='/sign-up' style={{ float: 'right' }}>
					<Link to='/sign-up'>{t('Common.NavbarContainer.SignUp')}</Link>
				</Menu.Item>
				<Menu.Item key='/sign-in' style={{ float: 'right' }}>
					<Link to='/sign-in'>{t('Common.NavbarContainer.SignIn')}</Link>
				</Menu.Item>
			</Menu>
		);
	}
};

export default NavbarContainer;
