using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperledger.Aries.Agents;
using Hyperledger.Aries.Features.DidExchange;
using Microsoft.AspNetCore.Mvc;

namespace WebEdgeClient.Controllers
{
    public class MessagingController : Controller
    {
        private readonly IAgentLoader agentLoader;
        private readonly IAgentProvider agentProvider;
        private readonly IConnectionService connectionService;
        private readonly IMessageService messageService;

        public MessagingController(
            IAgentLoader agentLoader,
            IAgentProvider agentProvider,
            IConnectionService connectionService,
            IMessageService messageService
            ) {
            this.agentLoader = agentLoader;
            this.agentProvider = agentProvider;
            this.connectionService = connectionService;
            this.messageService = messageService;
        }
        public async Task<IActionResult> SendMessageAsync(string agentId)
        {
            var context = await agentLoader.LoadAgentForWallet(agentId);

            return View();
        }
    }
}
