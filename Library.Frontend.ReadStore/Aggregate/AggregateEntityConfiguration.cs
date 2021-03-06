﻿using System.Data.Entity.ModelConfiguration;

namespace Library.Frontend.ReadStore.Aggregate
{
    public class AggregateEntityConfiguration : EntityTypeConfiguration<AggregateEntity>
    {
        public AggregateEntityConfiguration()
        {
            ToTable("Aggregate", "Aggregate");

            HasKey(x => x.AggregateId);

            var columnOrder = 0;

            Property(p => p.AggregateId)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
            Property(p => p.AggregateVersionNumber)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}