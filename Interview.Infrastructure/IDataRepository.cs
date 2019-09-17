using Interview.Domain;
using System;
using System.Collections.Generic;

namespace Interview.Infrastructure
{
    public interface IDataRepository
    {
        IEnumerable<Data> GetAll();
        void Delete(Guid id);
        Data GetOne(Guid id);
        void Add(Data data);
    }
}