using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public interface ISaver
   {
      Task<int> AsyncSave();
   }
}