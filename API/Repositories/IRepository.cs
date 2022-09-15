using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories
{
    public interface IRepository<T1, T2> where T1:class
    {
        Task<List<T1>> GetAll();
        Task<T1> GetById(T2 id);
        Task<T1> Add(T1 entity);
        Task<Boolean> Delete(T2 id);
        Task Save();
    }
}