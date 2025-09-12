using SalesSystem.DAL;
using SalesSystem.DTOs;
using SalesSystem.DTOs.User;
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

        public AuthenticatedUserDTO Login(UserLoginDTO loginDTO)
        {
            var user = userDAL.Login(loginDTO);
            AuthenticatedUserDTO authenticatedUserDTO = null;

            if (user == null)
            {
                throw new Exception("Credenciales inválidas o rol no autorizado");
            }
            else 
            { 
                authenticatedUserDTO = new AuthenticatedUserDTO {
                    UserID = user.UserID,
                    FullName = user.FullName,
                    Role = user.Role,
                };
            }

            return authenticatedUserDTO;
        }
    }
}
