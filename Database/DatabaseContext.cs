using Microsoft.EntityFrameworkCore;
using GAPI.Models;
namespace GAPI.Database;


public class UserContext: DbContext
{
    public required DbSet<User> Users { get; set; }
}