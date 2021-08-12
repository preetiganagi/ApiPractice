using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiPractice.Interfaces;
using ApiPractice.Models;
using ApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiPractice.Controllers
{
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserContext _context;

        // seeing differnce in transient and scoped
        //private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;



        public UserController(ILogger<UserController> logger, UserContext context,

            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation
            )
        {
            _logger = logger;
            _context = context;

            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;


        }

        [HttpGet]
        [Route("api/users")]
        public List<User> GetAllUsers() => _context.GetUsers();

        [HttpPost]
        [Route("api/adduser")]
        [AllowAnonymous]
        public Boolean AddUser([FromBody] User user)
        {
            _logger.LogInformation("Add User for UserId: {id}", user.Id);
            _context.AddUser(user);
            return true;
        }

        [HttpDelete]
        [Route("api/removeuser/{id:long}")]
        [AllowAnonymous]
        public Boolean DeleteUser([FromRoute] long id)
        {
            _logger.LogInformation("Delete User for UserId:", id);
            _context.DeleteUser(id);
            return true;
        }

        [HttpPut]
        [Route("api/updateuser")]
        [AllowAnonymous]
        public Boolean UpdateUser([FromBody] User user)
        {
            _logger.LogInformation("update User for UserId:");
            _context.UpdateUser(user);
            return true;
        }


        [HttpGet]
        [Route("api/variables")]
        public IDictionary<string, string> GetVariables()
        {
            IDictionary<string, string> Variables = new Dictionary<string, string>();
            Variables.Add(new KeyValuePair<string, string>("Transient" , _transientOperation.OperationId.ToString()));
            Variables.Add(new KeyValuePair<string, string>("Scoped", _scopedOperation.OperationId.ToString()));
            Variables.Add(new KeyValuePair<string, string>("SingletonInstance", _singletonInstanceOperation.OperationId.ToString()));

            return Variables;
        }
    }
}
