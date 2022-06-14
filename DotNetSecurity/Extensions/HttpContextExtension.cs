using System.Security.Claims;

namespace DotNetSecurity.Extensions
{
    public static class HttpContextExtension
    {

        public static string GetUserId(this HttpContext httpContext)
        {
            string result = string.Empty;
            try
            {
                if (httpContext.User != null)
                {
                    //result = httpContext.User.Claims
                    //    .SingleOrDefault(s => s.Type.ToString().Equals("Msisdn", StringComparison.InvariantCultureIgnoreCase))
                    //    .Value;
                    result = httpContext.User.Claims
                      .SingleOrDefault(s => s.Type.ToString().Equals(ClaimTypes.NameIdentifier, StringComparison.InvariantCultureIgnoreCase))
                      .Value;
                    
                }
            }
            catch (Exception ex)
            {

                // throw;               
                result = string.Empty;
            }
            return result;
        }
    }
}
