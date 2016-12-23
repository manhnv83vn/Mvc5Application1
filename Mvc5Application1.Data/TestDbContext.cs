using Mvc5Application1.Data.Model;
using Mvc5Application1.Framework.Security;
using System;
using System.Data.Entity;

namespace Mvc5Application1.Data
{
    public class TestDbContext : DBContextBase, IUnitOfWork
    {
        public TestDbContext()
            : base("name=TestDBConnectionString")
        {
        }

        public void Refresh()
        {
            // Undo the changes of the all entries.
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from
                    // Modified to Unchanged first sets the values of all
                    // properties to the original values that were read from
                    // the database when it was queried, and then marks the
                    // entity as Unchanged. This will also reject changes to
                    // FK relationships since the original value of the FK
                    // will be restored.
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.
                    case EntityState.Deleted:
                        entry.Reload();
                        break;

                    default: break;
                }
            }
        }

        void IUnitOfWork.SaveChanges()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            foreach (var changeEntry in ChangeTracker.Entries<ITrackableEntity>())
            {
                var now = DateTime.Now;
                var userName = Mvc5Application1PrincipalHelpers.GetUserName();
                if (changeEntry.State == EntityState.Added)
                {
                    changeEntry.Entity.CreatedBy = userName;
                    changeEntry.Entity.CreatedDate = now;
                }
                else if (changeEntry.State == EntityState.Modified)
                {
                    changeEntry.Property(x => x.CreatedBy).IsModified = false;
                    changeEntry.Property(x => x.CreatedDate).IsModified = false;

                    changeEntry.Entity.ModifiedDate = now;
                    changeEntry.Entity.ModifiedBy = userName;
                }
                else
                {
                    changeEntry.Property(x => x.CreatedBy).IsModified = false;
                    changeEntry.Property(x => x.CreatedDate).IsModified = false;
                }
            }
            Configuration.ValidateOnSaveEnabled = false;
            return base.SaveChanges();
        }
    }
}