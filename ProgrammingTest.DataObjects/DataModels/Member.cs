using System;

namespace ProgrammingTest.DataObjects.DataModels
{
    // See comments in IDataObject for explanation of the interface

    public class Member : IDataObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
