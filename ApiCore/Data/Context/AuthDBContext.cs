
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ApiCore.Context
{
    public class AuthDBContext : IdentityDbContext
    {

        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {}

    }
}
