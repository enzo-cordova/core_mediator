using Genzai.EfCore.Context;
using Genzai.Security.Context.Mapping;
using Genzai.Security.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Genzai.Security.Context
{
    /// <summary>
    /// Authorization context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AuthorizationContext : ContextDataBase<AuthorizationContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationContext"/> class.
        /// </summary>
        /// <param name="options">Db context options</param>
        /// <param name="mediator">Mediator service</param>
        /// <param name="claimsPrincipal">Current principal.</param>
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options, IMediator mediator, ClaimsPrincipal claimsPrincipal)
            : base(options, mediator, claimsPrincipal)
        {
        }

        /// <summary>
        /// Permissions table
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Permissions table
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public virtual DbSet<Center> Centers { get; set; }


        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new CenterMap());
        }
    }
}