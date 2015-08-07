﻿using NoteeFY.Buisness.DTOs;
using NoteeFY.Buisness.Managers;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace NoteeFY.Controllers
{
    public class UsersController : ApiController
    {
        private UserManagers userManager = new UserManagers();

        // GET: api/Users
        public List<UserDTO> GetTaskItems()
        {
            return userManager.GetSetOfUsers();
        }

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
