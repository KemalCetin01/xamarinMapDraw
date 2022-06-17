using DynamicAPI.Business.Constants;
using DynamicAPI.Business.Service;
using DynamicAPI.Core.Utilities.Business;
using DynamicAPI.Core.Utilities.Results;
using DynamicAPI.DataAccess.Service;
using DynamicAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Business.Concrete
{
    public class KonumManager:IKonumService
    {
        private IKonumDal _konumDal;
        private IUserDal _userDal;
        public KonumManager(IKonumDal konumDal, IUserDal userDal)
        {
            _konumDal = konumDal;
            _userDal = userDal;
        }
        private IResult CheckIfKonumNotExists(int konumId)
        {
            var result = _konumDal.GetList(p => p.Id == konumId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.KonumNotExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfKonumExists(string konumName)
        {
            var result = _konumDal.GetList(p => p.LatLongt == konumName).Any();
            if (result)
            {
                return new ErrorResult(Messages.KonumNameAlreadyExists);
            }

            return new SuccessResult();
        }
        public IResult Add(Location location)
        {
            //IResult result = BusinessRules.Run(CheckIfKonumNotExists(konum.Id), CheckIfKonumExists(konum.konum));//Odanın bağlı olduğu bina yoksa veya Aynı oda ismi varsa
            //if (result != null)
            //{
            //    return result;
            //}
            _konumDal.Add(location);
            return new SuccessResult(Messages.KonumAdded);
        }

        public IResult Delete(Location location)
        {
            _konumDal.Delete(location);
            return new SuccessResult(Messages.KonumDeleted);
        }

        public IDataResult<Location> getById(int Id)
        {
            return new SuccessDataResult<Location>(_konumDal.Get(p => p.Id == Id));
        }
        public IDataResult<List<Location>> getTodayByUserId(int userID)
        {
            return new SuccessDataResult<List<Location>>(_konumDal.GetList(p => p.UserID == userID).Where(a => a.UserID == userID && a.createTime == DateTime.Today).OrderBy(x=>x.Id).ToList());
        }

        public IDataResult<List<Location>> getList()
        {
            return new SuccessDataResult<List<Location>>(_konumDal.GetList().ToList());
        }

        public IResult Update(Location konum)
        {
            _konumDal.Update(konum);
            return new SuccessResult(Messages.KonumUpdated);
        }
    }
}
