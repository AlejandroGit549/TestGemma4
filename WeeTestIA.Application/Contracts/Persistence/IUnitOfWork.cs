namespace WeeTestIA.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> Complete();

    //Interfacs 
    ICatPerfilRepository CatPerfilRepository { get; }
}
