namespace cursoADVapi.Models
{
    public class AuthViewModel
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public string AccessKey { get; set; }

        public string Ip { get; set; }

        public bool Validar()
        {
            if (string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(AccessKey))
                return false;
            return true;
        }
    }
}
