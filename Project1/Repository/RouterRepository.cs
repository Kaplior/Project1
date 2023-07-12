using Project1.Data;
using Project1.Interface;
using Project1.Models;

namespace Project1.Repository
{
    public class RouterRepository : IRouterRepository
    {
        private readonly DataContext _context;

        public RouterRepository(DataContext context)
        {
            _context = context;
        }

        public Router GetRouter(int id)
        {
            return _context.Routers.Where(r => r.Id == id).FirstOrDefault();
        }
        public ICollection<Router> GetRouters()
        {
            return _context.Routers.OrderBy(r => r.Id).ToList();
        }
        public bool RouterExists(int Rid)
        {
            return _context.Routers.Any(r => r.Id == Rid);
            //throw new NotImplementedException();
        }

        public bool CreateRouter(Router router)
        {
            _context.Add(router);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
