using Play.Common.Service.Entities;

namespace Play.Inventory.Service.Entites
{
    public class InventoryItem: IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CatalogItemId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }
    }
}
