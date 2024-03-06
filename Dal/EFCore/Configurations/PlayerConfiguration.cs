using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.EFCore.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(f => f.Team)
            .WithMany(t => t.Footballers)
            .OnDelete(DeleteBehavior.SetNull);
    }
}