using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using WeeTestIA.Application.Contracts.Persistence;
using WeeTestIA.Application.Features.Perfil.Commands.DeleteCatPefil;
using WeeTestIA.Application.Models.Common;

namespace WeeTestIA.Application.Features.Perfil.Commands.UpdateCatPerfil;

public class UpdateCatPerfilCommandHandler : IRequestHandler<UpdateCatPerfilCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCatPerfilCommandHandler> _logger;

    public UpdateCatPerfilCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateCatPerfilCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<bool>> Handle(UpdateCatPerfilCommand request, CancellationToken cancellationToken)
    {
        var _entityCatPerfil = await _unitOfWork.CatPerfilRepository.GetByIdAsync(request.Id);

        if (_entityCatPerfil is null)
            return new Response<bool>()
            {
                Data = false,
                Message = $"No se encuentra ningún elemento con el Id: {request.Id}",
                StatusCode = (short)HttpStatusCode.NotFound,
                Success = false
            };


        _entityCatPerfil.Descripcion = request.Descripcion;
        _entityCatPerfil.Nombre = request.Nombre;
        _entityCatPerfil.FechaActualización = DateTime.Now;

        await _unitOfWork.CatPerfilRepository.UpdateAsync(_entityCatPerfil);

        var _update = await _unitOfWork.Complete();

        if (_update <= 0)
            throw new Exception("No se pudo actualizar el record de CatPefil");

        return new Response<bool>()
        {
            Data = true,
            Message = "Elemento actualizado correctamente.",
            StatusCode = (short)HttpStatusCode.OK,
            Success = true
        };



    }
}
