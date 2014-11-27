﻿using System;
using System.Data.Entity.ModelConfiguration;

namespace BankingManagementClient.ProjectionStore.EntityFramework
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
            Property(p => p.VersionNumber)
                .HasColumnOrder(++columnOrder)
                .IsRequired();
        }
    }
}