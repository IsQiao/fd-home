namespace Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
        
        public string NewPasswordConfirmed { get; set; }
    }
}