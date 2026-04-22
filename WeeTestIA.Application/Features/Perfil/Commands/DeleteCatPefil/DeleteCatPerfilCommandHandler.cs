using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using WeeTestIA.Application.Contracts.Persistence;
using WeeTestIA.Application.Features.Perfil.Commands.CreateCatPerfil;
using WeeTestIA.Application.Models.Common;

namespace WeeTestIA.Application.Features.Perfil.Commands.DeleteCatPefil;

public class DeleteCatPerfilCommandHandler : IRequestHandler<DeleteCatPerfilCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCatPerfilCommandHandler> _logger;

    public DeleteCatPerfilCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCatPerfilCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<bool>> Handle(DeleteCatPerfilCommand request, CancellationToken cancellationToken)
    {
        var _entityCatPerfil = await _unitOfWork.CatPerfilRepository.GetByIdAsync(request.Id);

        if (_entityCatPerfil is null)
            return new Response<bool>()
            {
                Data = false,
                Message = $"No se encontro un elemento con el Id: {request.Id}",
                Success = false,
                StatusCode = (short)HttpStatusCode.NotFound
            };

        await _unitOfWork.CatPerfilRepository.DeleteAsync(_entityCatPerfil);
        var _delete = await _unitOfWork.Complete();
        if (_delete <= 0)
            throw new Exception("No se pudo eliminar el record de CatPefil");

        return new Response<bool>()
        {
            Data = true,
            Message = "Elemento eliminado correctamente.",
            StatusCode = (short)HttpStatusCode.OK,
            Success = true
        };
    }
}
