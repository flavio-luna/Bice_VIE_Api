using BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos.Inidicadores;
using System;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos
{
    public class RootTodos
    {
        public string version { get; set; }
        public string autor { get; set; }
        public DateTime fecha { get; set; }
        public Uf uf { get; set; }
        public Ivp ivp { get; set; }
        public Dolar dolar { get; set; }
        public DolarIntercambio dolar_intercambio { get; set; }
        public Euro euro { get; set; }
        public Ipc ipc { get; set; }
        public Utm utm { get; set; }
        public Imacec imacec { get; set; }
        public Tpm tpm { get; set; }
        public LibraCobre libra_cobre { get; set; }
        public TasaDesempleo tasa_desempleo { get; set; }
        public Bitcoin bitcoin { get; set; }
    }
}
