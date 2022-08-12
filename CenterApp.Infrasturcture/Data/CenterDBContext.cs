using System.Xml.Schema;

using CenterApp.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace CenterApp.Infrasturcture.Data;

public class CenterDBContext : DbContext
{
    public CenterDBContext()
    {

    }

    public CenterDBContext(DbContextOptions<CenterDBContext> options) : base()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=.;DataBase=CenterITIDB;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Level>().HasKey(x => x.Level_Id);
        modelBuilder.Entity<Stage>().HasKey(x => x.Stage_Id);
        modelBuilder.Entity<Matrial>().HasKey(x => x.Matrial_Id);
        modelBuilder.Entity<Stage>().HasOne<Level>(x => x.Level).WithMany(x => x.Stages).HasForeignKey(x => x.Level_Id);
        modelBuilder.Entity<Matrial>().Property(x => x.Matrial_Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Stage>().Property(x => x.Stage_Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Level>().Property(x => x.Level_Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<LevelMatrial>().HasKey(x => new { x.Level_Id, x.Matrial_Id });
        modelBuilder.Entity<LevelMatrial>().HasOne<Level>(x => x.Level).WithMany(x => x.LevelMatrial).HasForeignKey(x => x.Level_Id);
        modelBuilder.Entity<LevelMatrial>().HasOne<Matrial>(x => x.Matrial).WithMany(x => x.LevelMatrial).HasForeignKey(x => x.Matrial_Id);
        modelBuilder.Entity<Teacher>().HasKey(x => x.Teacher_Id);
        modelBuilder.Entity<Teacher>().Property(x => x.Teacher_Email).IsRequired();
        modelBuilder.Entity<Teacher>().Property(x => x.Teacher_Name).IsRequired();
        modelBuilder.Entity<Teacher>().Property(x => x.Teacher_Image).IsRequired();
        modelBuilder.Entity<Teacher>().Property(x => x.Teacher_Phone).IsRequired();
        modelBuilder.Entity<Teacher>().Property(x => x.Teacher_BirthOfDate).IsRequired();
        modelBuilder.Entity<TeacherMatrial>().HasKey(x => new { x.Matrial_Id, x.Teacher_Id });
        modelBuilder.Entity<TeacherMatrial>().HasOne<Teacher>(x => x.Teacher).WithMany(x => x.TeacherMatrial)
            .HasForeignKey(x => x.Teacher_Id);
        modelBuilder.Entity<TeacherMatrial>().HasOne<Matrial>(x => x.Matrial).WithMany(x => x.TeacherMatrial)
            .HasForeignKey(x => x.Matrial_Id);
        modelBuilder.Entity<TeacherMatrial>().HasOne<Stage>(x => x.Stage).WithMany(x => x.TeacherMatrial)
            .HasForeignKey(x => x.Stage_Id);
        modelBuilder.Entity<Group>().HasKey(x => x.Group_Id);
        modelBuilder.Entity<Group>().Property(x => x.Group_Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Group>().HasOne<Teacher>(x => x.Teacher).WithMany(x => x.Groups)
            .HasForeignKey(x => x.Teacher_Id);
        modelBuilder.Entity<Student>().HasKey(x => x.Student_Id);
        modelBuilder.Entity<Student>().Property(x => x.Student_Name).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Student>().Property(x => x.Student_Address).IsRequired().HasMaxLength(30);
        modelBuilder.Entity<Student>().Property(x => x.Student_Email).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Student>().Property(x => x.Student_StdPhone).IsRequired();
        modelBuilder.Entity<Student>().Property(x => x.Gender).IsRequired();
        modelBuilder.Entity<Student>().Property(x => x.Student_BirthOfDate).IsRequired();
        modelBuilder.Entity<Student>().Property(x => x.Student_ParentPhone).IsRequired();
        modelBuilder.Entity<Student>().Property(x => x.Student_RegisterDate).IsRequired().HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<StudentGroup>().HasKey(x => new { x.Student_Id, x.Group_Id });
        modelBuilder.Entity<StudentGroup>().HasOne<Student>(x => x.Student).WithMany(x => x.StudentGroup).HasForeignKey(x => x.Student_Id);
        modelBuilder.Entity<StudentGroup>().HasOne<Group>(x => x.Group).WithMany(x => x.StudentGroup).HasForeignKey(x => x.Group_Id);
        modelBuilder.Entity<Student>().HasOne<Stage>(x => x.Stage).WithMany(x => x.Students)
            .HasForeignKey(x => x.Stage_id);
        modelBuilder.Entity<StudentPayments>().HasKey(d => d.Id);
        modelBuilder.Entity<StudentPayments>().HasOne(s => s.Student).WithMany(d => d.StudentPayments).HasForeignKey(d => d.Student_Id);
        modelBuilder.Entity<StudentPayments>().HasOne(s => s.Matrial).WithMany(d => d.StudentPayments).HasForeignKey(d => d.Matrial_Id);

    }

    public DbSet<Level> Levels { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Matrial> Matrials { get; set; }
    public DbSet<LevelMatrial> LevelMatrial { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<TeacherMatrial> TeacherMatrial { get; set; }
    public DbSet<Group> Group { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroup> StudentGroup { get; set; }
    public DbSet<StudentPayments> StudentPayments { get; set; }
}