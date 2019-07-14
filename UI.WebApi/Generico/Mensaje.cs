using UI.WebApi.Models;

namespace UI.WebApi.Generico
{
    public static class Mensaje
    {
        public static ResponseStatusModel MensajeJson(bool error, string mensaje, string result)
        {
            return (new ResponseStatusModel() { Error = error, Message = mensaje, Result = result });
        }
    }
}