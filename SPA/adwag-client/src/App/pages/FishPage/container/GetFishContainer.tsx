import React, { useEffect } from 'react';
import { RouteComponentProps, useHistory } from 'react-router';
import { useSelector, useDispatch } from 'react-redux';

import { Avatar, Badge, Button, Col, Result, Row, Typography } from 'antd';
import LoadingScreen from 'App/common/components/LoadingScreen';
import { RootState } from 'App/state/root.reducer';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';
import { getFish } from 'App/state/fish/fish.thunk';
import { cleanUpFishStatus, cleanUpSelectedFish } from 'App/state/fish/fish.slice';
import { GetFishTabs } from '../components/GetFishTab';
import { ArrowLeftOutlined } from '@ant-design/icons';

interface RouteParams {
	fishId: string;
}

interface GetFishContainerProps extends RouteComponentProps<RouteParams> {}

const { LOADING } = StatusType;

const GetFishContainer: React.FC<GetFishContainerProps> = ({ match }: GetFishContainerProps) => {
	const fishId = match.params.fishId;
	const { t } = useTranslation(['page', 'common']);

	const history = useHistory();
	const dispatch = useDispatch();

	const fish = useSelector((state: RootState) => state.fish.selectedFish);
	const fishStatus = useSelector((state: RootState) => state.fish.status);

	useEffect(() => {
		if (!fish) {
			dispatch(getFish(fishId));
		}
	}, [dispatch, fish, fishId]);

	useEffect(() => {
		return () => {
			dispatch(cleanUpFishStatus());
			dispatch(cleanUpSelectedFish());
		};
	}, [dispatch]);

	return fishStatus.getFish === LOADING ? (
		<LoadingScreen container='screen' />
	) : fish ? (
		<>
			<Button style={{ marginLeft: 16 }} onClick={() => history.push(`/aquariums`)} icon={<ArrowLeftOutlined />}>
				{t('common:Actions.GoBack')}
			</Button>
			<GetFishTabs fish={fish} />
		</>
	) : (
		<Result
			status='404'
			title={t('common:Errors.AnErrorOccured')}
			subTitle={t('common:Errors.UserNotFound')}
			extra={
				<Button type='primary' onClick={() => history.push('/')}>
					{t('common:Actions.BackToHome')}
				</Button>
			}
		/>
	);
};

export default GetFishContainer;
