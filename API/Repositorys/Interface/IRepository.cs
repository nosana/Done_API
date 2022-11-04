namespace API.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        public IEnumerable<Entity> Get();
        public Entity GetById(Key id);
        public int Create(Entity Entity);
      
        public int Update(Entity Entity);
        public int Delete(Key id);
    }
    
}
