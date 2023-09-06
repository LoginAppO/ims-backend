namespace Ims.Infrastructure.EntityConfigurations;

public class LoginConfiguration : IEntityTypeConfiguration<Login>
{
    public void Configure(EntityTypeBuilder<Login> builder)
    {
        _ = builder.Property(x => x.Email).IsRequired();
        _ = builder.Property(x => x.LoggedAt).IsRequired();
        _ = builder.Property(x => x.Status).IsRequired();
    }
}
