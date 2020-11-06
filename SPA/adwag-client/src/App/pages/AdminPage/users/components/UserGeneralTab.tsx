import { Badge, Col, Row, Tag, Typography } from 'antd';
import { GetUserResponse } from 'App/api/endpoints/admin/responses';
import React from 'react';
import { useTranslation } from 'react-i18next';

interface UserGeneralTabProps {
	user: GetUserResponse;
}

const UserGeneralTab: React.FC<UserGeneralTabProps> = ({ user }) => {
	const { t } = useTranslation(['page', 'form', 'common']);

	if (user) {
		return (
			<>
				<Row gutter={[16, 16]}>
					<Col span={24}>
						<Typography>
							<Typography.Text type='secondary'>{t('form:User.Labels.FirstName')}: </Typography.Text>
							<Typography.Text>{user.firstName}</Typography.Text>
						</Typography>
					</Col>
					<Col span={24}>
						<Typography>
							<Typography.Text type='secondary'>{t('form:User.Labels.LastName')}: </Typography.Text>
							<Typography.Text>{user.lastName}</Typography.Text>
						</Typography>
					</Col>
					<Col span={24}>
						<Badge
							status={user.emailConfirmed ? 'success' : 'default'}
							title={
								user.emailConfirmed
									? t('AdminPage.UserGeneralTab.StatusConfirmed')
									: t('AdminPage.UserGeneralTab.StatusUnConfirmed')
							}
						>
							<Typography>
								<Typography.Text type='secondary'>{t('form:User.Labels.Email')}: </Typography.Text>
								<Typography.Text>{user.email}</Typography.Text>
							</Typography>
						</Badge>
					</Col>
					<Col span={24}>
						<Badge
							status={user.phoneNumberConfirmed ? 'success' : 'default'}
							title={
								user.phoneNumberConfirmed
									? t('AdminPage.UserGeneralTab.StatusConfirmed')
									: t('AdminPage.UserGeneralTab.StatusUnConfirmed')
							}
						>
							<Typography>
								<Typography.Text type='secondary'>{t('form:User.Labels.PhoneNumber')}: </Typography.Text>
								<Typography.Text>
									{user.phoneNumber || t('AdminPage.UserGeneralTab.BlankPhoneNumber')}
								</Typography.Text>
							</Typography>
						</Badge>
					</Col>
					<Col span={24}>
						<Typography>
							<Typography.Text type='secondary'>{t('form:User.Labels.UserName')}: </Typography.Text>
							<Typography.Text>{user.userName}</Typography.Text>
						</Typography>
					</Col>
					<Col span={24}>
						<Typography>
							<Typography.Text type='secondary'>{t('form:User.Labels.Roles')}: </Typography.Text>
							{user.roles.map((role, index) => {
								const generatedRoleKey = `common:Roles.${role}`;
								return <Tag key={index}>{t(generatedRoleKey)}</Tag>;
							})}
						</Typography>
					</Col>
				</Row>
			</>
		);
	} else {
		return <></>;
	}
};

export default UserGeneralTab;
