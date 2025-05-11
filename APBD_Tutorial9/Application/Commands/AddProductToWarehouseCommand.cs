using MediatR;

namespace APBD_Tutorial9.Application.Commands;

public record AddProductToWarehouseCommand : IRequest<(bool success, string message, int resultId)>
{
    public int IdProduct { get; set; }
    public int IdWirehouse { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}