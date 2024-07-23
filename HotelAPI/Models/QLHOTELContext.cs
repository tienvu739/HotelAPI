using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelAPI.Models
{
    public partial class QLHOTELContext : DbContext
    {
        public QLHOTELContext()
        {
        }

        public QLHOTELContext(DbContextOptions<QLHOTELContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Convenient> Convenients { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Evaluate> Evaluates { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Hotelconvenient> Hotelconvenients { get; set; } = null!;
        public virtual DbSet<Hotelier> Hoteliers { get; set; } = null!;
        public virtual DbSet<Hotelservice> Hotelservices { get; set; } = null!;
        public virtual DbSet<Imagehotel> Imagehotels { get; set; } = null!;
        public virtual DbSet<Imageroom> Imagerooms { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-A2U8DB33;Initial Catalog=QLHOTEL;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PK__ADMIN__4C3F97F498FF12A8");

                entity.ToTable("ADMIN");

                entity.Property(e => e.IdAdmin).HasMaxLength(256);

                entity.Property(e => e.Account).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.Role).HasMaxLength(256);
            });
            modelBuilder.Entity<Convenient>(entity =>
            {
                entity.HasKey(e => e.IdConvenient)
                    .HasName("PK__CONVENIE__A99690163C6342E7");

                entity.ToTable("CONVENIENTS");

                entity.Property(e => e.IdConvenient).HasMaxLength(64);

                entity.Property(e => e.NameConvenient).HasMaxLength(64);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount)
                    .HasName("PK__DISCOUNT__C6A0EA328D9ED5AE");

                entity.ToTable("DISCOUNTS");

                entity.Property(e => e.IdDiscount).HasMaxLength(64);

                entity.Property(e => e.DescribeDiscount).HasMaxLength(256);

                entity.Property(e => e.NameDiscount).HasMaxLength(256);
            });

            modelBuilder.Entity<Evaluate>(entity =>
            {
                entity.HasKey(e => e.IdEvaluate)
                    .HasName("PK__EVALUATE__F47055117E79B9F8");

                entity.ToTable("EVALUATE");

                entity.Property(e => e.IdEvaluate).HasMaxLength(256);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.DescribeEvaluate).HasMaxLength(256);

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.Property(e => e.IdUser).HasMaxLength(64);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("fk_evaluate_hotel");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_evaluate_user");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.IdHotel)
                    .HasName("PK__HOTELS__653BCCC4DA848BAD");

                entity.ToTable("HOTELS");

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.Property(e => e.AddressHotel).HasMaxLength(256);

                entity.Property(e => e.DescribeHotel).HasMaxLength(256);

                entity.Property(e => e.IdHotelier).HasMaxLength(64);

                entity.Property(e => e.NameHotel).HasMaxLength(256);

                entity.Property(e => e.PolicyHotel).HasMaxLength(256);

                entity.Property(e => e.TypeHotel).HasMaxLength(256);

                entity.HasOne(d => d.IdHotelierNavigation)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.IdHotelier)
                    .HasConstraintName("fk_hotel_Hotelier");
            });

            modelBuilder.Entity<Hotelconvenient>(entity =>
            {
                entity.HasKey(e => e.IdHotelConvenient)
                    .HasName("PK__HOTELCON__C31BC0C730B4233B");

                entity.ToTable("HOTELCONVENIENTS");

                entity.Property(e => e.IdHotelConvenient)
                    .HasMaxLength(64)
                    .HasColumnName("idHotelConvenient");

                entity.Property(e => e.IdConvenient).HasMaxLength(64);

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.HasOne(d => d.IdConvenientNavigation)
                    .WithMany(p => p.Hotelconvenients)
                    .HasForeignKey(d => d.IdConvenient)
                    .HasConstraintName("FK__HOTELCONV__IdCon__4F7CD00D");

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Hotelconvenients)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__HOTELCONV__IdHot__5070F446");
            });

            modelBuilder.Entity<Hotelier>(entity =>
            {
                entity.HasKey(e => e.IdHotelier)
                    .HasName("PK__HOTELIER__8DA6E50ADDDE08CF");

                entity.ToTable("HOTELIER");

                entity.Property(e => e.IdHotelier).HasMaxLength(64);

                entity.Property(e => e.Account).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NameHotelier).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Hotelservice>(entity =>
            {
                entity.HasKey(e => e.IdHotelServer)
                    .HasName("PK__HOTELSER__6FE866445F37666E");

                entity.ToTable("HOTELSERVICES");

                entity.Property(e => e.IdHotelServer)
                    .HasMaxLength(64)
                    .HasColumnName("idHotelServer");

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.Property(e => e.IdService)
                    .HasMaxLength(64)
                    .HasColumnName("idService");

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Hotelservices)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__HOTELSERV__IdHot__5441852A");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.Hotelservices)
                    .HasForeignKey(d => d.IdService)
                    .HasConstraintName("FK__HOTELSERV__idSer__534D60F1");
            });

            modelBuilder.Entity<Imagehotel>(entity =>
            {
                entity.HasKey(e => e.IdImageHotel)
                    .HasName("PK__IMAGEHOT__E3EF21A74BC09478");

                entity.ToTable("IMAGEHOTEL");

                entity.Property(e => e.IdImageHotel).HasMaxLength(64);

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.Property(e => e.ImageData).HasMaxLength(1);

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Imagehotels)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("FK__IMAGEHOTE__IdHot__571DF1D5");
            });

            modelBuilder.Entity<Imageroom>(entity =>
            {
                entity.HasKey(e => e.IdImageRoom)
                    .HasName("PK__IMAGEROO__C84FE5559F831A08");

                entity.ToTable("IMAGEROOM");

                entity.Property(e => e.IdImageRoom).HasMaxLength(64);

                entity.Property(e => e.IdRoom).HasMaxLength(64);

                entity.Property(e => e.ImageData).HasMaxLength(1);

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Imagerooms)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK__IMAGEROOM__IdRoo__59FA5E80");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__ORDERS__C38F3009C8345AEE");

                entity.ToTable("ORDERS");

                entity.Property(e => e.IdOrder).HasMaxLength(64);

                entity.Property(e => e.CheckInDate).HasColumnType("date");

                entity.Property(e => e.CheckOutDate).HasColumnType("date");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.IdDiscount).HasMaxLength(64);

                entity.Property(e => e.IdRoom).HasMaxLength(64);

                entity.Property(e => e.IdUser).HasMaxLength(64);

                entity.HasOne(d => d.IdDiscountNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdDiscount)
                    .HasConstraintName("fk_order_discount");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("fk_order_room");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_order_user");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PK__ROOMS__B436880FE1E78D56");

                entity.ToTable("ROOMS");

                entity.Property(e => e.IdRoom).HasMaxLength(64);

                entity.Property(e => e.IdHotel).HasMaxLength(64);

                entity.Property(e => e.NameRoom).HasMaxLength(256);

                entity.Property(e => e.PolicyRoom).HasMaxLength(256);

                entity.Property(e => e.TypeRoom).HasMaxLength(256);

                entity.HasOne(d => d.IdHotelNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdHotel)
                    .HasConstraintName("fk_room_hotel");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService)
                    .HasName("PK__SERVICES__0E3EA45BE435C02A");

                entity.ToTable("SERVICES");

                entity.Property(e => e.IdService)
                    .HasMaxLength(64)
                    .HasColumnName("idService");

                entity.Property(e => e.NameService).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USERS__B7C92638D0204BED");

                entity.ToTable("USERS");

                entity.Property(e => e.IdUser).HasMaxLength(64);

                entity.Property(e => e.Account).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NameUser).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(128);

                entity.Property(e => e.PhoneNumber).HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
