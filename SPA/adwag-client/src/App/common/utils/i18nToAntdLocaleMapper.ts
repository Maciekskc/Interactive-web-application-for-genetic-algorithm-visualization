import { Locale } from 'antd/lib/locale-provider';
import en from 'antd/lib/locale/en_US';
import pl from 'antd/lib/locale/pl_PL';
import i18n from 'i18n';

export const i18nToAntdLocaleMapper = (): Locale => {
	if (i18n.language === 'pl') {
		return pl;
	} else {
		return en;
	}
};
