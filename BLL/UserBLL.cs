using SalesSystem.DAL;
using SalesSystem.Entities;
using System;
using System.Collections.Generic;

namespace SalesSystem.BLL
{
    public class UserBLL
    {
        private readonly UserDAL userDAL;
        private readonly UserBLL userBLL;

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

        public List<User> GetAll()
        {
            return userDAL.GetAll();
        }

        public int DeleteUser(int userId)
        {
            int filasAfectadas = userDAL.Delete(userId);
            return filasAfectadas;
        }

        public void Add(User user)
        {
            userDAL.Add(user);
        }

        public User GetById(int id)
        {
            return userDAL.GetById(id);
        }

        public void UpdateUser(User user)
        {
            userDAL.Update(user);
        }




    }
}
