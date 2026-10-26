using System;
using DirectoryService.Core.Departments;
using DirectoryService.Domain.DepartmentSpace;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Departments;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly AppDbContext _dbContext;

    public DepartmentsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> AddAsync(Department department, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Department>().AddAsync(department, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return department.Id;
    }

    public async Task<bool> CheckParentIdAsync(Guid? parentId, CancellationToken cancellationToken)
    {
        if (parentId == null)
        {
            return true;
        }

        return await _dbContext.Set<Department>().AnyAsync(d => d.Id == parentId, cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
