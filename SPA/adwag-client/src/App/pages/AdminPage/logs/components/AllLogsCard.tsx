import React from 'react';
import { Card, Row } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import { useTranslation } from 'react-i18next';

interface AllLogsCardProps {
	onDownloadClick: () => void;
}

const AllLogsCard: React.FC<AllLogsCardProps> = ({ onDownloadClick }: AllLogsCardProps) => {
	const { t } = useTranslation('page');

	return (
		<Card bordered={true} title={t('AdminPage.AllLogsCard.CardTitle')} className='card card-all-logs'>
			<Row justify='center' align='middle'>
				<DownloadOutlined className='download-icon' onClick={onDownloadClick} />
			</Row>
		</Card>
	);
};

export default AllLogsCard;
