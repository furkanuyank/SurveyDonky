using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SurveyApp.Entities;

namespace SurveyApp.Contexts
{
    public class TestContext : DbContext
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var server = configuration["Database:Mssql:Server"];
            var name = configuration["Database:Mssql:Databse"];
            var security = configuration["Database:Mssql:Integrated_Security"];
            var conn=string.Concat("server=",server,";database=",name, ";Integrated Security=", security);
            optionsBuilder.UseSqlServer(conn);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("test");

            modelBuilder.Entity<TestPollster>(entity =>
            {

                entity.ToTable("pollsters");

                entity.Property(i => i.TestPollsterId).HasColumnName("pollster_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.TestId).HasColumnName("test_id").HasColumnType("int");
                entity.Property(i => i.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(40);
                entity.Property(i => i.SchoolId).HasColumnName("school_id").HasColumnType("nvarchar").HasMaxLength(25);
                entity.Property(i => i.Correct).HasColumnName("correct").HasColumnType("int");
                entity.Property(i => i.Wrong).HasColumnName("wrong").HasColumnType("int");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("tests");

                entity.Property(i => i.TestId).HasColumnName("test_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.WillPublished).HasColumnName("will_published").HasColumnType("int");
                entity.Property(i => i.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar").HasMaxLength(30);
                entity.Property(i => i.Duration).HasColumnName("duration").HasColumnType("int");
                entity.Property(i => i.CreatedDateTime).HasColumnName("created_datetime").HasColumnType("datetime");
                entity.Property(i => i.Mail).HasColumnName("mail").HasColumnType("nvarchar").HasMaxLength(55);
                entity.Property(i => i.WillSend).HasColumnName("will_send").HasColumnType("int");
                entity.Property(i => i.Description).HasColumnName("description").HasColumnType("nvarchar").HasMaxLength(90);

                entity.HasMany(i => i.TestQuestions)
                    .WithOne(i => i.Test)
                    .HasForeignKey(i => i.TestId)
                    .HasConstraintName("test_question_id_fk");

                entity.HasMany(i => i.TestPollsters)
                    .WithOne(i => i.Test)
                    .HasForeignKey(i => i.TestId)
                    .HasConstraintName("test_pollster_id_fk");
            });

            modelBuilder.Entity<TestQuestion>(entity =>
            {

                entity.ToTable("questions");

                entity.Property(i => i.TestQuestionId).HasColumnName("question_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.QuestionText).HasColumnName("question_text").HasColumnType("nvarchar").HasMaxLength(320);
                entity.Property(i => i.CorrectAnswer).HasColumnName("correct_answer").HasColumnType("int");
                entity.Property(i => i.TestId).HasColumnName("test_id").HasColumnType("int");

                entity.HasMany(i => i.TestAnswers)
                .WithOne(i => i.TestQuestion)
                .HasForeignKey(i => i.TestQuestionId)
                .HasConstraintName("t_question_answer_id_fk");
            });

            modelBuilder.Entity<TestAnswer>(entity =>
            {

                entity.ToTable("answers");

                entity.Property(i => i.TestAnswerId).HasColumnName("answer_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.TestQuestionId).HasColumnName("question_id").HasColumnType("int");
                entity.Property(i => i.Choice).HasColumnName("choice").HasColumnType("int");
                entity.Property(i => i.ChoiceIndex).HasColumnName("choice_index").HasColumnType("int");
                entity.Property(i => i.AnswerText).HasColumnName("answer_text").HasColumnType("nvarchar").HasMaxLength(320);
                base.OnModelCreating(modelBuilder);
            });

        }
        public DbSet<TestPollster> TestPollsters { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestAnswer> TestAnswers { get; set; }
    }
}
