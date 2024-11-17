using GAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace GAPI.Controllers;

public class Controller : ControllerBase
{
    private UserContext _context;

    public Controller(UserContext context)
    {
        _context = context;
    }
}