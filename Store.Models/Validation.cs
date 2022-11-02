using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Models
{
    public class Validation
    {
        public enum Exceptions
        {
            COMPLETED,
            EXCEPTION,
            SESSION_EXPIRED
        };

        public enum SuggestedMessages
        {
            ErrorSql,
            ErrorService,
            Welcome,
            OperationSuccessful,
            OperationFailed,
        }

        public static string Exception(Exceptions exception)
        {
            return exception.ToString();
        }

        public static string SuggestedMessage(SuggestedMessages message)
        {
            switch (message)
            {
                case SuggestedMessages.ErrorSql:
                    return "Ha ocurrido un error y puede que el servicio no este disponible temporalmente."; 
                case SuggestedMessages.ErrorService:
                    return "Ha ocurrido un error en el servicio.";
                case SuggestedMessages.Welcome:
                    return "Bienvenid(a) a Banca Móvil."; 
                case SuggestedMessages.OperationSuccessful:
                    return "Operación Exitosa."; 
                case SuggestedMessages.OperationFailed:
                    return "Operación Fallada."; 
                default:
                    return "Sin Mensaje"; 
            }
        }
    }
}
