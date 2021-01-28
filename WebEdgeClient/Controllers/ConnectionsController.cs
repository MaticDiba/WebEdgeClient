using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperledger.Aries.Agents;
using Hyperledger.Aries.Extensions;
using Hyperledger.Aries.Features.DidExchange;
using Hyperledger.Aries.Routing;
using Hyperledger.Aries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebEdgeClient.Models;

namespace WebEdgeClient.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class ConnectionsController : Controller
    {
        private readonly IAgentLoader _agentLoader;
        private readonly IConnectionService connectionService;
        private readonly IMessageService messageService;

        public ConnectionsController(
            IAgentLoader agentLoader,
            IConnectionService connectionService,
            IMessageService messageService
            )
        {
            _agentLoader = agentLoader;
            this.connectionService = connectionService;
            this.messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateInvitation(string agentId)
        {
            var context = await _agentLoader.LoadAgentForWallet(agentId);
            //var context = await agentPr1ovider.GetContextAsync();
            var (invitation, connection) = await connectionService.CreateInvitationAsync(context, new InviteConfiguration { 
                AutoAcceptConnection = true,
                MyAlias = new ConnectionAlias
                {
                    ImageUrl = "myimage",
                    Name = agentId,
                },
                //TheirAlias = new ConnectionAlias
                //{
                //    ImageUrl = "theirimage",
                //    Name = "Their name",
                //}
            });

            return Json(new
            {
                Invitation = invitation,
                Encoded = EncodeInvitation(invitation),
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequestAsync([FromBody] AcceptConnectionForAgentViewModel model)
        {
            var context = await _agentLoader.LoadAgentForWallet(model.WalletId);

            var json = model.InvitationDetails.FromBase64();

            var invite = JsonConvert.DeserializeObject<ConnectionInvitationMessage>(json);
            var (request, record) = await connectionService.CreateRequestAsync(context, invite);
            request.ImageUrl = "request imgUrl";
            request.Label = model.WalletId;

            await messageService.SendAsync(context, request, record);

            var record2tmpb = await connectionService.ListAsync(context);
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetConnectionsAsync(string agentId)
        {
            var context = await _agentLoader.LoadAgentForWallet(agentId);
            var connections = await connectionService.ListAsync(context);

            return Ok(connections);
        }



        private string EncodeInvitation(ConnectionInvitationMessage invitation)
        {
            return invitation.ToJson().ToBase64();
        }
    }
}
