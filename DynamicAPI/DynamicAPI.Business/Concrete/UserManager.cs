using DynamicAPI.Business.Constants;
using DynamicAPI.Business.Service;
using DynamicAPI.Core.Utilities.Business;
using DynamicAPI.Core.Utilities.Results;
using DynamicAPI.DataAccess.Service;
using DynamicAPI.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Business.Concrete
{
    public class UserManager : IUserService
    {

        private IUserDal _userDal;
        public UserManager( IUserDal userDal)
        {
            _userDal = userDal;
        }
        private IResult CheckIfUserNotExists(int userId)
        {
            var result = _userDal.GetList(p => p.Id == userId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfUserExists(string userName)
        {
            var result = _userDal.GetList(p => p.kullaniciAdi == userName).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }

            return new SuccessResult();
        }
        public IResult Add(User user)
        {
            //IResult result = BusinessRules.Run(CheckIfUserNotExists(user.Id), CheckIfUserExists(user.kullaniciAdi));//Odanın bağlı olduğu bina yoksa veya Aynı oda ismi varsa
            IResult result = BusinessRules.Run(CheckIfUserExists(user.kullaniciAdi));//Odanın bağlı olduğu bina yoksa veya Aynı oda ismi varsa
            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> getById(int Id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == Id));
        }

        public IDataResult<List<User>> getList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
