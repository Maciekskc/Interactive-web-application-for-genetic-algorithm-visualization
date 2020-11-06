import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import Backend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';

export const languages = ['en', 'pl'];
export const fullLanguages = [{key: 'en', value: 'English'}, {key: 'pl', value: 'Polski'}]

i18n.use(Backend)
	.use(LanguageDetector)
	.use(initReactI18next) // passes i18n down to react-i18next
	.init({
		lng: 'pl',
		fallbackLng: false,
		whitelist: languages,

		ns: ['page', 'common', 'form', 'detailedErrors'],
		defaultNS: 'page',
		nsSeparator: ':',

		debug: true,
		detection: {
			order: ['queryString', 'cookie'],
			cache: ['cookie']
		},

		interpolation: {
			escapeValue: false // react already safes from xss
		}
	});

export default i18n;
