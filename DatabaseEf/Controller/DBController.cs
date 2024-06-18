using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEf.Controller
{
    public class DBController
    {
        private readonly DataContext _context;

        public DBController(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> FlushDB()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS = 0");

                // Obtenir toutes les tables de la base de données
                var tableNames = _context.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .Distinct()
                    .ToList();

                // Exécuter la commande TRUNCATE sur chaque table
                foreach (var tableName in tableNames)
                {
                    await _context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE `{tableName}`");
                }

                // Réactive la vérification des contraintes de clé étrangère
                await _context.Database.ExecuteSqlRawAsync("SET FOREIGN_KEY_CHECKS = 1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
