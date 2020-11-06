import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { FileSearchOutlined } from '@ant-design/icons';
import { Row, Col } from 'antd';
import { Input } from 'antd';

import LoadingScreen from 'App/common/components/LoadingScreen';
import PageTitle from 'App/common/components/PageTitle';
import { LogForGetLogsFilesResponse } from 'App/api/endpoints/logs/responses';
import LogCard from '../components/LogCard';
import AllLogsCard from '../components/AllLogsCard';
import {
	getLogsFiles,
	getLogsFileContent,
	downloadLogsFile,
	downloadAllLogsFiles
} from 'App/state/admin/logs/logs.thunk';
import { RootState } from 'App/state/root.reducer';

import './GetLogs.less';
import { StatusType } from 'App/types/requestStatus';
import { useTranslation } from 'react-i18next';

const { TextArea } = Input;
const { LOADING } = StatusType;

const GetLogsContainer = () => {
	const dispatch = useDispatch();
	const { t } = useTranslation('page');

	const { logs, logContent, status } = useSelector((state: RootState) => state.admin.logs);

	const [fileName, setFileName] = useState('');
	const [activeCardIndex, setActiveCardIndex] = useState(-1);

	useEffect(() => {
		dispatch(getLogsFiles());
	}, [dispatch]);

	useEffect(() => {
		dispatch(getLogsFileContent(fileName));
	}, [dispatch, fileName]);

	const showLogsFileContent = (fileName: string) => {
		setFileName(fileName);
	};

	const handleLogCardClick = (name: string, index: number) => {
		return () => {
			showLogsFileContent(name);
			setActiveCardIndex(index);
		};
	};

	const handleDownloadClick = (name: string) => {
		return () => {
			dispatch(downloadLogsFile(name));
		};
	};

	return (
		<>
			<PageTitle title={t('AdminPage.GetLogsContainer.PageTitle')} icon={<FileSearchOutlined />} />
			{status.getLogsFiles === LOADING ? (
				<LoadingScreen container={'screen'} />
			) : (
				<Row>
					<Col xl={20} xs={24}>
						{status.getLogsFileContent === LOADING ? (
							<LoadingScreen container='fill' />
						) : (
							<TextArea
								value={Array.prototype.join.call(logContent, '\n')}
								placeholder={t('AdminPage.GetLogsContainer.TextAreaPlaceholder')}
								className='logs-content'
							/>
						)}
					</Col>
					<Col xl={4} xs={24} className='col-cards'>
						<AllLogsCard onDownloadClick={() => dispatch(downloadAllLogsFiles('allLogs'))} />
						{logs.map((log: LogForGetLogsFilesResponse, index: number) => {
							return (
								<LogCard
									key={index}
									activeCardIndex={activeCardIndex}
									index={index}
									log={log}
									onCardClick={handleLogCardClick}
									onDownloadClick={handleDownloadClick}
								/>
							);
						})}
					</Col>
				</Row>
			)}
		</>
	);
};

export default GetLogsContainer;
