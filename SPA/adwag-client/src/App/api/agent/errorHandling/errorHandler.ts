import { AxiosError } from 'axios';
import { notification } from 'antd';
import { DetailedError } from './interfaces/detailedError';
import i18n from '../../../../i18n'

export const errorHandler = (error: AxiosError) => {
	const { status } = error.response || {};

	switch (status) {
		case 400:
			handleBadRequest(error);
			break;
		case 401:
			handleUnauthorized(error);
			break;
		case 403:
			handleForbidden(error);
			break;
		case 404:
			handleNotFound(error);
			break;
		case 500:
			handleInternalServerError(error);
			break;
		default:
			break;
	}

	throw error.response;
};
function handleBadRequest(error: AxiosError<any>) {
	const { data } = error.response || {};
	// mamy 2 typy 400-tek (teoretycznie)
	// zwykła 400 - zawiera obiekt errors, który zawiera obiekty detailedErrors oraz commonErrors
	// foramularzowa - nie zawiera obiektu errors ani detailedErrors ani commonErrors,
	// ale zawiera słownik o nazwach kluczy takich, jak pole jest nazwane, czyli np. mając w formularzu
	// inputy dla pól roles oraz firstName, to dostaniemy słowniki o kluczu roles oraz firstName, a dla nich mamy jako valuesy
	// już podaną arrayke detailedErrorów

	if(typeof data === "string") {
		notification['error']({
			message: i18n.t('common:Errors.Error'),
			description: i18n.t('common:Errors.AnErrorOccured')
		});
	} else if (data.errors) {
		// 400-tka zwykła(nieformularzowa)
		let mainErrorObject = data.errors;
		if (mainErrorObject.detailedErrors) {
			let detailedErrors = mainErrorObject.detailedErrors as DetailedError[];
			detailedErrors.forEach((detailedError) => {
				const { errorCode, errorParameters } = detailedError;
				
				const errorCodeWithDot = errorCode.replace('-', '.');
				const translationKey = `detailedErrors:${errorCodeWithDot}.DescriptionFormatter`

				const args = errorParameters;

				notification['error']({
					message: i18n.t('common:Errors.Error'),
					description: i18n.t(translationKey, {args})
				});
			});
		}

		if (mainErrorObject.commonErrors) {
			console.log(mainErrorObject.commonErrors);
		}
	} else if (data) {
		// 400-tka formularzowa
		Object.keys(data).forEach((key) => {
			let detailedErrorsForCurrentKey = data[key] as DetailedError[];
			detailedErrorsForCurrentKey.forEach((detailedError) => {
				
				const { errorCode, errorParameters } = detailedError;
				
				const errorCodeWithDot = errorCode.replace('-', '.');
				const translationKey = `detailedErrors:${errorCodeWithDot}.DescriptionFormatter`

				const args = errorParameters;

				notification['error']({
					message: i18n.t('common:Errors.Error'),
					description: i18n.t(translationKey, {args})
				});
			
			});
		});
	}
}

function handleInternalServerError(error: AxiosError<any>) {
	notification['error']({
		message: 'Błąd',
		description: 'Wystąpił błąd po stronie serwera. Spróbuj ponownie'
	});
	console.log(`500: ${error.response.data}`);
}

function handleUnauthorized(error: AxiosError<any>) {
	// if (headers['www-authenticate'] === 'Bearer error="invalid_token", error_description="The token is expired"') {
	// 	console.log('TOKEN EXPIRED');
	// store.dispatch(devalidateSession());
	// }

	notification['error']({
		message: 'Błąd',
		description: 'Nie jesteś autoryzowany. Zaloguj się ponownie'
	});

	// wyślij refresh tokena pod logowanie
	// jesli znów 401, to znaczy że refresh token wygasł - trzeba zalogować ponownie.
	// if (error.config.url === '/auth/login') {
	// 	console.log('Wrong credentials');
	// }
	console.log('401: ' + error);
}

function handleForbidden(error: AxiosError<any>) {
	console.log('403: ' + error.response);
	notification['error']({
		message: 'Błąd',
		description: 'Nie masz dostępu do tego zasobu'
	});
}

function handleNotFound(error: AxiosError<any>) {
	notification['error']({
		message: 'Błąd',
		description: 'Nie znaleziono zasobu'
	});
	console.log('404: ' + error.response.statusText);
}
