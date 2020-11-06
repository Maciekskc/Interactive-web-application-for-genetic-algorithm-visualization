import { IPageQueryParams } from 'App/types/pagination/pagination';

export const defaultPageQueryParams: IPageQueryParams = {
	orderBy: null,
	pageNumber: 1,
	pageSize: 10,
	query: ''
};

/**
 * Funkcja generująca callback `onChange` dla wszystkich tabel od ant.design.
 *
 * Parametry:
 *  - `previousPageQueryParams` - poprzedni obiekt danych paginacji. Może to być obiekt pobrany z `useSelector`a,
 *  - `dispatchCallback` - funkcja jaka się wykona z nowo utworzonym obiektem paginacji.
 * Najlepiej tutaj podać dispatch z getem na wszytkie encje. W parametrze otrzyma nowy obiekt paginacji.
 */
export const onPaginationChange = (
	previousPageQueryParams: IPageQueryParams,
	dispatchCallback: (newPageQueryParams: IPageQueryParams) => void
) => {
	let newPageQueryParams = { ...previousPageQueryParams };
	return (page: number, pageSize: number) => {
		newPageQueryParams.pageNumber = page;
		newPageQueryParams.pageSize = pageSize;
		dispatchCallback(newPageQueryParams);
	};
};
