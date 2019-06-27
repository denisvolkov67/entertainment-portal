﻿using EP.TicTacToe.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EP.TicTacToe.Data.Context.Configuration
{
    public class MapConfiguration : IEntityTypeConfiguration<MapDb>
    {
        public void Configure(EntityTypeBuilder<MapDb> builder)
        {
            builder.ToTable("Maps");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Size).IsRequired();
            builder.HasMany(m => m.Cells).WithOne();
        }
    }
}