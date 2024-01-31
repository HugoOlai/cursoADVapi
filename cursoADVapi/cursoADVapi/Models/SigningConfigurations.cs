using cursoADVapi.Model.ViewModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace cursoADVapi.Models
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);

            var sig = SigningCredentials;
        }

        public dynamic Failed()
        {
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }

        public dynamic Success(string token, UsuarioViewModel view, DateTime dataCriacao, DateTime dataExpiracao)
        {
            return new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                obj = view,
                message = "OK"
            };
        }
    }
}
