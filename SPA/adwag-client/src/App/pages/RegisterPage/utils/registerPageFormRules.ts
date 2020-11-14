import { Rule } from 'antd/lib/form';
import i18n from 'i18n';

export const registerFormRules = {
    email: (): Rule[] => [{ required: true, type: 'email', max: 255 }],
    firstName: (): Rule[] => [{ required: true, max: 255 }],
    lastName: (): Rule[] => [{ required: true, max: 255 }],
    roleName: (): Rule[] => [{ required: true, type: 'array' }],
    password: (arg: string): Rule[] => [
        {
            pattern: RegExp('\\d'),
            message: i18n.t('form:ValidationMessages.Custom.MustContainNumber', { arg })
        },
        {
            pattern: RegExp('[*@!#%&()^~{}]+'),
            message: i18n.t('form:ValidationMessages.Custom.MustContainSpecial', { arg })
        },
        {
            pattern: RegExp('(?=.*[a-z])'),
            message: i18n.t('form:ValidationMessages.Custom.MustContainLowercase', { arg })
        },
        {
            pattern: RegExp('(?=.*[A-Z])'),
            message: i18n.t('form:ValidationMessages.Custom.MustContainUppercase', { arg })
        },
        {
            min: 6
        },
        {
            required: true,
            max: 100
        }
    ],

    confirmPassword: (arg: string): Rule[] => [
        {
            required: true
        },
        ({ getFieldValue }) => ({
            validator(rule, value) {
                if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                }

                const arg = [i18n.t('form:User.Labels.Password'), i18n.t('form:User.Labels.ConfirmPassword')]
                return Promise.reject(i18n.t('form:ValidationMessages.Custom.PasswordMismatch', { arg }));
            }
        })
    ]

};