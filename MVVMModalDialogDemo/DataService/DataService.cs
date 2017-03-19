using MVVMModalDialogDemo.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MVVMModalDialogDemo.DataService
{
    public class DataService : IDisposable, IDataService
    {
        private DemoDbEntities _Context;

        public DataService()
        {
            _Context = new DemoDbEntities();
        }

        ~DataService()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free managed resources
                if (_Context != null)
                {
                    _Context.Dispose();
                    _Context = null;
                }
            }

            // Free any native resources here if there are any
        }

        public async Task<List<Person>> GetPeople()
        {
            return await Task.Run(() => _Context.People.ToList());
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await Task.Run(() => _Context.People.Where(o => o.Id == id).FirstOrDefault());
        }

        public async Task<Person> NewPerson()
        {
            return await Task.Run(() =>
            {
                Person person = new Person();
                _Context.People.Add(person);

                return person;
            });
        }

        public bool HasChanges()
        {
            return _Context.ChangeTracker.HasChanges();
        }

        public void RollBack()
        {
            var changedEntities = _Context.ChangeTracker.Entries()
                .Where(o => o.State != EntityState.Unchanged).ToList();

            foreach (var entity in changedEntities)
            {
                switch (entity.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Added:
                        entity.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entity.CurrentValues.SetValues(entity.OriginalValues);
                        entity.State = EntityState.Unchanged;
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task SaveChanges()
        {
            await Task.Run(() => _Context.SaveChanges());
        }
    }
}
