using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<Categorias>> GetAll();
        public Task<Categorias> Add(Categorias obj);
    }
}
