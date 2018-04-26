using ProgrammingTest.DataObjects.DataModels;
using ProgrammingTest.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingTest.Data.DataConnectors
{
    public static class _memberContainer
    {
        public static List<Member> MemberList { get; set; }
    }

    public class MemberConnector : IDataConnector<Member>
    {

        public MemberConnector()
        {
            if (_memberContainer.MemberList == null)
            {
                _memberContainer.MemberList = new List<Member>();
            }
        }

        /*
         * Data layer, handles the lowest level of data manipulaion and retreival.
         * Given more time I'd probably include something like Ninject to handle how and implementation of a dataconnector is bound into a
         * manager class in the business logic, that way this entire layer can be easily updated without disturbing object validation, or any client functionality.
         */

        public Member Create(Member toCreate)
        {
            // ensure any value passed in to be created is assigned a valid unique Id.
            if (_memberContainer.MemberList.Count <= 0)
            {
                toCreate.Id = 0;
            }
            else
            {
                toCreate.Id = _memberContainer.MemberList.Max(m => m.Id + 1);
            }

            toCreate.CreatedDate = DateTimeOffset.Now;

            try
            {
                _memberContainer.MemberList.Add(toCreate);
            }
            catch (Exception ex)
            {
                // capture the exception message and wrap it in a DataLayerException to be thrown
                throw new DataLayerException(ex, "Create", typeof(Member), toCreate.Id);
            }

            return toCreate;
        }

        public bool Delete(int toDelete)
        {
            var index = _memberContainer.MemberList.FindIndex(m => m.Id == toDelete);
            if (index > -1) // if a match was found, it can be removed
            {
                try
                {
                    _memberContainer.MemberList.RemoveAt(index);
                    return true;
                }
                catch (Exception ex)
                {
                    // capture the exception message and wrap it in a DataLayerException to be thrown
                    throw new DataLayerException(ex, "Delete", typeof(Member), toDelete);
                }
            }
            else
            {
                // if no match was found, throw an error
                throw new DataLayerException(
                    $"Could not delete Member with Id of {toDelete} as the Id could not be found. Please create the record first.",
                    "Update",
                    typeof(Member),
                    toDelete);
            }
        }

        public Member Get(int toGet)
        {
            var index = _memberContainer.MemberList.FindIndex(m => m.Id == toGet);
            if (index > -1) // if a match is found, the object can be returned
            {
                try
                {
                    return _memberContainer.MemberList[index];
                }
                catch (Exception ex)
                {
                    // capture the exception message and wrap it in a DataLayerException to be thrown
                    throw new DataLayerException(ex, "Update", typeof(Member), toGet);
                }
            }
            else
            {
                // if no match was found, throw an error
                throw new DataLayerException(
                    $"Could not retreive Member with Id of {toGet} as the Id could not be found. Please create the record first.",
                    "Update",
                    typeof(Member),
                    toGet);
            }
        }

        public List<Member> List()
        {
            List<Member> toReturn = null;

            try
            {
                toReturn = _memberContainer.MemberList;
            }
            catch (Exception ex)
            {
                // capture the exception message and wrap it in a DataLayerException to be thrown
                throw new DataLayerException(ex, "List", typeof(Member));
            }

            return toReturn;
        }

        public bool Update(Member toUpdate)
        {
            var index = _memberContainer.MemberList.FindIndex(m => m.Id == toUpdate.Id);
            if (index > -1) // if a match is found, the object can be updated
            {
                try
                {
                    _memberContainer.MemberList[index] = toUpdate;
                    return true;
                }
                catch (Exception ex)
                {
                    // capture the exception message and wrap it in a DataLayerException to be thrown
                    throw new DataLayerException(ex, "Update", typeof(Member), toUpdate.Id);
                }
            }
            else
            {
                // if no match was found, throw an error
                throw new DataLayerException(
                    $"Could not update Member with Id of {toUpdate.Id} as the Id could not be found. Please create the record first.",
                    "Update",
                    typeof(Member),
                    toUpdate.Id);
            }
        }
    }
}
