using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogsDAL;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<IdentityUser>(options);