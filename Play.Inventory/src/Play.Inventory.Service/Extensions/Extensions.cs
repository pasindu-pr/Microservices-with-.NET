using Play.Inventory.Service.Entites;
using static Play.Inventory.Service.Dto.Dto;

namespace Play.Inventory.Service.Extensions
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemDto(item.CatalogItemId, name, description, item.Quantity, item.AcquiredDate);
        }
    }
}
