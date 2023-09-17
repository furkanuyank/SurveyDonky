using Microsoft.EntityFrameworkCore;
using SurveyApp.Entities;
using Microsoft.Extensions.Configuration;

namespace SurveyApp.Contexts
{
    public class SurveyContext : DbContext
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var server = configuration["Database:Mssql:Server"];
            var name = configuration["Database:Mssql:Databse"];
            var security = configuration["Database:Mssql:Integrated_Security"];
            var conn = string.Concat("server=", server, ";database=", name, ";Integrated Security=", security);
            optionsBuilder.UseSqlServer(conn);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("survey");


            modelBuilder.Entity<SurveyCategory>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(i => i.SurveyCategoryId).HasColumnName("category_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.CategoryName).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(25);

                entity.HasMany(i => i.Surveys)
                   .WithOne(i => i.SurveyCategory)
                   .HasForeignKey(i => i.SurveyCategoryId)
                   .HasConstraintName("category_survey_id_fk");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("surveys");

                entity.Property(i => i.SurveyId).HasColumnName("survey_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(40);
                entity.Property(i => i.WillPublished).HasColumnName("will_published").HasColumnType("int");
                entity.Property(i => i.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar").HasMaxLength(30);
                entity.Property(i => i.SurveyCategoryId).HasColumnName("category_id").HasColumnType("int");
                entity.Property(i => i.CreatedDate).HasColumnName("created_date").HasColumnType("datetime");
                entity.Property(i => i.Description).HasColumnName("description").HasColumnType("nvarchar").HasMaxLength(90);

                entity.HasMany(i => i.SurveyQuestions)
                    .WithOne(i => i.Survey)
                    .HasForeignKey(i => i.SurveyId)
                    .HasConstraintName("survey_question_id_fk");

                entity.HasMany(i => i.SurveyPollsters)
                   .WithOne(i => i.Survey)
                   .HasForeignKey(i => i.SurveyId)
                   .HasConstraintName("survey_pollster_id_fk");

            });
            modelBuilder.Entity<SurveyPollster>(entity =>
            {
                entity.ToTable("pollster");

                entity.Property(i => i.SurveyPollsterId).HasColumnName("id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.Ip).HasColumnName("ip").HasColumnType("nvarchar").HasMaxLength(20);
                entity.Property(i => i.SurveyId).HasColumnName("survey_id").HasColumnType("int");
            });


            modelBuilder.Entity<SurveyQuestion>(entity =>
            {

                entity.ToTable("questions");

                entity.Property(i => i.SurveyQuestionId).HasColumnName("question_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.QuestionText).HasColumnName("question_text").HasColumnType("nvarchar").HasMaxLength(320);
                entity.Property(i => i.SurveyId).HasColumnName("survey_id").HasColumnType("int");

                entity.HasMany(i => i.SurveyAnswers)
                .WithOne(i => i.SurveyQuestion)
                .HasForeignKey(i => i.QuestionId)
                .HasConstraintName("s_question_answer_id_fk");
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {

                entity.ToTable("answers");

                entity.Property(i => i.SurveyAnswerId).HasColumnName("answer_id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.QuestionId).HasColumnName("question_id").HasColumnType("int");
                entity.Property(i => i.Choice).HasColumnName("choice").HasColumnType("int");
                entity.Property(i => i.ChoiceIndex).HasColumnName("choice_index").HasColumnType("int");
                entity.Property(i => i.AnswerText).HasColumnName("answer_text").HasColumnType("nvarchar").HasMaxLength(320);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SurveyCategory> SurveyCategories { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyPollster> SurveyPollsters { get; set; }
    }
}
