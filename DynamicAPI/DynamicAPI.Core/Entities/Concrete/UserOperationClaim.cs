using System;
using System.Collections.Generic;
using System.Text;
using DynamicAPI.Core.Entities;

namespace DynamicAPI.Core.Entities.Concrete
{
    public class UserOperationClaim:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

    }
}
