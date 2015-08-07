﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NoteeFY.Data.Models;
using NoteeFY.Buisness.Managers;

namespace NoteeFY.Controllers
{
    public class UserControllers : ApiController
    {
        private UserManagers userManager = new UserManagers();

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return userManager.GetSetOfUsers();
        }

        // GET api/User/5
        public User GetUser(int id)
        {
            try
            {
                return userManager.GetSingleUser(id);
            }
            catch (ObjectNotFoundException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }     
        } 
    }
}