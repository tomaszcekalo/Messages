using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;
using System.Web.Http;

namespace Messages.Filters
{
    public class BasicLoginAttribute : ActionFilterAttribute
    {
        (string, string)? ExtractUserNameAndPassword(ActionExecutingContext context)
        {
            byte[] credentialBytes;

            try
            {
                var authString = context.HttpContext.Request.Headers["Authorization"].ToString();
                if(string.IsNullOrWhiteSpace(authString)) {return null;}
                var authArray = authString.Split(new char[] { ' ' });
                credentialBytes = Convert.FromBase64String(authArray[1]);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);

            return (userName, password);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var credentials=ExtractUserNameAndPassword(context);
            if(credentials is null)
            {
                context.Result = new UnauthorizedResult();
                return;
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            if(credentials.Value.Item1 != "tom" && credentials.Value.Item2 != "cek")
            {
                context.Result = new UnauthorizedResult();
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            base.OnActionExecuting(context);

        }
    }
}
