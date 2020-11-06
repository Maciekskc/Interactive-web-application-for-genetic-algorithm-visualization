import { Rule } from 'antd/lib/form';

export const loginFormRules = {
	email: (): Rule[] => [{required: true}],
	password: (): Rule[] => [{required: true}]
}
