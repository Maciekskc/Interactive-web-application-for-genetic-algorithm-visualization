import { TFunction } from 'i18next';

export const validationMessages = (t: TFunction) => {
	return {
		default: t('form:ValidationMessages.Default'),
		required: t('form:ValidationMessages.Required'),
		enum: t('form:ValidationMessages.Enum'),
		whitespace: t('form:ValidationMessages.Whitespace'),
		date: {
			format: t('form:ValidationMessages.Date.Format'),
			parse: t('form:ValidationMessages.Date.Parse'),
			invalid: t('form:ValidationMessages.Date.Invalid')
		},
		types: {
			string: t('form:ValidationMessages.TypeTemplate'),
			method: t('form:ValidationMessages.TypeTemplate'),
			array: t('form:ValidationMessages.TypeTemplate'),
			object: t('form:ValidationMessages.TypeTemplate'),
			number: t('form:ValidationMessages.TypeTemplate'),
			date: t('form:ValidationMessages.TypeTemplate'),
			boolean: t('form:ValidationMessages.TypeTemplate'),
			integer: t('form:ValidationMessages.TypeTemplate'),
			float: t('form:ValidationMessages.TypeTemplate'),
			regexp: t('form:ValidationMessages.TypeTemplate'),
			email: t('form:ValidationMessages.TypeTemplate'),
			url: t('form:ValidationMessages.TypeTemplate'),
			hex: t('form:ValidationMessages.TypeTemplate')
		},
		string: {
			len: t('form:ValidationMessages.String.Len'),
			min: t('form:ValidationMessages.String.Min'),
			max: t('form:ValidationMessages.String.Max'),
			range: t('form:ValidationMessages.String.Range')
		},
		number: {
			len: t('form:ValidationMessages.Number.Len'),
			min: t('form:ValidationMessages.Number.Min'),
			max: t('form:ValidationMessages.Number.Max'),
			range: t('form:ValidationMessages.Number.Range')
		},
		array: {
			len: t('form:ValidationMessages.Array.Len'),
			min: t('form:ValidationMessages.Array.Min'),
			max: t('form:ValidationMessages.Array.Max'),
			range: t('form:ValidationMessages.Array.Range')
		},
		pattern: {
			mismatch: t('form:ValidationMessages.Pattern.Mismatch')
		}
	};
};
