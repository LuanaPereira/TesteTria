using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TriaAPI.Generics;
using TriaAPI.Interfaces;

namespace TriaAPI.Repositorio
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : BaseEntity
    {
        protected DbContext _entities;

        public Repositorio(DbContext context)
        {
            _entities = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual async Task<T> FindAsync(int id)
        {
            var query = await _entities.Set<T>().FindAsync(id);
            return query;
        }
        public virtual T Find(int id)
        {
            var query = _entities.Set<T>().Find(id);
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            Type typeSrc = entity.GetType();
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                var value = srcProp.GetValue(entity, null);
                if (srcProp.GetSetMethod(true).IsVirtual && value != null && !value.GetType().IsGenericType)
                    try
                    {
                        _entities.Entry(srcProp.GetValue(entity, null)).State = EntityState.Unchanged;
                    }
                    catch (Exception ex)
                    {

                    }
            }
            var register = srcProps.FirstOrDefault(p => p.Name == "DataCriacao");
            if (register != null)
                register.SetValue(entity, DateTime.Now);

            var ativo = srcProps.FirstOrDefault(p => p.Name == "Ativo");
            if (ativo != null)
                ativo.SetValue(entity, true);

            var excluido = srcProps.FirstOrDefault(p => p.Name == "Excluido");
            if (excluido != null)
                excluido.SetValue(entity, false);

            var update = srcProps.FirstOrDefault(p => p.Name == "DataAtualizacao");
            if (update != null)
                update.SetValue(entity, DateTime.Now);

            _entities.Set<T>().Add(entity);
        }

        public virtual async Task AddAndSaveAsync(T entity)
        {
            Add(entity);
            await SaveAsync();
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual async Task EditAndSaveAsync(T entity)
        {
            Edit(entity);
            await SaveAsync();
        }

        public virtual void Edit(T entity)
        {
            Type typeSrc = entity.GetType();
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                var value = srcProp.GetValue(entity, null);
                if (srcProp.GetSetMethod(true).IsVirtual && value != null && !value.GetType().IsGenericType)
                    try
                    {
                        _entities.Entry(srcProp.GetValue(entity, null)).State = EntityState.Unchanged;
                    }
                    catch (Exception ex)
                    {

                    }
            }

            var update = srcProps.FirstOrDefault(p => p.Name == "DataAtualizacao");
            if (update != null)
                update.SetValue(entity, DateTime.Now);

            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
        public virtual async Task SaveAsync()
        {
            await _entities.SaveChangesAsync();
        }
    }
}
