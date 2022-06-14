using DynamicAPI.Core.Utilities.Results;
using DynamicAPI.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Business.Service
{
    public interface IKonumService
    {
        IDataResult<List<Location>> getList();
        IDataResult<Location> getById(int Id);
        IDataResult<List<Location>> getTodayByUserId(int userID);
        IResult Add(Location location);
        IResult Delete(Location location);
        IResult Update(Location location);
    }
}
