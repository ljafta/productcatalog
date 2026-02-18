namespace ProductCatalog.Api.Repositories.Interfaces
{
    public abstract class Repository <T> : IRepository<T>
    {
        public abstract IEnumerable<T> GetAll();
        public abstract T? GetById(Guid id);
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract bool Delete(Guid id);
    }
}
