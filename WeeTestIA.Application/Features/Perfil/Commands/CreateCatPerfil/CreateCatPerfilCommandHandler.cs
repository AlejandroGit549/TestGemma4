using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using WeeTestIA.Application.Contracts.Persistence;
using WeeTestIA.Application.Models.Common;
using WeeTestIA.Domain;

namespace WeeTestIA.Application.Features.Perfil.Commands.CreateCatPerfil;

public class CreateCatPerfilCommandHandler : IRequestHandler<CreaCatPerfilCommand, Response<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCatPerfilCommandHandler> _logger;

    public CreateCatPerfilCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateCatPerfilCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<int>> Handle(CreaCatPerfilCommand request, CancellationToken cancellationToken)
    {
        var _entityCatPerfil = new CatPerfil()
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
        };

        _unitOfWork.CatPerfilRepository.AddEntity(_entityCatPerfil);
        var _insert = await _unitOfWork.Complete();
        if(_insert <= 0)
        {
            _logger.LogError("No se inserto el record de CatPerfil");
            throw new Exception("No se pudo insertar el record de CatPefil");
        }
        return new Response<int>()
        {
           Data = _entityCatPerfil.Id,
           Message = "Perfil creado exitosamente.",
           Success = true,
           StatusCode = (short)HttpStatusCode.OK
        };
    }
}
