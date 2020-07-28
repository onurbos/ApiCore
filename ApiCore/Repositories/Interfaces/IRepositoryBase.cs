using ApiCore.Context;
using ApiCore.Helpers;

namespace ApiCore.Repositories.Interfaces
{
    public interface IRepositoryBase
    {
        CurrentUser CurrentUser { get; set; }
        AppDBContext DbContext { get; }
    }
}