using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProvaContext _context;
        public ProductsService(ProvaContext context)
        {
            _context = context;
        }
        public async Task<Produtos> Add(Produtos obj)
        {
            try
            {
                if (obj.Nome == null || obj.Nome == "")
                {
                    throw new Exception("'Nome' can't be null");
                }
                var teste = _context.Produtos.Any(l => l.Nome == obj.Nome);
                if (teste)
                {
                    throw new Exception("'Nome' already exists");
                }
                if (obj.PrecoUnitario <= 0)
                {
                    throw new Exception("'PrecoUnitario' can't be negative or 0");
                }
                if(obj.QuantidadeEstoque <= 0){
                    throw new Exception("'QuantidadeEstoque' can't be negative or 0");
                }
                
                _context.Produtos.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
               

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public async Task<IEnumerable<Produtos>> GetAll(string term, int page, int pageSize)
        {
            try
            {
                if(term != null) //aqui faz a pesquisa por filtro
                {
                    var pesquisa = _context.Produtos.Where(s => s.Nome.Contains(term)
                               || s.Categoria.Nome.Contains(term));

                    if (pesquisa.Any())
                    {
                        return pesquisa;
                    }
                }
                if(page > 0 || pageSize > 0)//aqui faz a paginação
                {
                    var paginacao = await _context.Produtos.Skip(page).Take(pageSize).ToListAsync();
                    return paginacao;
                }
                var response = await _context.Produtos.ToListAsync();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Produtos> Update(Produtos obj)
        {
            try
            {
                long id = await _context.Produtos.Where(x => x.Id == obj.Id).Select(x => x.Id).FirstOrDefaultAsync();
                if (id <= 0)
                {
                    throw new Exception("Was not found a matching id");
                }
                if(obj.Nome == null || obj.Nome == "")
                {
                    throw new Exception("'Nome' can't be null");
                }
                if (obj.PrecoUnitario <= 0)
                {
                    throw new Exception("'PrecoUnitario' can't be negative or 0");
                }
                if (obj.QuantidadeEstoque <= 0)
                {
                    throw new Exception("'QuantidadeEstoque' can't be negative or 0");
                }

                _context.Produtos.Update(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public async Task<Produtos> Delete(long id)
        {
            try
            {
                var response = await _context.Produtos.FindAsync(id);
                if (response == null)
                {
                    throw new Exception("Can't find 'id'");
                }
                _context.Produtos.Remove(response);
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
