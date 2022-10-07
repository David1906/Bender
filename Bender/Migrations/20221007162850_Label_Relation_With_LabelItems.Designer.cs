﻿// <auto-generated />
using System;
using Bender.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bender.Migrations
{
    [DbContext(typeof(BenderContext))]
    [Migration("20221007162850_Label_Relation_With_LabelItems")]
    partial class Label_Relation_With_LabelItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("Bender.DataAccess.LabelDAO", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LabelId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("Bender.DataAccess.LabelItemDAO", b =>
                {
                    b.Property<int>("LabelItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LabelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Mode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Terminator")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LabelItemId");

                    b.HasIndex("LabelId");

                    b.ToTable("LabelItems");
                });

            modelBuilder.Entity("Bender.DataAccess.LabelItemDAO", b =>
                {
                    b.HasOne("Bender.DataAccess.LabelDAO", "Label")
                        .WithMany("Items")
                        .HasForeignKey("LabelId");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("Bender.DataAccess.LabelDAO", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
