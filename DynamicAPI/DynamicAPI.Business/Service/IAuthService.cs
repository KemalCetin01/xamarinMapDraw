using DynamicAPI.Core.Utilities.Results;
using DynamicAPI.Core.Entities.Concrete;
using DynamicAPI.Core.Utilities.Security.Jwt;
using DynamicAPI.Entities.Dtos;

namespace DynamicAPI.Business.Service
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto,string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<string> getUserId(string token);

    }
}
