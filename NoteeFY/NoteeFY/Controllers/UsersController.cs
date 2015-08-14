﻿using NoteeFY.Buisness.DTOs;
using NoteeFY.Buisness.Managers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using NoteeFY.Buisness.Helpers;

namespace NoteeFY.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private UsersManager userManager = new UsersManager();

        // GET api/Users/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUser(string id)
        {
            var user = userManager.GetUser(id);
            return user == null ? (IHttpActionResult) NotFound() : Ok(user);
        }

        // POST: api/Users - ADD or UPDATE
        public JsonResult<ModificationResult<UserDTO>> PostUser(UserDTO user)
        {
            var result = ValidateModelState<UserDTO>();
            if (result != null)
            {
                return result;
            }
            else
            {
                userManager.AddOrUpdateUser(user);
                return Json(new ModificationResult<UserDTO>(true)
                {
                    Data = user
                });
            }
        }
    }
}
