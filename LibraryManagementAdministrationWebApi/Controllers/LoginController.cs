using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementAdministrationWebApi.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAdministrationWebApi.Helpers;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using LibraryManagementAdministrationWebApi.Helpers.LibraryManagement.Helpers;
using System.IO;

namespace LibraryManagementAdministrationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LibraryManagementContext _context;
        private IConfiguration _config;

        public LoginController(LibraryManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCredential c)
        {
            string token = null;

            /*
            using (var reader = new StreamReader(Request.Body))
            {
                try
                {
                    var body = await reader.ReadToEndAsync(); // reader.ReadToEnd();
                }
                catch (Exception e)
                { 
                }

                // Do something else
            }
            */
            if (c.Uname != null && c.Upassword != null)
            {
                var admin = await _context.Administrator.FirstOrDefaultAsync(
                    a => a.Uname == c.Uname && a.Upassword == c.Upassword);

                if (admin != null)
                {
                    var adminRole = await _context.AdminRole.FirstOrDefaultAsync(a=> a.AdminLevel.ToString() == admin.AdminLevel.ToString());
                    token = JwtHelper.GenrateJwtTokenForLibraryAdmin(admin.AdminId.ToString(),System.Convert.ToString(adminRole), _config["Jwt:Key"]);
                    return Ok(new { token });
                }
            }

            return BadRequest(new { message = "Username or password is incorrect." });
        }

        public string Test()
        {
            return "Hello";
        }

    }
}