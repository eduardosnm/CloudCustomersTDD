using CloudCustomersTDD.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomersTDD.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _usersService.GetAllUsers();
        if (!users.Any())
        {
            return NotFound();
        }
        return Ok(users);
    }
}
