using System.Collections.Generic;
using System.Threading.Tasks;
using AuthWebApi.Models;

namespace AuthWebApi.Repository
{
   public interface IRefreshTokenRepository
   {
      Client FindClient(string clientId);
      Task<bool> AddRefreshToken(RefreshToken refreshToken);
      Task<bool> RemoveRefreshToken(string refreshTokenId);
      Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
      Task<RefreshToken> FindRefreshToken(string refreshTokenId);
      List<RefreshToken> GetAllRefreshTokens();
   }
}
