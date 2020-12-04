export interface GetAquariumResponse {
	id: number;
	width: number;
	height: number;

	capacity: number;
	foodMaximalAmount: number;
	currentPopulationCount: number;
	currentFoodsAmount: number;

	hungaryHistogramData: number[];
	hungaryAvarage: number;
}
