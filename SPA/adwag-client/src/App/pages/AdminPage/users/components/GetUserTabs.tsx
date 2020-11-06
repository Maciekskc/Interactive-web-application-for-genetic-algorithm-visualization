import React, { ReactNode, useState } from 'react';
import { Card, Col, Row } from 'antd';
import { GetUserResponse } from 'App/api/endpoints/admin/responses';
import { useTranslation } from 'react-i18next';
import UserGeneralTab from './UserGeneralTab';
import UserSecurityTab from './UserSecurityTab';

interface GetUserTabsProps {
	user: GetUserResponse;
}

export const GetUserTabs: React.FC<GetUserTabsProps> = ({ user }) => {
	interface AvailableTab {
		key: string;
		content: ReactNode;
		tab: string;
	}

	const { t } = useTranslation('page');

	const availableTabs = [
		{
			key: 'k1',
			tab: t('AdminPage.GetUserTabs.TabGeneral'),
			content: <UserGeneralTab user={user} />
		},
		{
			key: 'k2',
			tab: t('AdminPage.GetUserTabs.TabSecurity'),
			content: <UserSecurityTab user={user} />
		}
	] as AvailableTab[];

	const [state, setState] = useState<AvailableTab>(availableTabs.find((a) => a.key === 'k1'));

	const onTabChange = (key: string) => {
		setState(availableTabs.find((a) => a.key === key));
	};

	return (
		<Row
			style={{
				marginTop: 16
			}}
			justify='center'
		>
			<Col span={18}>
				<Card
					tabList={availableTabs}
					activeTabKey={state.key}
					onTabChange={(key) => {
						onTabChange(key);
					}}
				>
					{state.content}
				</Card>
			</Col>
		</Row>
	);
};
