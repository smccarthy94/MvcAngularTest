using ProgrammingTest.DataObjects.DataModels;
using System.Collections.Generic;

namespace ProgrammingTest.Data.DataConnectors
{
    /*
    * Data layer, handles the lowest level of data manipulaion and retreival.
    * Given more time I'd probably include something like Ninject to handle how and implementation of a dataconnector is bound into a
    * manager class in the business logic, that way this entire layer can be easily updated without disturbing object validation, or any client functionality.
    */

    public interface IDataConnector<T>
        where T : class, IDataObject
    {
        List<T> List();
        T Get(int toGet);
        bool Update(T toUpdate);
        bool Delete(int toDelete);
        T Create(T toCreate);
    }
}
