using Microsoft.EntityFrameworkCore;
using VMS.Entities;

namespace VMS.Repository
{
    public partial class VMSDatabaseContext
    {
        public async Task<int> ApplyChanges<TEntity>(TEntity root) where TEntity : class, IObjectWithState
        {
            Set<TEntity>().Add(root);
            CheckForEntitiesWithoutStateInterface(this);
            foreach (var entry in ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.State);
            }

            return await SaveChangesAsync();
        }

        private static EntityState ConvertState(ModelState state)
        {
            switch (state)
            {
                case ModelState.Added:
                    return EntityState.Added;
                case ModelState.Modified:
                    return EntityState.Modified;
                case ModelState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        private static void CheckForEntitiesWithoutStateInterface(VMSDatabaseContext context)
        {
            var entitiesWithoutState =
            from e in context.ChangeTracker.Entries()
            where !(e.Entity is IObjectWithState)
            select e;
            if (entitiesWithoutState.Any())
            {
                throw new NotSupportedException("IObjectWithState not implemented");
            }
        }
    }
}