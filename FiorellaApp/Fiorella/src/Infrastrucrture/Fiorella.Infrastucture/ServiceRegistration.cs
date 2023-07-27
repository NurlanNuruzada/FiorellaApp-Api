using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Infrastucture.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Infrastucture;
public static class ServiceRegistration
{
   public static void AddInfrastuctureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
    }
}
