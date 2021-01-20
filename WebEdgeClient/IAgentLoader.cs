using Hyperledger.Aries.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEdgeClient
{
    public interface IAgentLoader
    {
        Task<DefaultAgentContext> LoadAgentForWallet(string walletId);
    }
}
