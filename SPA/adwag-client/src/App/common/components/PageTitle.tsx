import React from 'react';

import { PageHeader } from 'antd';

interface PageTitleProps {
	title: string;
	icon?: React.ReactNode;
}

const PageTitle: React.FC<PageTitleProps> = (props: PageTitleProps) => {
	return (
		<div className='m-auto w-fit'>
			{props.icon ? (
				<PageHeader title={props.title} avatar={{ icon: props.icon }} />
			) : (
				<PageHeader title={props.title} />
			)}
		</div>
	);
};

export default PageTitle;
