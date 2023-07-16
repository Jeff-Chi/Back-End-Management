﻿// <auto-generated />
using System;
using Managemrnt.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Managemrnt.EFCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Management.Domain.Permission", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ParentCode")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Code = "BackEndManagement.UserManagement",
                            Name = "用户管理",
                            SortOrder = 1
                        },
                        new
                        {
                            Code = "BackEndManagement.UserManagement.Query",
                            Name = "查询",
                            ParentCode = "BackEndManagement.UserManagement",
                            SortOrder = 2
                        },
                        new
                        {
                            Code = "BackEndManagement.UserManagement.Create",
                            Name = "创建",
                            ParentCode = "BackEndManagement.UserManagement",
                            SortOrder = 3
                        },
                        new
                        {
                            Code = "BackEndManagement.UserManagement.Update",
                            Name = "更新",
                            ParentCode = "BackEndManagement.UserManagement",
                            SortOrder = 4
                        },
                        new
                        {
                            Code = "BackEndManagement.UserManagement.Delete",
                            Name = "删除",
                            ParentCode = "BackEndManagement.UserManagement",
                            SortOrder = 5
                        },
                        new
                        {
                            Code = "BackEndManagement.UserManagement.ChangeUserRole",
                            Name = "设置用户角色",
                            ParentCode = "BackEndManagement.UserManagement",
                            SortOrder = 6
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement",
                            Name = "角色管理",
                            SortOrder = 7
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement.Query",
                            Name = "查询",
                            ParentCode = "BackEndManagement.RoleManagement",
                            SortOrder = 8
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement.Create",
                            Name = "创建",
                            ParentCode = "BackEndManagement.RoleManagement",
                            SortOrder = 9
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement.Update",
                            Name = "更新",
                            ParentCode = "BackEndManagement.RoleManagement",
                            SortOrder = 10
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement.Delete",
                            Name = "删除",
                            ParentCode = "BackEndManagement.RoleManagement",
                            SortOrder = 11
                        },
                        new
                        {
                            Code = "BackEndManagement.RoleManagement.ChangeRolePermission",
                            Name = "设置角色权限",
                            ParentCode = "BackEndManagement.RoleManagement",
                            SortOrder = 12
                        });
                });

            modelBuilder.Entity("Management.Domain.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Management.Domain.RolePermission", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("PermissionCode")
                        .HasColumnType("varchar(100)");

                    b.HasKey("RoleId", "PermissionCode");

                    b.HasIndex("PermissionCode");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Management.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Avatars")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Management.Domain.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Management.Domain.RolePermission", b =>
                {
                    b.HasOne("Management.Domain.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Management.Domain.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Management.Domain.UserRole", b =>
                {
                    b.HasOne("Management.Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Management.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Management.Domain.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Management.Domain.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
