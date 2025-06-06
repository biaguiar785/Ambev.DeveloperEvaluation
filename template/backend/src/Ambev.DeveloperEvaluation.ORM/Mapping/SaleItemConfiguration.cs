﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration: IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.SaleId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(si => si.ProductId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.Price)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(si => si.Discount)
                .HasColumnType("numeric(5,2)")
                .IsRequired();

            builder.Property(si => si.TotalItemPrice)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            builder.Property(si => si.Cancelled)
                .IsRequired();
        }
    }
}
