using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAL
{
    public class BaseDAO<TObject> where TObject : class
    {
        protected readonly DbContext _context;

        #region Constructor

        public BaseDAO()
        {
            _context = new SistemaGestionEntities();
        }

        public BaseDAO(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos

        public TObject BuscarPorId(int id)
        {
            return _context.Set<TObject>().Find(id);
        }

        public IEnumerable<TObject> GetAll()
        {
            return _context.Set<TObject>().ToList();
        }

        public int Insertar(TObject entity)
        {
            try
            {
                _context.Set<TObject>().Add(entity);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Actualizar(TObject entity)
        {

            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }



        public int Eliminar(int id)
        {

            _context.Set<TObject>().Remove(this.BuscarPorId(id));
            return _context.SaveChanges();

        }


        #endregion
    }
}