using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class ContextExtensions
    {
        public static void AddOrUpdate(this DbContext context, object entity)
        {
            var entry = context.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    context.Add(entity);
                    break;
                case EntityState.Modified:
                    context.Update(entity);
                    break;
                case EntityState.Added:
                    context.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}