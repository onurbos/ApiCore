
using ApiCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using ApiCore.Helpers;

namespace ApiCore
{
    public partial class Startup
    {
        public static void DIRegister(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("DBConnection")));
            services.AddDbContext<AuthDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("AuthDBConnection")));

            services.AddScoped(p => new AppDBContext(p.GetService<DbContextOptions<AppDBContext>>(), configuration));
        
            //Register all repositories
            foreach (var assembly in new[] { "ApiCore" })
            {
                var assemblies = Assembly.Load(assembly);
                var allServices = assemblies.GetTypes().Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && t.GetTypeInfo().Name.EndsWith("Repository")).ToList();

                foreach (var type in allServices)
                {
                    var allInterfaces = type.GetInterfaces().Where(x => x.Name.EndsWith("Repository")).ToList();
                    var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                    foreach (var itype in mainInterfaces)
                    {
                        if (allServices.Any(x => x != type && itype.IsAssignableFrom(x)))
                        {
                            throw new Exception("The " + itype.Name + " type has more than one implementations, please change your filter");
                        }

                        services.AddTransient(itype, type);
                    }
                }
            }

            services.AddScoped<IUserHelper, UserHelper>();
        }
    }
}
