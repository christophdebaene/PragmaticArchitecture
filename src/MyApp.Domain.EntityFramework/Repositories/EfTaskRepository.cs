using MyApp.Domain.EntityFramework;
using MyApp.Domain.Model;
using System;

namespace MyApp.Domain.Repositories
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly IUnitOfWork _uow;

        public EfTaskRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _uow = uow;
        }

        public Model.Task GetById(Guid id)
        {
            return _uow.Set<Task>().Find(id);
        }

        public void Add(Task task)
        {
            _uow.Set<Task>().Add(task);
        }

        public void Delete(Task task)
        {
            _uow.Set<Task>().Remove(task);
        }
    }
}