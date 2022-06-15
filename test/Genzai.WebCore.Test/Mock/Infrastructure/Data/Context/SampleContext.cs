using Genzai.EfCore.Context;
using Genzai.WebCore.Test.Mock.Infrastructure.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using System.Security.Claims;

namespace Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;

/// <summary>
/// Sample context.
/// </summary>
public class SampleContext : CoreContextDataBase<SampleContext>
{
    public SampleContext(DbContextOptions<SampleContext> options, IMediator mediator, ClaimsPrincipal claimsPrincipal) : base(options, mediator, claimsPrincipal)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        if (_provider != null)
        {
            modelBuilder.UseEncryption(_provider);
        }
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(SampleEntityConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
