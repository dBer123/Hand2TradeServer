using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hand2TradeServerBL.Models;

namespace Hand2TradeServer.Controllers
{
    [Route("Hand2TradeAPI")]
    [ApiController]
    public class Hand2TradeController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        Hand2TradeDBContext context;
        public Hand2TradeController(Hand2TradeDBContext context)
        {
            this.context = context;
        }
        #endregion
    }
}
