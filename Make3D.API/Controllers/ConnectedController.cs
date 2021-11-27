using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Make3D.API.Controllers
{
    public class ConnectedController : ControllerBase
    {
        protected int? GetConnectedUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
        }
    }
}
