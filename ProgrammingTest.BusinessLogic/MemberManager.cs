using System.Collections.Generic;
using System.Text.RegularExpressions;
using ProgrammingTest.BusinessLogic.Exceptions;
using ProgrammingTest.Data.DataConnectors;
using ProgrammingTest.DataObjects.DataModels;

namespace ProgrammingTest.BusinessLogic
{
    public class MemberManager : IManager<Member>
    {
        private MemberConnector _connector;
        public MemberManager()
        {
            _connector = new MemberConnector();
        }

        /*
         * Intermediate layer for abstracting the data layer from the API client.
         * This layer will handle any business logic before a retreive from the client, and after a save from the client.
         * Other tasks handled here include object data validation.
         * Given more time it would be great to use something like Ninject to bind the data connector into the constructor, then we'd just have an
         * application level config we could leverage if we wanted to swap out to a different implementation of the data layer.
         */

        public Member Create(Member toCreate)
        {
            if (IsValid(toCreate) == true)
            {
                return _connector.Create(toCreate);
            }
            else
            {
                throw new ValidationException(GetValidationMessages(toCreate));
            }
        }

        public bool Delete(int toDelete)
        {
            return _connector.Delete(toDelete);
        }

        public Member Get(int toGet)
        {
            return _connector.Get(toGet);
        }

        public List<Member> List()
        {
            return _connector.List();
        }

        public bool Update(Member toUpdate)
        {
            if (IsValid(toUpdate) == true)
            {
                return _connector.Update(toUpdate);
            }
            else
            {
                throw new ValidationException(GetValidationMessages(toUpdate));
            }
        }

        public bool IsValid(Member toValidate)
        {
            // helper function in cases where a quick inline check is required to determine if a member is valid
            // well at least I think it looks neater in use this way.
            var result = GetValidationMessages(toValidate);

            if (result == null || result.Count <= 0)
            {
                return true;
            }

            return false;
        }

        public Dictionary<string, string> GetValidationMessages(Member toValidate)
        {
            // run validation checks on the input object with regex and such

            Dictionary<string, string> validationMessages = new Dictionary<string, string>();

            if (Regex.IsMatch(toValidate.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") == false)
                validationMessages.Add("Email", "Email is not a valid email address");

            if (string.IsNullOrWhiteSpace(toValidate.FirstName))
                validationMessages.Add("FirstName", "First Name is a required field");

            if (string.IsNullOrWhiteSpace(toValidate.LastName))
                validationMessages.Add("LastName", "Last Name is a required field");

            if (Regex.IsMatch(toValidate.Phone, @"^\d*$") == false)
                validationMessages.Add("Phone", "Phone is not a valid phone number");

            if (Regex.IsMatch(toValidate.WebSite, @"^(http\:\/\/|https\:\/\/)?([a-z0-9][a-z0-9\-]*\.)+[a-z0-9][a-z0-9\-]*\/?$") == false)
                validationMessages.Add("WebSite", "WebSite is not a valid web address");

            return validationMessages;
        }
    }
}
