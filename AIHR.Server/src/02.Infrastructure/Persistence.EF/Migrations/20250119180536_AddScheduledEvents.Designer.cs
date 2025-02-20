﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AIHR.EventScheduler.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIHR.EventScheduler.Persistence.EF.Migrations
{
    [DbContext(typeof(EfDataContext))]
    [Migration("20250119180536_AddScheduledEvents")]
    partial class AddScheduledEvents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("AIHR.EventScheduler.Domain.Entities.ScheduledEvents.ScheduledEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("DateRange", "AIHR.EventScheduler.Domain.Entities.ScheduledEvents.ScheduledEvent.DateRange#DateRange", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("End")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.ToTable("ScheduledEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
