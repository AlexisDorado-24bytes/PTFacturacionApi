using System.Collections.Generic;

namespace Aplication.Wrappers
{
    //Clase para darle un formato organizado a la respuesta de la api
    public class Response<T>
    {
        private readonly T data;

        public Response()
        {

        }

        //Cuando todo sale bien
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        //Cuando hay errores
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

    }
}
