namespace ProgrammingTest.DataObjects.DataModels
{
    /*
     * In a large app we could assume any object that will be written/retreived from a data layer will have some sort of Id,
     * It would be possible to assign one implementation of a dataconnector for multiple types of data object types this way.
     */

    public interface IDataObject
    {
        int Id { get; set; }
    }
}
