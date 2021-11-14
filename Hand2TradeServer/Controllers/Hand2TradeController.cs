using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hand2TradeServerBL.Models;
using System.IO;
using Hand2TradeServer.DTO;
using Hand2TradeServer.Sevices;


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

        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("SignUp")]
        [HttpPost]
        public UserDTO SignUp([FromBody] UserDTO a)
        {
           
            User p = context.AddUser(a.Passwrd, a.UserName, a.Email, a.Coins, a.Adress, a.BirthDate, a.TotalRank, a.IsAdmin, a.IsBlocked, a.CreditNum, a.CardDate, a.CVV);
            if (p != null)
            {
                UserDTO uDTO = new UserDTO(p);
                HttpContext.Session.SetObject("user", uDTO);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return uDTO;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
    }
    
}
