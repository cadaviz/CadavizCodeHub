namespace CadavizCodeHub.Domain.Services
{
    using System;
    using CadavizCodeHub.Domain.Entities;
    using CadavizCodeHub.Domain.Gateways;

    internal class InventoryService : IInventoryService
    {
        private readonly IInventoryGateway _inventoryGateway;

        public InventoryService(IInventoryGateway inventoryGateway)
        {
            _inventoryGateway = inventoryGateway;
        }

        public bool ConfirmReservation(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
