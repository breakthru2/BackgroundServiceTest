using BgService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgService.Models.Table
{
    public class UpdateDatable : IUpdateDatable
    {
        private readonly BgServiceDbContext _context;

        public UpdateDatable(BgServiceDbContext context)
        {
            _context = context;
        }

         public async Task RunUpdate()
        {
            var query = await _context.SimpleTable.Where(s => s.Id == 1).FirstOrDefaultAsync();

            //var counter = 1 ;


            query.Value += 1;

            _context.SimpleTable.Update(query);

            await _context.SaveChangesAsync();
        }

        public async Task RunCSVUpdate()
        {

        }
    }
}
