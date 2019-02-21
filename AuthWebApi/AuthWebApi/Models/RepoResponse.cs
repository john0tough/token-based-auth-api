using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
   public class RepoResponse
   {
      public bool succeded { get; set; }
      public IEnumerable<string> errors { get; set; }
      
   }
}