﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using eMTE.Common.Domain;

namespace eMTE.Common.DataAccess
{
    public interface IQueryableConnector
    {
        DbSet<TDomain> GetDbSet<TDomain>()
            where TDomain : class, IDomain;
        IQueryable<TDomain> ReadAll<TDomain>()
            where TDomain : class, IDomain;
        IQueryable<TDomain> FromSQL<TDomain>(string sqlQuery)
            where TDomain : class, IDomain;
        IQueryable<TDomain> ReadByIds<TDomain>(Guid[] ids)
            where TDomain : class, IDomain, IWithId;
        TDomain ReadById<TDomain>(Guid id)
            where TDomain : class, IDomain;
        TDomain Update<TDomain>(TDomain domain)
            where TDomain : class, IDomain;
        TDomain Create<TDomain>(TDomain domain)
            where TDomain : class, IDomain;
        void DeleteAll<TDomain>()
            where TDomain : class, IDomain;
        Task DeleteByIds<TDomain>(Guid[] ids, CancellationToken cancellationToken)
           where TDomain : class, IDomain, IWithId;
        Task<TDomain> ReadByIdAsync<TDomain>(Guid id, CancellationToken cancellationToken)
            where TDomain : class, IDomain;
        Task<EntityEntry<TDomain>> CreateAsync<TDomain>(TDomain domain, CancellationToken cancellationToken)
            where TDomain : class, IDomain;
        Task CreateMultipleAsync<TDomain>(TDomain[] domain, CancellationToken cancellationToken)
            where TDomain : class, IDomain;

        Task<TDomain[]> ReadAllAsync<TDomain>(CancellationToken token)
            where TDomain : class, IDomain;
        Task<TDomain[]> ReadByIdsAsync<TDomain>(Guid[] ids, CancellationToken token)
            where TDomain : class, IDomain, IWithId;
        Task<TDomain> ReadByIdAsync<TDomain>(string id, CancellationToken cancellationToken)
            where TDomain : class, IDomain;
    }
}
