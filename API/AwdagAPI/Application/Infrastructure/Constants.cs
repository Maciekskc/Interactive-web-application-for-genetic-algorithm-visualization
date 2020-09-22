namespace Application.Infrastructure
{
    public static class Constants
    {
        public const string CheckEmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string CheckPasswordRegex = @"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{6,}$";
        public const string SendEmailAfterRegistrationPLPath = "../API/wwwroot/EmailTemplates/SendEmailAfterRegistration/SendEmailAfterRegistrationTemplatePL.html";
        public const string SendEmailAfterRegistrationENPath = "../API/wwwroot/EmailTemplates/SendEmailAfterRegistration/SendEmailAfterRegistrationTemplateEN.html";
        public const string SendPasswordResetEmailPLPath = "../API/wwwroot/EmailTemplates/SendPasswordResetEmail/SendPasswordResetEmailTemplatePL.html";
        public const string SendPasswordResetEmailENPath = "../API/wwwroot/EmailTemplates/SendPasswordResetEmail/SendPasswordResetEmailTemplateEN.html";
    }
}