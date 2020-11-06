import React from 'react';
import { Result, Button } from 'antd';
import { RouteComponentProps } from 'react-router';
import { useTranslation } from 'react-i18next';

interface NotFoundPageContainerProps extends RouteComponentProps {}

const NotFoundPageContainer: React.FC<NotFoundPageContainerProps> = ({ history }: NotFoundPageContainerProps) => {
	const buttonGoBackHomeOnClick = () => history.push('/');
	const {t} = useTranslation(['page', 'common']);

	return (
		<Result
			status='404'
			title='404'
			subTitle={t('NotFoundPage.NotFoundPageContainer.WeAreSorry')}
			extra={
				<Button type='primary' onClick={buttonGoBackHomeOnClick}>
					{t('common:Actions.BackToHome')}
				</Button>
			}
		></Result>
	);
};

export default NotFoundPageContainer;
