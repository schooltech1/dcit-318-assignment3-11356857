using System.Collections.Generic;

public interface IRepository<T>
{
    void Add(T item);
    List<T> GetAll();
}
