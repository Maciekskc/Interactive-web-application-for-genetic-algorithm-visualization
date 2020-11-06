import { IPageQueryParams } from 'App/types/pagination/pagination';

const defaultPageQueryParams: IPageQueryParams = {
	orderBy: null,
	pageNumber: 1,
	pageSize: 10,
	query: ''
};

export default defaultPageQueryParams;
