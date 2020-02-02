using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Entities;
using Entities.Attributes;
using Entities.Enums;
using Entities.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }

        private DbSet<SystemSettings> SystemSettings { get; set; }

        public Task<SystemSettings> Settings => SystemSettings.FirstOrDefaultAsync();

        public DbSet<UserActionAudit> UserActionAudit { get; set; }

        public void Save(string auditMessage = null, bool enableUserAudit = true)
        {
            // TODO: Encryption

            PopulateRecordAudit();

            var additions = GenerateUserAudits(auditMessage, enableUserAudit);

            SaveChanges();

            SaveUserAuditAdditions(enableUserAudit, additions, auditMessage);
        }

        /// <summary>
        /// New entities are saved before we add audits so tha we can retrieve Id's
        /// </summary>
        /// <param name="enableUserAudit"></param>
        /// <param name="additions"></param>
        /// <param name="auditMessage"></param>
        private void SaveUserAuditAdditions(
            bool enableUserAudit,
            IEnumerable<EntityEntry<IEntityBase>> additions,
            string auditMessage = null)
        {
            if (!enableUserAudit) return;
            var username = ClaimsPrincipal.Current.Identity.Name;
            var user = Users.FirstOrDefault(x => x.UserName == username);

            if (!additions.Any()) return;
            var addAudits = additions.Select(added => new UserActionAudit
                {
                    Action = AuditAction.Creation,
                    EntityId = GetNewEntityId(added),
                    Description = auditMessage ?? string.Empty,
                    UserId = user.Id,
                    EntityType = added.Entity.GetType().Name
                })
                .ToList();
            Set<UserActionAudit>().AddRange(addAudits);
            SaveChanges();
        }

        private static int GetNewEntityId(EntityEntry<IEntityBase> added)
        {
            return added.State == EntityState.Unchanged
                ? int.Parse(added.CurrentValues[nameof(IEntityBase.Id)].ToString())
                : 0;
        }

        /// <summary>
        /// Generate user audit logs for <see cref="IEntityBase"/> entities which have the <see cref="EnableUserAudit"/> attribute
        /// </summary>
        /// <param name="auditMessage">Optionally supply a message for the audit description</param>
        /// <param name="enableUserAudit">Optionally, you can disable audit logging</param>
        /// <returns>New entities are returned so we can save changes so that we can retrieve Id's</returns>
        private IEnumerable<EntityEntry<IEntityBase>> GenerateUserAudits(string auditMessage, bool enableUserAudit)
        {
            IEnumerable<EntityEntry<IEntityBase>> additions = new List<EntityEntry<IEntityBase>>();

            if (!enableUserAudit) return additions;
            additions = GetEntityAdditions();
            var modifications = GetEntityModifications();
            var deletions = GetEntityDeletions();

            var username = ClaimsPrincipal.Current.Identity.Name;
            var user = Users.FirstOrDefault(x => x.UserName == username);

            if (user == null) return additions;

            if (modifications != null && modifications.Any())
            {
                var editAudits = modifications.Select(modification => new UserActionAudit
                {
                    Action = AuditAction.Modification,
                    EntityId = modification.Entity.Id,
                    Description = this.GetCompleteAuditMessage(modification, auditMessage),
                    UserId = user.Id,
                    EntityType = GetEntityName(modification)
                })
                .ToList();
                Set<UserActionAudit>().AddRange(editAudits);
            }

            if (deletions != null && deletions.Any())
            {
                var deleteAudits = deletions.Select(deletion => new UserActionAudit
                {
                    Action = AuditAction.Deletion,
                    EntityId = deletion.Entity.Id,
                    Description = auditMessage ?? string.Empty,
                    UserId = user.Id,
                    EntityType = GetEntityName(deletion)
                })
                    .ToList();
                Set<UserActionAudit>().AddRange(deleteAudits);
            }

            return additions;
        }

        private static string GetEntityName(EntityEntry<IEntityBase> modification)
        {
            return modification.Entity.GetType().BaseType?.Name ?? modification.Entity.GetType().Name;
        }

        private string GetCompleteAuditMessage(EntityEntry<IEntityBase> dbEntityEntry, string auditMessage)
        {
            var type = dbEntityEntry.Entity.GetType();

            var valueChangedAuditProperties = type.GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(ValueChangedAudit), true).Any()).Select(x => x.Name);

            if (!valueChangedAuditProperties.Any()) return auditMessage ?? string.Empty;

            var stringBuilder = new StringBuilder(auditMessage);

            foreach (var valueChangedProperty in valueChangedAuditProperties)
            {
                var currentValue = dbEntityEntry.CurrentValues[valueChangedProperty]?.ToString().AddSpaces() ?? string.Empty;
                var originalValue = dbEntityEntry.OriginalValues[valueChangedProperty]?.ToString().AddSpaces() ?? string.Empty;

                if (currentValue == originalValue) continue;

                var message = $"{valueChangedProperty.AddSpaces()} changed from {originalValue} to {currentValue}";

                stringBuilder.AppendLine(message);
            }

            return stringBuilder.ToString();
        }

        private IEnumerable<EntityEntry<IEntityBase>> GetEntityDeletions()
        {
            var deletions = ChangeTracker.Entries<IEntityBase>()
                .Where(x => x.State == EntityState.Deleted)
                .Where(x => x.Entity.GetType().HasAttribute<EnableUserAudit>());
            return deletions.ToList();
        }

        private IEnumerable<EntityEntry<IEntityBase>> GetEntityModifications()
        {
            var modifications = ChangeTracker.Entries<IEntityBase>()
                .Where(x => x.State == EntityState.Modified && x.CurrentValues.Properties.Select(p => p.Name)
                                .Any(y => (x.Property(y).CurrentValue ?? string.Empty).ToString() != (x.Property(y).OriginalValue ?? string.Empty).ToString()))
                .Where(x => x.Entity.GetType().HasAttribute<EnableUserAudit>());

            return modifications.ToList();
        }

        private IEnumerable<EntityEntry<IEntityBase>> GetEntityAdditions()
        {
            var additions = ChangeTracker.Entries<IEntityBase>()
                .Where(x => x.State == EntityState.Added)
                .Where(x => x.Entity.GetType().HasAttribute<EnableUserAudit>());
            return additions.ToList();
        }

        private void PopulateRecordAudit()
        {
            var addedAuditedEntities = ChangeTracker.Entries<AuditedEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntries = ChangeTracker.Entries<AuditedEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var now = DateTime.Now;

            var username = ClaimsPrincipal.Current.Identity.Name;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedBy = username;
                if (added.CreatedDate == null)
                {
                    added.CreatedDate = now;
                }
                added.LastModifiedBy = username;
                added.LastModifiedDate = now;
            }

            foreach (var modified in modifiedAuditedEntries)
            {
                modified.LastModifiedBy = username;
                modified.LastModifiedDate = now;
            }

        }
    }
}