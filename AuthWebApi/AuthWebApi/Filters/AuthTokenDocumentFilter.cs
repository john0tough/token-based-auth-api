﻿using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace AuthWebApi.Filters
{
   public class AuthTokenDocumentFilter : IDocumentFilter
   {
      public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
      {
         swaggerDoc.paths.Add("/token", new PathItem
         {
            post = new Operation
            {
               tags = new List<string> {"Auth"},
               consumes = new List<string>
               {
                  "application/x-www-form-urlencoded"
               },
               parameters = new List<Parameter>
               {
                  new Parameter
                  {
                     type = "string",
                     name = "grant_type",
                     required = true,
                     @in = "formData",
                     @default = "password|refresh_token"
                  },
                  new Parameter
                  {
                     type = "string",
                     name = "username",
                     required = false,
                     @in = "formData"
                  },
                  new Parameter
                  {
                     type = "string",
                     name = "password",
                     required = false,
                     @in = "formData"
                  },
                  new Parameter
                  {
                     type = "string",
                     name = "client_id",
                     required = false,
                     @in = "formData"
                  },
                  new Parameter
                  {
                     type = "string",
                     name = "refresh_token",
                     required = false,
                     @in = "formData"
                  },
               }
            }
         });
      }
   }
}