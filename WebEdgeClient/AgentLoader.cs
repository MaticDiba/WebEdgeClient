﻿using Hyperledger.Aries.Agents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace WebEdgeClient
{
    public class AgentLoader : IAgentLoader
    {

        internal const string MediatorConnectionIdTagName = "MediatorConnectionId";

        public AgentLoader()
        {

        }

        public async Task<DefaultAgentContext> LoadAgentForWallet(string walletId)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAriesFramework(builder =>
                    {
                        builder.RegisterEdgeAgent(options =>
                        {
                            options.EndpointUri = "http://localhost:5000";
                            //options.EndpointUri = "http://localhost:5000?wallet_id=" + walletId;
                            options.WalletConfiguration.Id = walletId;
                        });
                    });
                });
            var agentHost = host.Build();
            await agentHost.StartAsync();

            var context = await agentHost.Services.GetRequiredService<IAgentProvider>().GetContextAsync();

            return context as DefaultAgentContext;
        }
    }

}
