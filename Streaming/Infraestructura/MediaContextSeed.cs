using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura
{
    public class MediaContextSeed
    {
        
        private MediaContext _context;

        public async Task SeedAsync(MediaContext context)
        {
            this._context = context;
            using (context)
            {
                //ESTE SEED LO QUE HACE ES QUE SI NO HAY DATOS DE CONFIGURACION , QUE SE IMPORTANTAN DESDE ARCHIVOS CSV, SE REALIZA
                //LA MIGRACIÓN AUTOMATICAMENTE

                //context.Database.Migrate();
                
                if (!context.Media.Any())
                {
                    
                    var media = new MediaEntity();
                    media.monedaOrigen = "USD";
                    media.monedaDestino = "USD";
                
                    media.porcentaje = 10;
                    context.Media.Add(media);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

/*
 * 
 * public class CommonContextSeed
    {
        private CommonContext _context;

        public async Task SeedAsync(CommonContext context, IHostingEnvironment env, ICSVLoaderService loaderCSV)
        {
            this._context = context;
            using (context)
            {
                //ESTE SEED LO QUE HACE ES QUE SI NO HAY DATOS DE CONFIGURACION , QUE SE IMPORTANTAN DESDE ARCHIVOS CSV, SE REALIZA
                //LA MIGRACIÓN AUTOMATICAMENTE

                context.Database.Migrate();
                if (!context.comisionParametrias.Any())
                {
                    //context.comisionParametrias.AddRange(loaderCSV.ObtenerModel<ComisionParametriaCSVModel, ComisionParametria>("Conceptos.csv", ";"));
                }
                if (!context.MonedaParametrizacion.Any())
                {
                    var moneda = new MonedaParametria();
                    moneda.monedaOrigen = "USD";
                    moneda.monedaDestino = "USD";
                    moneda.porcentaje = 10;
                    context.MonedaParametrizacion.Add(moneda);
                }
                await context.SaveChangesAsync();
            }
        }
    }

    */

