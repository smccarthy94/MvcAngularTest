using ProgrammingTest.DataObjects.DataModels;
using System;

namespace ProgrammingTest.Data.Exceptions
{
    public class DataLayerException : Exception
    {
        public DataLayerException(string message, string methodName, Type objectType, int? objectId = null)
        {
            MethodName     = methodName;
            DataObjectType = objectType;
            DataObjectId   = objectId;
            Message        = message;
        }

        public DataLayerException(string message, string methodName, string objectTypeName, int? objectId = null)
        {
            MethodName   = methodName;
            DataObjectId = objectId;
            Message      = message;

            try
            {
                DataObjectType = Type.GetType(objectTypeName);
            }
            catch
            {
                DataObjectType = typeof(IDataObject);
            }
        }

        public DataLayerException(Exception exception, string methodName, Type objectType, int? objectId = null)
        {
            MethodName     = methodName;
            DataObjectType = objectType;
            DataObjectId   = objectId;
            Message        = exception.Message;
        }

        public DataLayerException(Exception exception, string methodName, string objectTypeName, int? objectId = null)
        {
            MethodName   = methodName;
            DataObjectId = objectId;
            Message      = exception.Message;

            try
            {
                DataObjectType = Type.GetType(objectTypeName);
            }
            catch
            {
                DataObjectType = typeof(IDataObject);
            }
        }

        public string MethodName { get; set; }
        public Type DataObjectType { get; set; }
        public int? DataObjectId { get; set; }
        public string Message { get; set; }
    }
}
