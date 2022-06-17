using DynamicAPI.Core.DataAccess.EntityFramework;
using DynamicAPI.DataAccess.Concrete.EntityFramework.Contexts;
using DynamicAPI.DataAccess.Service;
using DynamicAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.DataAccess.Concrete.EntityFramework
{
    public class EfKonumDal : EfEntityRepositoryBase<Location, DynamicAPIContext>, IKonumDal
    {
    }
}
