using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ProvaContext _context;
        public CategoriesService(ProvaContext context)
        {
            _context = context;
        }

        public async Task<Categorias> Add(Categorias obj)
        {
            try
            {
                if (obj.Nome == null || obj.Nome == "")
                {
                    throw new Exception("'Nome' can't be null"); 
                }
                var teste = _context.Categorias.Any(l => l.Nome == obj.Nome);
                if (teste)
                {
                    throw new Exception("'Nome' already exists");
                }
                
                _context.Categorias.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
                

            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            

        }

        public async Task<IEnumerable<Categorias>> GetAll()
        {
            try
            {
                var response = await _context.Categorias.ToListAsync();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
