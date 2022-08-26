using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Repositories
{
    public interface IAccountManager
    {
        public Task<Account> GetAccountAsync(Guid id);
        public Task<IEnumerable<Account>> GetAccountsAsync(int take = 0);
        public Task<Account> UpdateAccountAsync(Guid id, Account account);
        public Task<bool> DeleteAccountAsync(Guid id);

        public Task<IActionResult> SignUpAsync(SignUp account);
        public Task<IActionResult> SignInAsync(SignIn account);
    }

    public class AccountRepository : EntityFrameworkRepository<AccountEntity>, IAccountManager
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountRepository(DataContext context, IMapper mapper, IConfiguration config) : base(context)
        {
            _mapper = mapper;
            _config = config;
        }



        public async Task<Account> GetAccountAsync(Guid id)
        {
            var account = _mapper.Map<Account>(await ReadRecordAsync(x => x.Id == id));
            return account ?? null!;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(int take = 0)
        {
            return _mapper.Map<IEnumerable<Account>>(await ReadRecordsAsync(take));
        }

        public async Task<Account> UpdateAccountAsync(Guid id, Account account)
        {
            if (id == account.Id)
                return _mapper.Map<Account>(await UpdateRecordAsync(x => x.Id == id, _mapper.Map<AccountEntity>(account)));

            return null!;
        }

        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            return await DeleteRecordAsync(x => x.Id == id);
        }

        public async Task<IActionResult> SignUpAsync(SignUp account)
        {
            try
            {
                if (await ReadRecordAsync(x => x.Email == account.Email) != null)
                    return new ConflictObjectResult("a user account with the same email address already exists");

                var accountEntity = _mapper.Map<AccountEntity>(account);
                accountEntity.CreatePassword(account.Password);

                await CreateRecordAsync(accountEntity);
                return new OkObjectResult("user account was successfully created");
            }
            catch { }
            return new BadRequestObjectResult("unable to process the signup request");
        }

        public async Task<IActionResult> SignInAsync(SignIn account)
        {
            try
            {
                var accountEntity = await ReadRecordAsync(x => x.Email == account.Email);

                if (accountEntity == null || !accountEntity.ValidatePassword(account.Password))
                    return new UnauthorizedObjectResult("incorrect email address or password");

                var tokenHandler = _mapper.Map<AccountTokenHandler>(accountEntity);
                return new OkObjectResult(tokenHandler.GenerateToken(_config.GetValue<string>("Secret")));
            }
            catch { }
            return new BadRequestObjectResult("unable to process the signin request");
        }
    }
}
