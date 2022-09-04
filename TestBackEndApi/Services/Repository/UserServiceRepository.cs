using Microsoft.IdentityModel.Tokens;
using static TestBackEndApi.Services.Key;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestBackEndApi.Domain;
using TestBackEndApi.Data;
using TestBackEndApi.Models.ViewModels;
using TestBackEndApi.Models.ViewModels.RepositoryViewModel;
using TestBackEndApi.Models.ViewModels.ListUserViewModel;
using Microsoft.EntityFrameworkCore;

namespace TestBackEndApi.Services.Repository
{
    public class UserServiceRepository : UserServicesRepositoryImp
    {
        private readonly ApplicationDbContext _context;
        public UserServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task <User> AuthenticateAsync(string username, string password)
        {
            password = Encrypt.EncryptPassword(password);
            var user = await Task.FromResult(_context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault());

            if (user == null)
                return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("TestBackEndApi", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }

        public async Task<IEnumerable<ListUserViewModel>> GetUsersAsync()
        {
            return await _context.Users
                .Select(x => new ListUserViewModel
                {
                    Username = x.Username,
                    Password = x.Password,
                    Role = x.Role
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
