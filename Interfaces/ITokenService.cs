using Tweet_Api.Entities;

namespace Tweet_Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}