using System.Collections;
using WeeTestIA.Application.Contracts.Persistence;
using WeeTestIA.Infrastructure.Persistence;

namespace WeeTestIA.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private Hashtable _repositories;
    private readonly AppTestIaContext _context;

    public UnitOfWork(AppTestIaContext context)
    {
        _context = context;
    }

    public AppTestIaContext Context => _context;
    private ICatPerfilRepository _catPerfilRepository;
    public ICatPerfilRepository CatPerfilRepository => _catPerfilRepository ?? new CatPerfilRepository(_context);

    public async Task<int> Complete()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al guardar: {ex.Message}");
        }
    }

    public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<TEntity>)_repositories[type]!;
    }
}
