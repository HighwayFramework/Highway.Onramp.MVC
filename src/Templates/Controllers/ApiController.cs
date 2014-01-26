using Highway.Data;
using Highway.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Templates.BaseTypes;
using Templates.Entities;

namespace Templates.Controllers
{
    public class ApiController : BaseRestApiController<Domain, ExampleEntity, Guid>
    {
        public ApiController(IDomainRepositoryFactory factory)
            : base(factory, RestOperations.All)
        {
        }
    }
}
