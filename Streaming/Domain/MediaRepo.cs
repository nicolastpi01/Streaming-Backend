using NuGet.ProjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Domain
{
    public class MediaRepo
    {
        public System.Collections.ArrayList listaVideos = new System.Collections.ArrayList();

        public void Add(string nombre, string ruta)
        {
            listaVideos.Add(new Media(nombre,ruta));
        }

        public string ruta(int indice) => (listaVideos[indice] as Media).Ruta;

    }
}
