using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Repositories
{
    public interface IAuthManager
    {
        public Task<IActionResult> SignUpAsync(SignUp model);
        public Task<IActionResult> SignInAsync(SignIn model);
    }

    public interface IUserManager
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(Guid id);
    }

    public class UserRepository : GenericRepository<UserEntity>, IUserManager, IAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IConfiguration configuration, IMapper mapper) : base(context)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return _mapper.Map<User>(await GetAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<User>>(await GetAllAsync());
        }

        public async Task<IActionResult> SignInAsync(SignIn model)
        {
            try
            {
                var user = await GetAsync(x => x.Email == model.Email);
                if (user == null || !user.ValidatePassword(model.Password))
                    return new BadRequestObjectResult("Incorrect email address or password.");

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Email)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"))),
                        SecurityAlgorithms.HmacSha512Signature)
                };

                return new OkObjectResult(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));


            }   
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }




        public async Task<IActionResult> SignUpAsync(SignUp model)
        {
            try
            {
                if (await GetAsync(x => x.Email == model.Email) != null)
                    return new ConflictObjectResult("A user with the same email address already exists.");

                var userEntity = _mapper.Map<UserEntity>(model);
                userEntity.CreatePassword(model.Password);
                
                await CreateAsync(userEntity);
                return new OkResult();
            }
            catch
            {
                return new BadRequestObjectResult("Something went wrong when we tried to create your account");
            }
        }
    }
}
