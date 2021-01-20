using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebEdgeClient.Models
{
    public class AcceptConnectionViewModel
    {
        [Required]
        public string InvitationDetails { get; set; }
    }

    public class AcceptConnectionForAgentViewModel
    {
        public string InvitationDetails { get; set; }
        public string WalletId { get; set; }
    }
}
