using DynamicAPI.Core.DataAccess;
using DynamicAPI.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.DataAccess.Service
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
