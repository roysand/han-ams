﻿using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAppclicationDbContext
    {
        DbSet<RawData> RawSet { get; set; }
        DbSet<Detail> DetailSet { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}