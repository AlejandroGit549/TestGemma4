using WeeTestIA.Application.Contracts.Persistence;
using WeeTestIA.Domain;
using WeeTestIA.Infrastructure.Persistence;

namespace WeeTestIA.Infrastructure.Repositories;

public class CatPerfilRepository : RepositoryBase<CatPerfil>, ICatPerfilRepository
{
    public CatPerfilRepository(AppTestIaContext context):base(context){}
}
