using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
 
namespace WebApplication3.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext db)
        {
            db.Users.Add(new User {UsrId = 1, Username = "Danila", Email = "dan@gmail.com", Password = "4321" });
            db.Users.Add(new User {UsrId = 2, Username = "Artemka", Email = "art@mail.ru", Password = "9876" });
            base.Seed(db);
        }
    }
}