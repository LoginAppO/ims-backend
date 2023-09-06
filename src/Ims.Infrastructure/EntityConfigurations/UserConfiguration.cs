namespace Ims.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.Property(x => x.Email).IsRequired();
        _ = builder.Property(x => x.Email).IsRequired();
    }
}
