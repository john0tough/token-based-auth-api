using System.Collections.Generic;
using System.Threading.Tasks;
using AuthWebApi.Models;

namespace AuthWebApi.Repository
{
   public interface IRefreshTokenRepository<TRefreshToken>
   {
      Task<bool> AddRefreshToken(TRefreshToken refreshToken);
      Task<bool> RemoveRefreshToken(string refreshTokenId);
      Task<bool> RemoveRefreshToken(TRefreshToken refreshToken);
      Task<TRefreshToken> FindRefreshToken(string refreshTokenId);
      List<TRefreshToken> GetAllRefreshTokens();
   }
}
