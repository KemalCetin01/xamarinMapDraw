using DynamicAPI.Core.Entities.Concrete;
using DynamicAPI.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Business.Service
{
   public interface IUserService
    {
        IDataResult<List<User>> getList();
        IDataResult<User> getById(int Id);
        IDataResult<User> GetByMail(string email);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
        List<OperationClaim> GetClaims(User user);

    }
}
