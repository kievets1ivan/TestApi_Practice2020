namespace TestApi.DAL.Entities
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
