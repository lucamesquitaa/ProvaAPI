using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductsService
    {
        public Task<IEnumerable<Produtos>> GetAll(string term, int page, int pageSize);
        public Task<Produtos> Add(Produtos obj);
        public Task<Produtos> Update(Produtos obj);
        public Task<Produtos> Delete(long id);
    }
}
