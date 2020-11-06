import React from 'react';

import { Card, Col, Row } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';

import { LogForGetLogsFilesResponse } from 'App/api/endpoints/logs/responses';
import { useTranslation } from 'react-i18next';

interface LogCardProps {
	log: LogForGetLogsFilesResponse;
	activeCardIndex: number;
	index: number;
	onCardClick: (name: string, index: number) => () => void;
	onDownloadClick: (name: string) => () => void;
}

const LogCard: React.FC<LogCardProps> = ({
	log,
	activeCardIndex,
	index,
	onCardClick,
	onDownloadClick
}: LogCardProps) => {
	let cardClasses = activeCardIndex === index ? 'active-card card' : 'card';

	const { i18n } = useTranslation();
	return (
		<Card
			bordered={true}
			title={
				<Row justify='center'>
					<Col>
						{new Date(log.date).toLocaleString(i18n.language, {
							month: 'short',
							day: '2-digit',
							year: 'numeric'
						})}
					</Col>
				</Row>
			}
			className={cardClasses}
			onClick={onCardClick(log.name, index)}
			key={index}
		>
			<Row justify='center' align='middle'>
				<DownloadOutlined className='download-icon' onClick={onDownloadClick(log.name)} />
			</Row>
		</Card>
	);
};

export default LogCard;
