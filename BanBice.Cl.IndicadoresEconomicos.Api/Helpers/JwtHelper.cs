using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class JwtHelper
    {

        public static string GenerarToken(UsuarioEntity usuario)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(AppVariables.DuracionTokenMinutos),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), AppVariables.AlgoritmoSeguridad)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return token;
        }


        public static SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Encoding.ASCII.GetBytes(AppVariables.LlaveToken);
            return new SymmetricSecurityKey(symmetricKey);
        }

        public static bool TokenValido(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static IEnumerable<Claim> ObtenerTokenClaims(string token)
        {
             var tokenValidationParameters = new TokenValidationParameters()
             {                 
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = GetSymmetricSecurityKey(),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ClockSkew = TimeSpan.Zero
             };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return tokenValid.Claims;            
        }

        public static Guid ObtenerIdUsuario(string token) 
        {
            var claims = ObtenerTokenClaims(token);
            var guid = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (guid == null) 
            {
                throw new Exception("no hay claims");
            }
            return Guid.Parse(guid.Value);
        }
        public static Guid ObtenerIdUsuario(IEnumerable<Claim> claims)
        {
            var guid = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (guid == null)
            {
                throw new Exception("no hay claims");
            }
            return Guid.Parse(guid.Value);
        }
    }
}
