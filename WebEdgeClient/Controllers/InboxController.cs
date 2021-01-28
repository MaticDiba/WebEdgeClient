using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperledger.Aries.Agents;
using Hyperledger.Aries.Routing;
using Hyperledger.Aries.Utils;
using Microsoft.AspNetCore.Mvc;

namespace WebEdgeClient.Controllers
{
    public class InboxController : Controller
    {
        private readonly IAgentLoader _agentLoader;
        private readonly IEdgeClientService edgeClientService;
        private readonly IMessageService messageService;

        public InboxController(
            IAgentLoader agentLoader,
            IEdgeClientService edgeClientService,
            IMessageService messageService
            )
        {
            _agentLoader = agentLoader;
            this.edgeClientService = edgeClientService;
            this.messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> FetchInboxAsync(string agentId)
        {
            var context = await _agentLoader.LoadAgentForWallet(agentId);

            var inbox1 = await edgeClientService.FetchInboxAsync(context);
            foreach(var inboxItem in inbox1.unprocessedItems)
            {
                var packedMessage = new PackedMessageContext(inboxItem.Data);
                var unpacked = await CryptoUtils.UnpackAsync(context.Wallet, packedMessage.Payload);
            }


            return Ok(inbox1.unprocessedItems);
        }
        [HttpGet]
        public async Task<IActionResult> GetInboxItemsAsync(string agentId)
        {
            var context = await _agentLoader.LoadAgentForWallet(agentId);

            var inboxItems = await edgeClientService.ListInboxAsync(context);
            foreach (var inboxItem in inboxItems.Items)
            {
                var packedMessage = new PackedMessageContext(inboxItem.Data);
                var unpacked = await CryptoUtils.UnpackAsync(context.Wallet, packedMessage.Payload);
            }
            return Ok(inboxItems);
        }
    }
}
