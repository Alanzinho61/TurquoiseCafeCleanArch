using System.ComponentModel.Design;
using System.Data.Common;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurqoiseEatary.Domain.Common.Menu.ValueObjects;
using TurqoiseEatary.Domain.Common.ValueObjects;
using TurqoiseEatary.Domain.Host;
using TurqoiseEatary.Domain.Menu;
using TurqoiseEatary.Domain.Menu.Entities;

namespace TurqoiseEatary.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);
    }

    private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, rib =>
        {
            rib.ToTable("MenuReviewIds");
            rib.WithOwner().HasForeignKey("MenuId");
            rib.HasKey("Id");
            rib.Property(r => r.Value)
            .HasColumnName("ReviewId")
            .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dib =>
        {
            dib.ToTable("MenuDinnerIds");
            dib.WithOwner().HasForeignKey("MenuId");
            dib.HasKey("Id");
            dib.Property(d => d.Value)
            .HasColumnName("DinnerId")
            .ValueGeneratedNever();

        });
        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");
            sb.WithOwner().HasForeignKey("MenuId");
            sb.HasKey("Id", "MenuId");
            //sb.HasKey(s => s.Id);
            sb.Property(s => s.Id)
            .HasColumnName("MenuSectionId")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuSectionId.Create(value));
            sb.Property(s => s.Name)
            .HasMaxLength(100);
            sb.Property(s => s.Description)
            .HasMaxLength(200);
            sb.OwnsMany(s => s.Items, ib =>
            {
                ib.ToTable("MenuItems");
                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");
                ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
                //yukaridaki iki siraya daha sonra bak
                ib.Property(i => i.Id)
                .HasColumnName("MenuItemId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuItemId.Create(value));
                ib.Property(s => s.Name)
                .HasMaxLength(100);
                ib.Property(s => s.Description)
                .HasMaxLength(200);
            });

            sb.Navigation(s => s.Items).Metadata.SetField("_items");
            sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        });


        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
        .ValueGeneratedNever()
        .HasConversion(
            id => id.Value,
            value => MenuId.Create(value));
        builder.Property(m => m.Name)
        .HasMaxLength(100);
        builder.Property(m => m.Description)
        .HasMaxLength(100);

        builder.OwnsOne(m => m.AverageRating);

        builder.Property(m => m.HostId)
        .HasConversion(
            id => id.Value,
            value => HostId.Create(value));

    }
}