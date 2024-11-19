using Library_Management_System.Models;

namespace Library_Management_System.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
        void CheckPenalty();
        void Delete(Checkout checkout);
    }

}
