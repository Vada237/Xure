namespace Xure.App.Models
{
    public class TwoFactorAuthentificatorModel
    {
        public string AuthenticatorKey { get; set; }
        public string Code { get; set; }
    }

    public class LoginTwoStepModel { 
        public string ReturnUrl { get; set; }
        public string Token { get; set; }        
        public string Code { get; set; }
    }    
}
