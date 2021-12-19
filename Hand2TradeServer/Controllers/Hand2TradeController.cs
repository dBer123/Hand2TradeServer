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
using System.Text.RegularExpressions;

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

        [Route("CheckEmail")]
        [HttpGet]
        public bool CheckEmail([FromQuery] string checkPass)
        {
            UserDTO user = HttpContext.Session.GetObject<UserDTO>("user");
            string pass = HttpContext.Session.GetObject<string>("EmailValidation");
            if (checkPass == pass)
            {
                context.ValidatedEmail(user.Email);
                return true;

            }
            return false;
        }


        [Route("SignUp")]
        [HttpPost]
        public UserDTO SignUp([FromBody] UserDTO a)
        {
            User p = null;
            if (IsValidCard(a.CreditNum))
            {
                p = context.AddUser(a.Passwrd, a.UserName, a.Email, a.Coins, a.Adress, a.BirthDate, a.IsAdmin, a.IsBlocked, a.CreditNum, a.CardDate, a.CVV);

            }
            if (p != null)
            {
                //Create temporary code for email validation
                Random random = new Random();
                int pass = random.Next(10000, 100000);
                try
                {
                    EmailSender.SendEmail("Validate Sign Up", $"Validation email code-{pass}", a.Email, "New User", "hand2trade1@gmail.com", "hand2trade", "daniel6839", "smtp.gmail.com");
                }
                catch
                {

                }
                UserDTO uDTO = new UserDTO(p);
                HttpContext.Session.SetObject("user", uDTO);
                HttpContext.Session.SetObject("EmailValidation", pass.ToString());
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return uDTO;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        public bool IsValidCard(string txtCardNumber)
        {
            if (txtCardNumber.StartsWith("1298") ||
                txtCardNumber.StartsWith("1267") ||
                txtCardNumber.StartsWith("4512") ||
                txtCardNumber.StartsWith("4567") ||
                txtCardNumber.StartsWith("8901") ||
                txtCardNumber.StartsWith("5326") ||
                txtCardNumber.StartsWith("8933")) return true;

            return false;
        }

        [Route("AddItem")]
        [HttpPost]
        public ItemDTO AddItem([FromBody] ItemDTO itm)
        {
            Item item = context.AddItem(itm.Price, itm.Desrciption);
            if(item != null)
            {
                ItemDTO itemDTO = new ItemDTO(item);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return itemDTO;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
    }

}
