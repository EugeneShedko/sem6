//using Microsoft.ApplicationInsights.Extensibility.Implementation;
using sevenlabproject.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sevenlabproject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                //получаем текущий домен
                DirectoryEntry domain = new DirectoryEntry();
                //получаем различающееся имя домена
                string fullDomainName = domain.
                    Properties["distinguishedName"].
                    Value.ToString();
                //убираем DC=, 
                //чтобы привести имя домена к привычной форме
                fullDomainName = fullDomainName.Replace("DC=", "");
                //заменяем запятые в иерархии доменов на привычные точки 
                fullDomainName = fullDomainName.Replace(",", ".");
                //передаем имя домена в представление
                ViewBag.Domain = fullDomainName;
                //передаем имя пользователя в представление
                ViewBag.Username = User.Identity.Name.Substring(
                    User.Identity.Name.IndexOf("\\") + 1
                    );
                //получаем список пользователей и их свойств
                //берем с помощью контекста 
                //список моделей для вывода на странице
                
                //Что Женя сделал с этим контекстом?(вернуться к нему позже)(этот класс создается позже)
                ViewBag.UserList = UserContext.Users();
            }
            catch (Exception eeeee)
            {
                ViewBag.Error =
                    eeeee.Message + " - " + eeeee.StackTrace;
            }
            return View();

        }

        public ActionResult FullDesk()
        {
            //получаем список пользователей и их свойств
            //берем с помощью контекста 
            //список моделей для вывода на странице
            ViewBag.UserList = UserContext.UsersDebug();
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}