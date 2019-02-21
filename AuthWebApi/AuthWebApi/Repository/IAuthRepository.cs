using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthWebApi.Repository
{
   public interface IAuthRepository<TAuthUser, TAuthResponse>: IDisposable
   {
      Task<TAuthResponse> RegisterUser(TAuthUser user);
      Task<TAuthUser> FindUser(string userName, string password);
   }
}