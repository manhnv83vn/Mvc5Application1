namespace Mvc5Application1.Data.Model
{
    public interface IUnitOfWork
    {
        void Refresh();

        void SaveChanges();
    }
}