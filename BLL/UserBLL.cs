using SalesSystem.DAL;
using SalesSystem.Entities;
using System;

namespace SalesSystem.BLL
{
    public class UserBLL
    {
        private readonly UserDAL userDAL;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        public User Login(string email, string password)
        {
            var user = userDAL.Login(email, password);

            if (user == null)
            {
                throw new Exception("Credenciales inválidas o rol no autorizado");
            }

            return user;
        }
    }
}
