using System;
using System.Collections.Generic;
using System.Text;
using DynamicAPI.Core.Entities.Concrete;

namespace DynamicAPI.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
