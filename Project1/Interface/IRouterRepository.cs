using Project1.Models;

namespace Project1.Interface
{
    public interface IRouterRepository
    {
        ICollection<Router> GetRouters();
        Router GetRouter(int id);
        bool RouterExists(int Rid);
        bool CreateRouter(Router router);
        bool UpdateRouter(Router router);
        bool Save();
    }
}
