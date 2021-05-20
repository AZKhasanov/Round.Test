using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteMonitoring.Core.Security;

namespace SiteMonitoring.Core.Extensions
{
    public static class AuthOptionsExtension
    {
        public static IServiceCollection AddAuthOptions(this IServiceCollection serviceCollection, IConfiguration configuration, out AuthOptions options)
        {
            options = new AuthOptions()
            {
                Issuer = configuration["ISSUER"],
                Audience = configuration["AUDIENCE"],
                Secret = configuration["SECRET"],
                Lifetime = byte.Parse(configuration["LIFETIME"])
            };

            serviceCollection.AddSingleton(options);

            return serviceCollection;
        }
    }
}
