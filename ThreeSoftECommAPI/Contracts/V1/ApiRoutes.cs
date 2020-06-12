using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "V1";
        public const string Base = Root + "/" + Version;

        public static class Brands
        {
            public const string GetAll = Base + "/brands";
            public const string Get = Base + "/brands/{brandId}";
            public const string Create = Base + "/brands";
            public const string Update = Base + "/brands/{brandId}";
            public const string Delete = Base + "/brands/{brandId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string CreateRole = Base + "/identity/role";
            public const string GetRole = Base + "/identity/role/{roleId}";
        }
    }
}
