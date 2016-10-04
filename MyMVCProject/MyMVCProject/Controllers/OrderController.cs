using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMVCProject.Models;
using System.Web.Helpers;
using System.Data.Entity.Validation;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace MyMVCProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        ShopDBEntities OrderDetails = new ShopDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchProduct()
        {
            var productName = Request["searchProduct"].ToString();
            ViewBag.SearchString = productName;
            var products = OrderDetails.ProductsDatas.Where(i => i.Name.Contains(productName)).ToList();
            if (products.Count > 0)
            {                
                return View(products);
            }
            else
            {
                ViewData["NoProducts"] = 1;
                return View();
            }
        }

        protected void orderEmail_TextChanged(object sender, EventArgs e)
        {

            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<YourGmailIDHere@gmail.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + Request["orderEmail"].ToString().Trim() + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (GetResponseCode(ResponseString) == 550)
            {
                Response.Write("Mai Address Does not Exist !<br/><br/>");
                Response.Write("<B><font color='red'>Original Error from Smtp Server :</font></b>" + ResponseString);
            }
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();
        }
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }    

        public ActionResult OrderRequest(int? Id)
        {
            var products = OrderDetails.ProductsDatas.Where(i => i.ID == Id).ToList();
            return View(products);
        }
                
        [HttpPost]
        public ActionResult PlaceOrder()
        {
            var productId = Convert.ToInt32(Request["ProductId"]);
            var email = Request["orderEmail"];
            var shipping = Request["orderShipping"];
            try
            {                
                var products = OrderDetails.ProductsDatas.Where(i => i.ID == productId).ToList();
                var product = products.Take(1).ToList();
                if (product == null)
                {
                    Response.Redirect("~/");
                }
                if (ModelState.IsValid)
                {
                    var user = new UserDetail { Email = email, Address = shipping };
                    ViewBag.Info = string.Format("{0}, {1}", user.Email, user.Address);
                    OrderDetails.UserDetails.Add(user);

                    //If there is no error try to process order
                    var body = "Thank you, we have received your order for " + Request["orderQty"] + " unit(s) of " + product[0].Name + "!<br/>";

                    if (shipping != string.Empty)
                    {
                        //Replace carriage returns with HTML breaks for HTML mail
                        var formattedOrder = shipping.Replace("\r\n", "<br/>");
                        body += "Your address is: <br/>" + formattedOrder + "<br/>";
                    }
                    body += "Your total is $" + product[0].Price * Convert.ToDouble(Request["orderQty"]) + ".<br/>";
                    body += "We will contact you if we have questions about your order.  Thanks!<br/>";

                    //SMTP Configuration for Hotmail
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;

                    //Enter your Hotmail credentials for UserName/Password and a "From" address for the e-mail
                    WebMail.UserName = "racktaka@gmail.com";
                    WebMail.Password = "racktaka@123";
                    WebMail.From = "racktaka_ind@gmail.com";

                    if (WebMail.UserName == string.Empty || WebMail.Password == string.Empty || WebMail.From == string.Empty)
                    {
                        ViewData["NoEmail"] = 1;
                    }
                    else
                    {
                        OrderDetails.SaveChanges();
                        WebMail.Send(to: email, subject: "RackTaka - New Order", body: body);
                    }
                }                
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"", ve.PropertyName, eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName), ve.ErrorMessage);
                    }
                }
                ViewBag.exception = ex;                
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex;
            }
            return View();
        }
    }
}