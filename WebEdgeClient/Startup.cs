using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.RouteAnalyzer;
using Hyperledger.Aries.Agents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Hyperledger.Aries.Features.BasicMessage;
using Hyperledger.Aries.Features.DidExchange;
using Hyperledger.Aries.Features.Discovery;
using Hyperledger.Aries.Features.IssueCredential;
using Hyperledger.Aries.Features.PresentProof;
using Hyperledger.Aries.Features.Routing;
using Hyperledger.Aries.Features.TrustPing;
using Hyperledger.Aries.Agents.Edge;
using Hyperledger.Aries.Storage;
using Hyperledger.Aries.Routing;
using Hyperledger.Aries.Configuration;
using Hyperledger.Aries.Routing.Edge;
using Hyperledger.Aries.Contracts;
using Hyperledger.Aries.Ledger;
using Hyperledger.Aries.Runtime;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Hyperledger.Aries.Payments;

namespace WebEdgeClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAriesFramework(builder =>
            {
                builder.RegisterEdgeAgent(options =>
                {
                    options.EndpointUri = "http://localhost:5000";
                    ////options.EndpointUri = "http://localhost:5000?gid_uuid="+walletId;
                    options.WalletConfiguration.Id = "dummy";
                });
            });
            services.AddSingleton<IAgentLoader, AgentLoader>();

            //services.AddOptions<AgentOptions>();
            //services.AddLogging();
            //services.AddHttpClient();

            //services.AddSingleton<IEventAggregator, EventAggregator>();
            //services.AddSingleton<IProvisioningService, DefaultProvisioningService>();
            //services.AddSingleton<IAgent, DefaultAgent>();
            //services.AddSingleton<IConnectionService, DefaultConnectionService>();
            //services.AddSingleton<IMessageService, DefaultMessageService>();
            //services.AddSingleton<IMessageDispatcher, HttpMessageDispatcher>();
            //services.AddSingleton<IEdgeClientService, EdgeClientService>();
            //services.AddSingleton<IWalletService, DefaultWalletService>();
            //services.AddSingleton<IWalletRecordService, DefaultWalletRecordService>();
            //services.AddSingleton<IPoolService, DefaultPoolService>();
            //services.AddSingleton<IAgentProvider, DefaultAgentProvider
            //services.AddDefaultMessageHandlers();

            services.AddControllers();
            services.AddRouteAnalyzer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
