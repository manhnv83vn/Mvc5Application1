

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "Mvc5Application1.Data.Model\App.config"
//     Connection String Name: "TestDBConnectionString"
//     Connection String:      "Data Source=HN-SD-0381-WK;Initial Catalog=Test;MultipleActiveResultSets=True;User Id=sa;password=123456;"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Mvc5Application1.Data.Model
{
    // ************************************************************************
    // Unit of work
    public interface IDBContextBase : IDisposable
    {
        IDbSet<Customers> Customers { get; set; } // Customers
        IDbSet<CustomerSeeds> CustomerSeeds { get; set; } // CustomerSeeds
        IDbSet<Employee> Employee { get; set; } // Employee
        IDbSet<Employees> Employees { get; set; } // Employees
        IDbSet<Function> Function { get; set; } // Function
        IDbSet<PaymentType> PaymentType { get; set; } // PaymentType
        IDbSet<Permissions> Permissions { get; set; } // Permissions
        IDbSet<Project> Project { get; set; } // Project
        IDbSet<ProjectPermissions> ProjectPermissions { get; set; } // ProjectPermissions
        IDbSet<ProjectRole> ProjectRole { get; set; } // ProjectRole
        IDbSet<Settings> Settings { get; set; } // Settings
        IDbSet<SpecicalPermission> SpecicalPermission { get; set; } // SpecicalPermission
        IDbSet<umbCategories> umbCategories { get; set; } // umbCategories
        IDbSet<umbProducts> umbProducts { get; set; } // umbProducts
        IDbSet<umbReviews> umbReviews { get; set; } // umbReviews
        IDbSet<UserProfile> UserProfile { get; set; } // UserProfile
        IDbSet<vwEmployee> vwEmployee { get; set; } // vwEmployee
        IDbSet<vwSecutiryMatrix> vwSecutiryMatrix { get; set; } // vwSecutiryMatrix
        IDbSet<vwUserProfile> vwUserProfile { get; set; } // vwUserProfile
        IDbSet<vwUserRoles> vwUserRoles { get; set; } // vwUserRoles

        int SaveChanges();
    }

    // ************************************************************************
    // Database context
    public partial class DBContextBase : DbContext, IDBContextBase
    {
        public IDbSet<Customers> Customers { get; set; } // Customers
        public IDbSet<CustomerSeeds> CustomerSeeds { get; set; } // CustomerSeeds
        public IDbSet<Employee> Employee { get; set; } // Employee
        public IDbSet<Employees> Employees { get; set; } // Employees
        public IDbSet<Function> Function { get; set; } // Function
        public IDbSet<PaymentType> PaymentType { get; set; } // PaymentType
        public IDbSet<Permissions> Permissions { get; set; } // Permissions
        public IDbSet<Project> Project { get; set; } // Project
        public IDbSet<ProjectPermissions> ProjectPermissions { get; set; } // ProjectPermissions
        public IDbSet<ProjectRole> ProjectRole { get; set; } // ProjectRole
        public IDbSet<Settings> Settings { get; set; } // Settings
        public IDbSet<SpecicalPermission> SpecicalPermission { get; set; } // SpecicalPermission
        public IDbSet<umbCategories> umbCategories { get; set; } // umbCategories
        public IDbSet<umbProducts> umbProducts { get; set; } // umbProducts
        public IDbSet<umbReviews> umbReviews { get; set; } // umbReviews
        public IDbSet<UserProfile> UserProfile { get; set; } // UserProfile
        public IDbSet<vwEmployee> vwEmployee { get; set; } // vwEmployee
        public IDbSet<vwSecutiryMatrix> vwSecutiryMatrix { get; set; } // vwSecutiryMatrix
        public IDbSet<vwUserProfile> vwUserProfile { get; set; } // vwUserProfile
        public IDbSet<vwUserRoles> vwUserRoles { get; set; } // vwUserRoles

        static DBContextBase()
        {
            Database.SetInitializer<DBContextBase>(null);
        }

        public DBContextBase()
            : base("Name=TestDBConnectionString")
        {
        InitializePartial();
        }

        public DBContextBase(string connectionString) : base(connectionString)
        {
        InitializePartial();
        }

        public DBContextBase(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CustomersConfiguration());
            modelBuilder.Configurations.Add(new CustomerSeedsConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new EmployeesConfiguration());
            modelBuilder.Configurations.Add(new FunctionConfiguration());
            modelBuilder.Configurations.Add(new PaymentTypeConfiguration());
            modelBuilder.Configurations.Add(new PermissionsConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectPermissionsConfiguration());
            modelBuilder.Configurations.Add(new ProjectRoleConfiguration());
            modelBuilder.Configurations.Add(new SettingsConfiguration());
            modelBuilder.Configurations.Add(new SpecicalPermissionConfiguration());
            modelBuilder.Configurations.Add(new umbCategoriesConfiguration());
            modelBuilder.Configurations.Add(new umbProductsConfiguration());
            modelBuilder.Configurations.Add(new umbReviewsConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new vwEmployeeConfiguration());
            modelBuilder.Configurations.Add(new vwSecutiryMatrixConfiguration());
            modelBuilder.Configurations.Add(new vwUserProfileConfiguration());
            modelBuilder.Configurations.Add(new vwUserRolesConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CustomersConfiguration(schema));
            modelBuilder.Configurations.Add(new CustomerSeedsConfiguration(schema));
            modelBuilder.Configurations.Add(new EmployeeConfiguration(schema));
            modelBuilder.Configurations.Add(new EmployeesConfiguration(schema));
            modelBuilder.Configurations.Add(new FunctionConfiguration(schema));
            modelBuilder.Configurations.Add(new PaymentTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new PermissionsConfiguration(schema));
            modelBuilder.Configurations.Add(new ProjectConfiguration(schema));
            modelBuilder.Configurations.Add(new ProjectPermissionsConfiguration(schema));
            modelBuilder.Configurations.Add(new ProjectRoleConfiguration(schema));
            modelBuilder.Configurations.Add(new SettingsConfiguration(schema));
            modelBuilder.Configurations.Add(new SpecicalPermissionConfiguration(schema));
            modelBuilder.Configurations.Add(new umbCategoriesConfiguration(schema));
            modelBuilder.Configurations.Add(new umbProductsConfiguration(schema));
            modelBuilder.Configurations.Add(new umbReviewsConfiguration(schema));
            modelBuilder.Configurations.Add(new UserProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new vwEmployeeConfiguration(schema));
            modelBuilder.Configurations.Add(new vwSecutiryMatrixConfiguration(schema));
            modelBuilder.Configurations.Add(new vwUserProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new vwUserRolesConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }

    // ************************************************************************
    // POCO classes

    // Customers
    public partial class Customers : ITrackableEntity
    {
        public int CustomerID { get; set; } // CustomerID (Primary key)
        public string FirstName { get; set; } // FirstName
        public string LastName { get; set; } // LastName
        public string Email { get; set; } // Email
        public bool Active { get; set; } // Active
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public DateTime? ModifiedDate { get; set; } // ModifiedDate
        public string CreatedBy { get; set; } // CreatedBy
        public string ModifiedBy { get; set; } // ModifiedBy

        public Customers()
        {
            Active = true;
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // CustomerSeeds
    public partial class CustomerSeeds : object
    {
        public int rn { get; set; } // rn (Primary key)
        public string FirstName { get; set; } // FirstName
        public string LastName { get; set; } // LastName
        public string EMail { get; set; } // EMail
        public bool? Active { get; set; } // Active
    }

    // Employee
    public partial class Employee : object
    {
        public int EmployeeID { get; set; } // EmployeeID (Primary key)
        public int? ParentID { get; set; } // ParentID
        public string JobTitle { get; set; } // JobTitle
        public string FirstName { get; set; } // FirstName
        public int? LeftExtent { get; set; } // LeftExtent
        public int? RightExtent { get; set; } // RightExtent

        // Reverse navigation
        public virtual ICollection<Employee> Employee2 { get; set; } // Employee.FK__Employee__Parent__625A9A57

        // Foreign keys
        public virtual Employee Employee1 { get; set; } // FK__Employee__Parent__625A9A57

        public Employee()
        {
            Employee2 = new List<Employee>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Employees
    public partial class Employees : ITrackableEntity
    {
        public int EmployeeId { get; set; } // EmployeeId (Primary key)
        public string PayrollNumber { get; set; } // PayrollNumber
        public string Surname { get; set; } // Surname
        public string FirstName { get; set; } // FirstName
        public string ModifiedBy { get; set; } // ModifiedBy
        public DateTime? ModifiedDate { get; set; } // ModifiedDate
        public string CreatedBy { get; set; } // CreatedBy
        public DateTime? CreatedDate { get; set; } // CreatedDate
    }

    // Function
    public partial class Function : object
    {
        public int FunctionId { get; set; } // FunctionId (Primary key)
        public string Name { get; set; } // Name

        // Reverse navigation
        public virtual ICollection<Permissions> Permissions { get; set; } // Permissions.FK_Permissions_Function

        public Function()
        {
            Permissions = new List<Permissions>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // PaymentType
    public partial class PaymentType : object
    {
        public int PaymentTypeId { get; set; } // PaymentTypeId (Primary key)
        public string PaymentType_ { get; set; } // PaymentType
    }

    // Permissions
    public partial class Permissions : object
    {
        public int PermissionId { get; set; } // PermissionId (Primary key)
        public int? ProjectRoleId { get; set; } // ProjectRoleId
        public int? FunctionId { get; set; } // FunctionId

        // Reverse navigation
        public virtual ICollection<ProjectPermissions> ProjectPermissions { get; set; } // ProjectPermissions.FK_ProjectPermissions_Permissions

        // Foreign keys
        public virtual Function Function { get; set; } // FK_Permissions_Function
        public virtual ProjectRole ProjectRole { get; set; } // FK_Permissions_ProjectRole

        public Permissions()
        {
            ProjectPermissions = new List<ProjectPermissions>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Project
    public partial class Project : object
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
    }

    // ProjectPermissions
    public partial class ProjectPermissions : object
    {
        public int ProjectPermissionId { get; set; } // ProjectPermissionId (Primary key)
        public int? PermissionId { get; set; } // PermissionId
        public int? UserId { get; set; } // UserId

        // Foreign keys
        public virtual Permissions Permissions { get; set; } // FK_ProjectPermissions_Permissions
        public virtual UserProfile UserProfile { get; set; } // FK_ProjectPermissions_UserProfile
    }

    // ProjectRole
    public partial class ProjectRole : object
    {
        public int ProjectRoleId { get; set; } // ProjectRoleId (Primary key)
        public string Name { get; set; } // Name

        // Reverse navigation
        public virtual ICollection<Permissions> Permissions { get; set; } // Permissions.FK_Permissions_ProjectRole

        public ProjectRole()
        {
            Permissions = new List<Permissions>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Settings
    public partial class Settings : object
    {
        public int SettingsId { get; set; } // SettingsId (Primary key)
        public decimal NormalWorkHoursPerDay { get; set; } // NormalWorkHoursPerDay
        public decimal MaximumWorkHoursPerDay { get; set; } // MaximumWorkHoursPerDay
    }

    // SpecicalPermission
    public partial class SpecicalPermission : object
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public string Function { get; set; } // Function

        // Foreign keys
        public virtual UserProfile UserProfile { get; set; } // FK_SpecicalPermission_UserProfile
    }

    // umbCategories
    public partial class umbCategories : object
    {
        public int category_id { get; set; } // category_id (Primary key)
        public int? category_parent_id { get; set; } // category_parent_id
        public string category_name { get; set; } // category_name
        public string description { get; set; } // description
        public byte[] picture { get; set; } // picture

        // Reverse navigation
        public virtual ICollection<umbProducts> umbProducts { get; set; } // umbProducts.FK_umbProducts_umbCategories

        public umbCategories()
        {
            umbProducts = new List<umbProducts>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // umbProducts
    public partial class umbProducts : object
    {
        public int product_id { get; set; } // product_id (Primary key)
        public int category_id { get; set; } // category_id
        public short published { get; set; } // published
        public double rating_cache { get; set; } // rating_cache
        public int rating_count { get; set; } // rating_count
        public string name { get; set; } // name
        public double pricing { get; set; } // pricing
        public string short_description { get; set; } // short_description
        public string long_description { get; set; } // long_description
        public string icon { get; set; } // icon
        public DateTime created_at { get; set; } // created_at
        public DateTime updated_at { get; set; } // updated_at

        // Reverse navigation
        public virtual ICollection<umbReviews> umbReviews { get; set; } // umbReviews.FK_umbReviews_umbProducts

        // Foreign keys
        public virtual umbCategories umbCategories { get; set; } // FK_umbProducts_umbCategories

        public umbProducts()
        {
            published = 0;
            umbReviews = new List<umbReviews>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // umbReviews
    public partial class umbReviews : object
    {
        public int review_id { get; set; } // review_id (Primary key)
        public int product_id { get; set; } // product_id
        public int user_id { get; set; } // user_id
        public int rating { get; set; } // rating
        public string comment { get; set; } // comment
        public short approved { get; set; } // approved
        public short spam { get; set; } // spam
        public DateTime created_at { get; set; } // created_at
        public DateTime updated_at { get; set; } // updated_at

        // Foreign keys
        public virtual umbProducts umbProducts { get; set; } // FK_umbReviews_umbProducts

        public umbReviews()
        {
            approved = 1;
            spam = 0;
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // UserProfile
    public partial class UserProfile : object
    {
        public int UserId { get; set; } // UserId (Primary key)
        public string UserName { get; set; } // UserName
        public bool? IsGroupAdmin { get; set; } // IsGroupAdmin
        public DateTime? LastLogIn { get; set; } // LastLogIn

        // Reverse navigation
        public virtual ICollection<ProjectPermissions> ProjectPermissions { get; set; } // ProjectPermissions.FK_ProjectPermissions_UserProfile
        public virtual ICollection<SpecicalPermission> SpecicalPermission { get; set; } // SpecicalPermission.FK_SpecicalPermission_UserProfile

        public UserProfile()
        {
            ProjectPermissions = new List<ProjectPermissions>();
            SpecicalPermission = new List<SpecicalPermission>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // vwEmployee
    public partial class vwEmployee : ITrackableEntity
    {
        public int EmployeeId { get; set; } // EmployeeId
        public string PayrollNumber { get; set; } // PayrollNumber
        public string Surname { get; set; } // Surname
        public string FirstName { get; set; } // FirstName
        public string CreatedBy { get; set; } // CreatedBy
        public string ModifiedBy { get; set; } // ModifiedBy
        public DateTime? CreatedDate { get; set; } // CreatedDate
        public DateTime? ModifiedDate { get; set; } // ModifiedDate
    }

    // vwSecutiryMatrix
    public partial class vwSecutiryMatrix : object
    {
        public string FunctionName { get; set; } // FunctionName
        public int FunctionId { get; set; } // FunctionId
        public string UserName { get; set; } // UserName
    }

    // vwUserProfile
    public partial class vwUserProfile : object
    {
        public int UserId { get; set; } // UserId
        public string UserName { get; set; } // UserName
        public bool? IsGroupAdmin { get; set; } // IsGroupAdmin
        public DateTime? LastLogIn { get; set; } // LastLogIn
    }

    // vwUserRoles
    public partial class vwUserRoles : object
    {
        public int UserId { get; set; } // UserId
        public string UserName { get; set; } // UserName
        public string ProjectRoles { get; set; } // ProjectRoles
    }


    // ************************************************************************
    // POCO Configuration

    // Customers
    internal partial class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Customers");
            HasKey(x => x.CustomerID);

            Property(x => x.CustomerID).HasColumnName("CustomerID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(64);
            Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(64);
            Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(320);
            Property(x => x.Active).HasColumnName("Active").IsRequired();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsOptional().HasMaxLength(100);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // CustomerSeeds
    internal partial class CustomerSeedsConfiguration : EntityTypeConfiguration<CustomerSeeds>
    {
        public CustomerSeedsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".CustomerSeeds");
            HasKey(x => x.rn);

            Property(x => x.rn).HasColumnName("rn").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FirstName).HasColumnName("FirstName").IsOptional().HasMaxLength(64);
            Property(x => x.LastName).HasColumnName("LastName").IsOptional().HasMaxLength(64);
            Property(x => x.EMail).HasColumnName("EMail").IsRequired().HasMaxLength(320);
            Property(x => x.Active).HasColumnName("Active").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Employee
    internal partial class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Employee");
            HasKey(x => x.EmployeeID);

            Property(x => x.EmployeeID).HasColumnName("EmployeeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ParentID).HasColumnName("ParentID").IsOptional();
            Property(x => x.JobTitle).HasColumnName("JobTitle").IsRequired().HasMaxLength(50);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            Property(x => x.LeftExtent).HasColumnName("LeftExtent").IsOptional();
            Property(x => x.RightExtent).HasColumnName("RightExtent").IsOptional();

            // Foreign keys
            HasOptional(a => a.Employee1).WithMany(b => b.Employee2).HasForeignKey(c => c.ParentID); // FK__Employee__Parent__625A9A57
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Employees
    internal partial class EmployeesConfiguration : EntityTypeConfiguration<Employees>
    {
        public EmployeesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Employees");
            HasKey(x => x.EmployeeId);

            Property(x => x.EmployeeId).HasColumnName("EmployeeId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PayrollNumber).HasColumnName("PayrollNumber").IsRequired().HasMaxLength(50);
            Property(x => x.Surname).HasColumnName("Surname").IsRequired().HasMaxLength(50);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(50);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Function
    internal partial class FunctionConfiguration : EntityTypeConfiguration<Function>
    {
        public FunctionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Function");
            HasKey(x => x.FunctionId);

            Property(x => x.FunctionId).HasColumnName("FunctionId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // PaymentType
    internal partial class PaymentTypeConfiguration : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeConfiguration(string schema = "Ref")
        {
            ToTable(schema + ".PaymentType");
            HasKey(x => x.PaymentTypeId);

            Property(x => x.PaymentTypeId).HasColumnName("PaymentTypeId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PaymentType_).HasColumnName("PaymentType").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Permissions
    internal partial class PermissionsConfiguration : EntityTypeConfiguration<Permissions>
    {
        public PermissionsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Permissions");
            HasKey(x => x.PermissionId);

            Property(x => x.PermissionId).HasColumnName("PermissionId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectRoleId).HasColumnName("ProjectRoleId").IsOptional();
            Property(x => x.FunctionId).HasColumnName("FunctionId").IsOptional();

            // Foreign keys
            HasOptional(a => a.ProjectRole).WithMany(b => b.Permissions).HasForeignKey(c => c.ProjectRoleId); // FK_Permissions_ProjectRole
            HasOptional(a => a.Function).WithMany(b => b.Permissions).HasForeignKey(c => c.FunctionId); // FK_Permissions_Function
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Project
    internal partial class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Project");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // ProjectPermissions
    internal partial class ProjectPermissionsConfiguration : EntityTypeConfiguration<ProjectPermissions>
    {
        public ProjectPermissionsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ProjectPermissions");
            HasKey(x => x.ProjectPermissionId);

            Property(x => x.ProjectPermissionId).HasColumnName("ProjectPermissionId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PermissionId).HasColumnName("PermissionId").IsOptional();
            Property(x => x.UserId).HasColumnName("UserId").IsOptional();

            // Foreign keys
            HasOptional(a => a.Permissions).WithMany(b => b.ProjectPermissions).HasForeignKey(c => c.PermissionId); // FK_ProjectPermissions_Permissions
            HasOptional(a => a.UserProfile).WithMany(b => b.ProjectPermissions).HasForeignKey(c => c.UserId); // FK_ProjectPermissions_UserProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // ProjectRole
    internal partial class ProjectRoleConfiguration : EntityTypeConfiguration<ProjectRole>
    {
        public ProjectRoleConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ProjectRole");
            HasKey(x => x.ProjectRoleId);

            Property(x => x.ProjectRoleId).HasColumnName("ProjectRoleId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Settings
    internal partial class SettingsConfiguration : EntityTypeConfiguration<Settings>
    {
        public SettingsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Settings");
            HasKey(x => x.SettingsId);

            Property(x => x.SettingsId).HasColumnName("SettingsId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NormalWorkHoursPerDay).HasColumnName("NormalWorkHoursPerDay").IsRequired().HasPrecision(18,1);
            Property(x => x.MaximumWorkHoursPerDay).HasColumnName("MaximumWorkHoursPerDay").IsRequired().HasPrecision(18,1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // SpecicalPermission
    internal partial class SpecicalPermissionConfiguration : EntityTypeConfiguration<SpecicalPermission>
    {
        public SpecicalPermissionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".SpecicalPermission");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.Function).HasColumnName("Function").IsRequired().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.UserProfile).WithMany(b => b.SpecicalPermission).HasForeignKey(c => c.UserId); // FK_SpecicalPermission_UserProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // umbCategories
    internal partial class umbCategoriesConfiguration : EntityTypeConfiguration<umbCategories>
    {
        public umbCategoriesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".umbCategories");
            HasKey(x => x.category_id);

            Property(x => x.category_id).HasColumnName("category_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.category_parent_id).HasColumnName("category_parent_id").IsOptional();
            Property(x => x.category_name).HasColumnName("category_name").IsRequired().HasMaxLength(15);
            Property(x => x.description).HasColumnName("description").IsOptional().HasMaxLength(1073741823);
            Property(x => x.picture).HasColumnName("picture").IsOptional().HasMaxLength(2147483647);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // umbProducts
    internal partial class umbProductsConfiguration : EntityTypeConfiguration<umbProducts>
    {
        public umbProductsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".umbProducts");
            HasKey(x => x.product_id);

            Property(x => x.product_id).HasColumnName("product_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.category_id).HasColumnName("category_id").IsRequired();
            Property(x => x.published).HasColumnName("published").IsRequired();
            Property(x => x.rating_cache).HasColumnName("rating_cache").IsRequired();
            Property(x => x.rating_count).HasColumnName("rating_count").IsRequired();
            Property(x => x.name).HasColumnName("name").IsRequired().HasMaxLength(255);
            Property(x => x.pricing).HasColumnName("pricing").IsRequired();
            Property(x => x.short_description).HasColumnName("short_description").IsRequired().HasMaxLength(255);
            Property(x => x.long_description).HasColumnName("long_description").IsRequired();
            Property(x => x.icon).HasColumnName("icon").IsRequired().HasMaxLength(255);
            Property(x => x.created_at).HasColumnName("created_at").IsRequired();
            Property(x => x.updated_at).HasColumnName("updated_at").IsRequired();

            // Foreign keys
            HasRequired(a => a.umbCategories).WithMany(b => b.umbProducts).HasForeignKey(c => c.category_id); // FK_umbProducts_umbCategories
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // umbReviews
    internal partial class umbReviewsConfiguration : EntityTypeConfiguration<umbReviews>
    {
        public umbReviewsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".umbReviews");
            HasKey(x => x.review_id);

            Property(x => x.review_id).HasColumnName("review_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.product_id).HasColumnName("product_id").IsRequired();
            Property(x => x.user_id).HasColumnName("user_id").IsRequired();
            Property(x => x.rating).HasColumnName("rating").IsRequired();
            Property(x => x.comment).HasColumnName("comment").IsRequired();
            Property(x => x.approved).HasColumnName("approved").IsRequired();
            Property(x => x.spam).HasColumnName("spam").IsRequired();
            Property(x => x.created_at).HasColumnName("created_at").IsRequired();
            Property(x => x.updated_at).HasColumnName("updated_at").IsRequired();

            // Foreign keys
            HasRequired(a => a.umbProducts).WithMany(b => b.umbReviews).HasForeignKey(c => c.product_id); // FK_umbReviews_umbProducts
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // UserProfile
    internal partial class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserProfile");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(100);
            Property(x => x.IsGroupAdmin).HasColumnName("IsGroupAdmin").IsOptional();
            Property(x => x.LastLogIn).HasColumnName("LastLogIn").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // vwEmployee
    internal partial class vwEmployeeConfiguration : EntityTypeConfiguration<vwEmployee>
    {
        public vwEmployeeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vwEmployee");
            HasKey(x => new { x.EmployeeId, x.PayrollNumber, x.Surname, x.FirstName });

            Property(x => x.EmployeeId).HasColumnName("EmployeeId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PayrollNumber).HasColumnName("PayrollNumber").IsRequired().HasMaxLength(50);
            Property(x => x.Surname).HasColumnName("Surname").IsRequired().HasMaxLength(50);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(50);
            Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsOptional().HasMaxLength(50);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // vwSecutiryMatrix
    internal partial class vwSecutiryMatrixConfiguration : EntityTypeConfiguration<vwSecutiryMatrix>
    {
        public vwSecutiryMatrixConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vwSecutiryMatrix");
            HasKey(x => new { x.FunctionId, x.UserName });

            Property(x => x.FunctionName).HasColumnName("FunctionName").IsOptional().HasMaxLength(50);
            Property(x => x.FunctionId).HasColumnName("FunctionId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(100);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // vwUserProfile
    internal partial class vwUserProfileConfiguration : EntityTypeConfiguration<vwUserProfile>
    {
        public vwUserProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vwUserProfile");
            HasKey(x => new { x.UserId, x.UserName });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(100);
            Property(x => x.IsGroupAdmin).HasColumnName("IsGroupAdmin").IsOptional();
            Property(x => x.LastLogIn).HasColumnName("LastLogIn").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // vwUserRoles
    internal partial class vwUserRolesConfiguration : EntityTypeConfiguration<vwUserRoles>
    {
        public vwUserRolesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vwUserRoles");
            HasKey(x => new { x.UserId, x.UserName });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(100);
            Property(x => x.ProjectRoles).HasColumnName("ProjectRoles").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}

