﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MVVMModalDialogDemo.Data;

namespace MVVMModalDialogDemo.DataService
{
    public interface IDataService
    {
        Task<List<Person>> GetPeople();
        Task<Person> GetPersonById(int id);
        Task<Person> NewPerson();
        bool HasChanges();
        void RollBack();
        Task SaveChanges();
    }
}