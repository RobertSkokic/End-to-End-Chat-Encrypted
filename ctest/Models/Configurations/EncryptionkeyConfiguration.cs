﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ctest.Models;
using ctest.Models.dboSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ctest.Models.Configurations
{
    public partial class EncryptionkeyConfiguration : IEntityTypeConfiguration<Encryptionkey>
    {
        public void Configure(EntityTypeBuilder<Encryptionkey> entity)
        {
            entity.ToTable("ENCRYPTIONKEY", tb =>
                {
                    tb.HasTrigger("TRG_BI_ENCRYPTIONKEY");
                    tb.HasTrigger("TRG_BU_ENCRYPTIONKEY");
                });

            entity.Property(e => e.Valid).HasDefaultValue((short)1);

            entity.HasOne(d => d.Chatuser).WithMany(p => p.Encryptionkey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCRYPTIONKEY_CHATUSER");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Encryptionkey> entity);
    }
}
