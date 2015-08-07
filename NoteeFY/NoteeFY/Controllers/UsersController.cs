﻿using NoteeFY.Buisness.DTOs;
using NoteeFY.Buisness.Managers;
using System.Web.Http;
using System.Web.Http.Description;

namespace NoteeFY.Controllers
{
    public class UsersController : ApiController
    {
        private UserManagers userManager = new UserManagers();

        // GET api/User/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUser(int id)
        {
            UserDTO user = userManager.GetSingleUser(id);
            if(user == null) return NotFound();
            else return Ok(user);
        } 
    }
}