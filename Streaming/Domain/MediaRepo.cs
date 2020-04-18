using System.Collections.Generic;

namespace Streaming.Domain
{
    public class MediaRepo
    {
        //TODO:un map que se llama lista, imaginar algo mejor
        public Dictionary<int,Media> listaVideos = new Dictionary<int, Media>();
        private int lastIndex = 0;

        public void Add(string nombre, string ruta)
        {
            listaVideos.Add(lastIndex,new Media(nombre,ruta));
        }

        public string ruta(int indice) => listaVideos[indice].Ruta;
    }
}
