using MediatR;
using WeeTestIA.Application.Models.Common;

namespace WeeTestIA.Application.Features.Perfil.Commands.UpdateCatPerfil;

public class UpdateCatPerfilCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public UpdateCatPerfilCommand(int id, string nombre, string descripcion)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
    }

}
