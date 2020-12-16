using BanBice.Cl.IndicadoresEconomicos.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Entities
{
    public class IndicadoresEconomicosContext : DbContext
    {
        public IndicadoresEconomicosContext(DbContextOptions<IndicadoresEconomicosContext> options) : base(options)
        {

        }


        //clases
        public DbSet<UsuarioEntity> Usuarios { get; set; }

        public DbSet<FavoritoEntity> Favoritos { get; set; }

        public DbSet<AlertaEntity> Alertas { get; set; }

        public DbSet<NotificacionEntity> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {            

            modelBuilder.Entity<UsuarioEntity>().HasData(AppVariables.ListaUsuarios);

            modelBuilder.Entity<FavoritoEntity>().HasData(AppVariables.ListaFavoritos);

            

            //modelBuilder.Entity<AlertaEntity>().HasData(
            //   new AlertaEntity() { },
            //   new AlertaEntity() { }
            //   );
            //
            //modelBuilder.Entity<NotificacionEntity>().HasData(
            //    new NotificacionEntity() { },
            //    new NotificacionEntity() { },
            //    new NotificacionEntity() { },
            //    new NotificacionEntity() { },
            //    new NotificacionEntity() { },
            //    new NotificacionEntity() { }
            //    );

        }

    }
}
