using Chamtsere.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chamtsere.Infrastructure.Data.Configurations.TokenConfigurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.RefreshToken).IsRequired();
        builder.Property(b => b.ExpiryDate).IsRequired();
        builder.Property(b => b.IsRevoked).IsRequired();

        builder.HasIndex(b => b.UserId);
    }
}
