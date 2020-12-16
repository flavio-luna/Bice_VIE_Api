using System;
using System.Linq;
using System.Reflection;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class Mapper
    {
        public static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new()
        {
            if (source != null && destination != null)
            {
                var sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
                var destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    var destinationProperty = destinationProperties.Find(item => item.Name.ToLower() == sourceProperty.Name.ToLower());

                    if (destinationProperty != null)
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

        }

        public static TObjetoDestino Mappear<TObjetoDestino>(this object fuente)
                    where TObjetoDestino : class, new()
        {
            var objetoDestino = Activator.CreateInstance<TObjetoDestino>();
            MatchAndMap(fuente, objetoDestino);

            return objetoDestino;
        }

    }
}
