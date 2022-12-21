using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Repository
{
    internal class UserRepository
    {
        public Usuarios Get(string nome, string email, string senha)
        {
            var users = new List<Usuarios>();
            users.Add(new Usuarios
            {
                Email = "prova@doubleit.com.br",
                Nome = "Candidato",
                Senha = "Prova@DoubleIt21"
            });
            return users.Where(x => x.Nome == nome && x.Email == email && x.Senha == senha).FirstOrDefault();
        }
    }
}
