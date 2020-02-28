using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChildrenHelper.DataBase;
using ChildrenHelper.Models;
using ChildrenHelper.Services.Contracts.Interfaces;
using ChildrenHelper.Services.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChildrenHelper.Services
{
    public class ChildrenServices : IChildrenService
    {
        public DataBaseContext db;

        public ChildrenServices(DataBaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> Create(ChildrenModel childrenModel)
        {

            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Chsurname == childrenModel.Chsurname);
            if (children == null)
            {
                db.Childrens.Add(new Children
                {
                    Chname = childrenModel.Chname, Chsurname = childrenModel.Chsurname,
                    Chsecname = childrenModel.Chsecname, DiagnozID = childrenModel.DiagnozID,
                    Chdesc = childrenModel.Chdesc, Summa = childrenModel.Summa
                });
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Edit(ChildrenModel childrenModel)
        {
            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == childrenModel.Id);
            if (children != null)
            {
                db.Childrens.Update(new Children
                {
                    Chname = childrenModel.Chname, Chsurname = childrenModel.Chsurname,
                    Chsecname = childrenModel.Chsecname, DiagnozID = childrenModel.DiagnozID,
                    Chdesc = childrenModel.Chdesc, Summa = childrenModel.Summa
                });
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> ConfirmDelete(int? Id)
        {
            if (Id != null)
            {


                var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == Id);

                return true;
            }
            else
            {
                return false;
            }
        }
    

    public async Task<bool> Delete(ChildrenModel childrenModel)
        {

            var children = await db.Childrens.FirstOrDefaultAsync(u => u.Id == childrenModel.Id);
                if (children != null)
                {
                    db.Childrens.Remove(children);
                    await db.SaveChangesAsync();
                    return true;
                }
            
            else
            {
                return false;
            }
        }
        public IEnumerable<Children> GetFilteredChildrens(int id, string searchQuery)
        {

            if (string.IsNullOrEmpty(searchQuery) || string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetChildrensByDiagnozId(id);
            }

            return GetFilteredChildrens(searchQuery).Where(children => children.Diagnoz.Id == id);
        }
        public IEnumerable<Children> GetChildrensByDiagnozId(int diagnozId)
        {
            return GetAll().Where(children => children.Diagnoz.Id == diagnozId);
        }

        public IEnumerable<Children> GetFilteredChildrens(string searchQuery)
        {
            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower().Split(' ');
            if (queries == null)
            {
                return GetPreferred(10);
            }

            return GetAll().Where(item => queries.Any(query => (item.Chname.ToLower().Contains(query))));
        }
        public IEnumerable<Children> GetAll()
        {
            return db.Childrens
                .Include(u => u.Diagnoz);
        }
        public IEnumerable<Children> GetPreferred(int count)
        {
            return GetAll().OrderByDescending(children => children.Id).Where(children => children.Chname != null).Take(count);
        }
    }
}
