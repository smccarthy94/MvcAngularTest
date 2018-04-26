using ProgrammingTest.DataObjects.DataModels;
using System.Collections.Generic;

namespace ProgrammingTest.BusinessLogic
{
    /*
     * Intermediate layer for abstracting the data layer from the API client.
     * This layer will handle any business logic before a retreive from the client, and after a save from the client.
     * Other tasks handled here include object data validation.
     * Given more time it would be great to use something like Ninject to bind the data connector into the constructor, then we'd just have an
     * application level config we could leverage if we wanted to swap out to a different implementation of the data layer.
     */

    public interface IManager<T>
        where T : class, IDataObject
    {
        bool IsValid(T toValidate);
        Dictionary<string, string> GetValidationMessages(T toValidate);
        T Create(T toCreate);
        bool Update(T toUpdate);
        List<T> List();
        T Get(int toGet);
        bool Delete(int toDelete);
    }
}
