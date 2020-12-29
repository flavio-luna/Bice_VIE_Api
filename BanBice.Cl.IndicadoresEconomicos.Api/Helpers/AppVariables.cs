using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class AppVariables
    {
        public static string ServidorSql = @"FLAVIO-PC\SQLSERVER";
        public static string NombreBaseDatos = "BanBice.InidicadoresEconomicos";
        public static int LargoSalt = 20;
        private static string Salt = Crypto.CrearSalt(LargoSalt);
        public static int DuracionTokenMinutos =  300; // 5 horas.
        public static string AlgoritmoSeguridad { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public static string LlaveToken = "keyTokenBancoBice";
        public static int IntentosParaBloqueoUsuario = 5;
        public static int DiasPermitidosParaValidarCorreo = 1;

        public static List<UsuarioEntity> ListaUsuarios = new List<UsuarioEntity>() {
            new UsuarioEntity() {
                Id = Guid.Parse("b3eef3ec-3fb5-4025-8490-b92ae7f4336f"),
                NombreCompleto = "Flavio Luna",
                Correo = "flavio.luna@bittobyte.cl",
                IdValidacionCorreo = Guid.NewGuid(),
                FechaEnvioValidacion = DateTime.Now,
                CorreoValidado = true,
                Edad= 33,
                HashContrasenna = Crypto.CrearHashSHA256("luna", Salt),
                SaltContrasenna = Salt,
                Bloqueado = false,
                Intentos = 0
            },
           

            new UsuarioEntity()
            {
                Id = Guid.NewGuid(),
                NombreCompleto = "Usuario Bloqueado",
                Correo = "bloqueado@bittobyte.cl",
                IdValidacionCorreo = Guid.NewGuid(),
                FechaEnvioValidacion = DateTime.Now.AddDays(-30),
                CorreoValidado = true,
                Edad= 33,
                HashContrasenna = Crypto.CrearHashSHA256("bloqueado", Salt),
                SaltContrasenna = Salt,
                Bloqueado = true,
                Intentos = 0
            }
        };

        public static List<FavoritoEntity> ListaFavoritos = new List<FavoritoEntity>() {
                new FavoritoEntity() {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.Parse("b3eef3ec-3fb5-4025-8490-b92ae7f4336f"),
                    CodigoInidicador = "uf"
                },
                new FavoritoEntity()
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.Parse("b3eef3ec-3fb5-4025-8490-b92ae7f4336f"),
                    CodigoInidicador = "dolar"
                },
                new FavoritoEntity()
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.Parse("b3eef3ec-3fb5-4025-8490-b92ae7f4336f"),
                    CodigoInidicador = "euro"
                },
                new FavoritoEntity()
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.Parse("cec0a7bc-ea1d-4c9d-ac5f-457ff4cda1c4"),
                    CodigoInidicador = "tasa_desempleo"
                },
                new FavoritoEntity()
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = Guid.Parse("cec0a7bc-ea1d-4c9d-ac5f-457ff4cda1c4"),
                    CodigoInidicador = "bitcoin"
                }
        };


    }
}
