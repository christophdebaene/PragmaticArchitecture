using MyApp.Domain.Model;
using System;

namespace MyApp.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task GetById(Guid id);
        void Add(Task task);
        void Delete(Task task);
    }
}