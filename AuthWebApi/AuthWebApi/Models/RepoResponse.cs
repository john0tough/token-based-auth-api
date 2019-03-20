using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWebApi.Models
{
   public class RepoResponse
   {
      public bool Succeded { get; set; }
      public IEnumerable<string> Errors { get; set; }
   }
}