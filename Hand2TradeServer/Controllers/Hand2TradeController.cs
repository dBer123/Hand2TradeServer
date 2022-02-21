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
        public UserDTO Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                UserDTO u = new UserDTO(user);
                return u;
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
            if (itm != null)
            {
                User user = HttpContext.Session.GetObject<User>("theUser");
                Item item = context.AddItem(itm.Price, itm.Desrciption, itm.ItemName, user);
                if (item != null)
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
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

        }

        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("UpdateUser")]
        [HttpPost]
        public UserDTO UpdateContact([FromBody] UserDTO userDTO)
        {
            //If contact is null the request is bad
            if (userDTO == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the userDTO user ID
            if (user != null && user.UserId == userDTO.UserId)
            {

                //update user to the DB by marking all entities that should be modified or added
                User updated = context.UpdateUser(userDTO.Adress, userDTO.Passwrd, userDTO.UserName, userDTO.UserId);
                HttpContext.Session.SetObject("theUser", user);
                //return the contact with its new ID if that was a new userDTO
                UserDTO u = new UserDTO(updated);
                return u;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("UpdateItem")]
        [HttpPost]
        public ItemDTO UpdateContact([FromBody] ItemDTO itemDTO)
        {
           
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the userDTO user ID
            if (user != null)
            {

                //update user to the DB by marking all entities that should be modified or added
                Item updated = context.UpdateItem(itemDTO.Desrciption, itemDTO.Price, itemDTO.ItemName, itemDTO.ItemId);
                //return the contact with its new ID if that was a new userDTO
                ItemDTO i = new ItemDTO(updated);
                return i;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("DeleteItem")]
        [HttpGet]
        public bool DeleteItem([FromQuery] int id)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the userDTO user ID
            if (user != null)
            {

                bool isDeleted = context.DeleteItem(id);
                return isDeleted;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("Search")]
        [HttpGet]
        public IEnumerable<Item> Search([FromQuery] string ItemName)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the userDTO user ID
            if (user != null)
            {
                return context.Search(ItemName, user.UserId);

            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

    }

}
