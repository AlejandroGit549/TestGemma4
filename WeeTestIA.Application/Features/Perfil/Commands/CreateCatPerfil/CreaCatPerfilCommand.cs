using MediatR;
using WeeTestIA.Application.Models.Common;

namespace WeeTestIA.Application.Features.Perfil.Commands.CreateCatPerfil;

public class CreaCatPerfilCommand : IRequest<Response<int>>
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public CreaCatPerfilCommand(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
    public CreaCatPerfilCommand()
    {
        Nombre = string.Empty;
        Descripcion = string.Empty;
    }
}
