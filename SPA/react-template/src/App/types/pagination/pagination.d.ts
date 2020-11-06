export interface IPageQueryParams {
	pageNumber: number;
	pageSize: number;
	query: string | null;
	orderBy: string | null;
	totalNumberOfItems?: number;
}

export type ICollectionResponse<T> = {
	data: T[];
	totalNumberOfItems: number;
} & IPageQueryParams;
