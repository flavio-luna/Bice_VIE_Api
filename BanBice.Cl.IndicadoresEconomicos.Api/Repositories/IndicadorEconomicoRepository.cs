using BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories
{
    public class IndicadorEconomicoRepository : IIndicadorEconomicoRepository
    {
        private readonly string _UrlMiIndicador = "https://www.mindicador.cl/api";

        private async Task<T> LlamarApiMiIndicador<T>(string queryString) where T : class, new()
        {
            var indicadores = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_UrlMiIndicador + queryString);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(string.Empty);
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    indicadores = JsonConvert.DeserializeObject<T>(response);
                }
            }
            return indicadores;
        }


        public async Task<RootIndicadorPeriodo> GetIndicadoresPorTipo(string tipo)
        {
            return await LlamarApiMiIndicador<RootIndicadorPeriodo>("/" + tipo);
        }

        public async Task<RootIndicadorPeriodo> GetIndicadoresPorTipoYAnno(string tipo, string anno)
        {
            return await LlamarApiMiIndicador<RootIndicadorPeriodo>("/" + tipo + "/" + anno);
        }

        public async Task<RootIndicadorPeriodo> GetIndicadoresPorTipoYFecha(string tipo, DateTime fecha)
        {
            return await LlamarApiMiIndicador<RootIndicadorPeriodo>("/" + tipo + "/" + fecha.ToString("dd-MM-yyyy"));
        }

        public async Task<RootTodos> GetTodos()
        {
            return await LlamarApiMiIndicador<RootTodos>("");
        }
    }
}
