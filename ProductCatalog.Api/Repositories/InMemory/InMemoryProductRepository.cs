using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.Repositories;
using ProductCatalog.Api.Repositories.Interfaces;

namespace ProductCatalog.Api.Repositories.InMemory
{
    //In-Memory Product Repository (NO EF)
    //This is a fake, in-memory database used to store Products while the app is running.
    //No SQL.No EF.No disk. Just RAM.
    public class InMemoryProductRepository : Repository<Product>
    {
        private readonly Dictionary<Guid, Product> _store = new();

        public override IEnumerable<Product> GetAll() => _store.Values;

        public override Product? GetById(Guid id)
            => _store.TryGetValue(id, out var product) ? product : null;

        public override void Add(Product entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _store[entity.Id] = entity;
        }

        public override void Update(Product entity)
        {
            if (!_store.ContainsKey(entity.Id)) return;

            entity.UpdatedAt = DateTime.UtcNow;
            _store[entity.Id] = entity;
        }

        public override bool Delete(Guid id)
        {
            return _store.Remove(id);
        }
    }

    /// <summary>
    /// In-memory repository for categories
    /// Simulates a database using Dictionary
    /// </summary>
    public class InMemoryCategoryRepository : Repository<Category>
    {
        private readonly Dictionary<Guid, Category> _store = new();

        public override IEnumerable<Category> GetAll()
            => _store.Values;

        public override Category? GetById(Guid id)
            => _store.TryGetValue(id, out var category) ? category : null;

        public override void Add(Category entity)
        {
            entity.Id = Guid.NewGuid();
            _store[entity.Id] = entity;
        }

        public override void Update(Category entity)
        {
            if (!_store.ContainsKey(entity.Id)) return;
            _store[entity.Id] = entity;
        }

        public override bool Delete(Guid id)
        {
            return _store.Remove(id);
        }
    }
}
