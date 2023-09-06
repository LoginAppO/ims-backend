using System.Reflection;

namespace Ims.Infrastructure;

public class ImsDbContext : DbContext
{
    public ImsDbContext(DbContextOptions<ImsDbContext> context)
        : base(context)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Login> Logins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        ConfigureEntityTypes(modelbuilder);

        base.OnModelCreating(modelbuilder);
    }

    private static void ConfigureEntityTypes(ModelBuilder modelbuilder)
    {
        var typesToConfigure = Assembly.GetAssembly(typeof(ImsDbContext))?.GetTypes()
            .Where(type => type.GetInterfaces().Any(iface => iface.IsGenericType &&
                iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        if (typesToConfigure is not null)
        {
            foreach (var type in typesToConfigure)
            {
                dynamic configurationInstance = Activator.CreateInstance(type)!;
                modelbuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
