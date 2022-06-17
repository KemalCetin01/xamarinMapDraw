using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DynamicAPI.Business.Service;
using DynamicAPI.Business.Constants;
using DynamicAPI.Core.Entities.Concrete;
using DynamicAPI.Core.Extensions;
using DynamicAPI.Core.Utilities.Results;
using DynamicAPI.Core.Utilities.Security.Hashing;
using DynamicAPI.Core.Utilities.Security.Jwt;
using DynamicAPI.Entities.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DynamicAPI.Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return  new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck.Data==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            //Signing(userForLoginDto);

            return new SuccessDataResult<User>(userToCheck.Data,Messages.SuccessfulLogin);
        }
        //public IResult SignIng(UserForLoginDto userForLoginDto)
        //{
        //    var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name,userForLoginDto.FirstName+" "+userToLogin.Data.LastName),
        //            new Claim(ClaimTypes.Email,userToLogin.Data.Email),
        //            new Claim(ClaimTypes.NameIdentifier,userToLogin.Data.Id.ToString()),
        //            new Claim(ClaimTypes.Role,"Admin"),
        //            new Claim(ClaimTypes.Role,"Product.List")

        //           // new Claim(ClaimTypes.Role,)
        //        };
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);

        //    var props = new AuthenticationProperties();
        //    HttpContext.Session.SetString("JWToken", result.Data.Token);
        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        //}
        public IResult UserExists(string email)
        {
            var deneme = _userService.GetByMail(email);
            if (_userService.GetByMail(email).Data!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public IDataResult<string> getUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var claims = tokenS.Claims;


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

           return new SuccessDataResult<string>(principal.GetClaimsUserId());
        }
    }
}
