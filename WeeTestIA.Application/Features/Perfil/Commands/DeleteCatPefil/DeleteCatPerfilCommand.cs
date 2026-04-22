using MediatR;
using WeeTestIA.Application.Models.Common;

namespace WeeTestIA.Application.Features.Perfil.Commands.DeleteCatPefil;

public class DeleteCatPerfilCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public DeleteCatPerfilCommand(int id)
    {
        Id = id;
    }

}
