namespace CadavizCodeHub.Domain.Services
{
    using CadavizCodeHub.Domain.Entities;

    internal interface IInventoryService
    {
        bool ConfirmReservation(Order order);
    }
}
