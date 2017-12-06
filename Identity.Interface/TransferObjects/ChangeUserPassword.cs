namespace Identity.Interface.TransferObjects
{
    public class ChangeUserPassword
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}