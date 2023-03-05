using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Contracts;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Extensions;
using Play.Common.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IRepository<Item> _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(IRepository<Item> repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        {
            var items = (await _repository.GetAllAsync()).Select(item => AsDto.AsItemDto(item));
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsItemDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Id = Guid.NewGuid(), 
                Name = createItemDto.Name,
                Description = createItemDto.Description, 
                Price = createItemDto.Price, 
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateAsync(item);

            await _publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction("GetById", new {id = item.Id}, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _repository.GetByIdAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _repository.UpdateAsync(existingItem);
            await _publishEndpoint.Publish(new CatalogItemUpdated(id, updateItemDto.Name, updateItemDto.Description));
            
            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _publishEndpoint.Publish(new CatalogItemDeleted(id));
            return NoContent();
         }
    }
}
