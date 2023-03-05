using Play.Common.Service.Entities;

namespace Play.Inventory.Service.Entites
{
    public class CatalogItem : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
