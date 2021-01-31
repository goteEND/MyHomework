using System.Threading.Tasks;
using MyHomework.API.Entities;

namespace MyHomework.API.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
