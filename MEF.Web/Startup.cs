using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEF.Providers;
using MEF.Providers.Abstract;
using MEF.Providers.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MEF.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogProvider, LogProvider>();

            services.AddMvc();
           
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();
        }
    }
}
