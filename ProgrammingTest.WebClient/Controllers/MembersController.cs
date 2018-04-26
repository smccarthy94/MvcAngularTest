using ProgrammingTest.BusinessLogic;
using ProgrammingTest.BusinessLogic.Exceptions;
using ProgrammingTest.DataObjects.DataModels;
using System;
using System.Web.Http;

namespace ProgrammingTest.WebClient.Controllers
{
    public class MembersController : ApiController
    {
        private IManager<Member> _manager;
        public MembersController()
        {
            _manager = new MemberManager();
        }

        // GET api/members
        public IHttpActionResult Get()
        {
            return Ok(_manager.List());
        }

        // GET api/members/5
        public IHttpActionResult Get(int id)
        {
            var result = _manager.Get(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/members
        public IHttpActionResult Post(Member value)
        {
            try
            {
                var created = _manager.Create(value);
                if (created != null)
                {
                    return Created(created.Id.ToString(), created);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ValidationException vEx)
            {
                foreach (var err in vEx.ValidationMessages)
                {
                    ModelState.AddModelError(err.Key, err.Value);
                }

                // the client can consume the validation messages this way
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/members/5
        public IHttpActionResult Put(int id, Member value)
        {
            try
            {
                if (_manager.Get(id) == null)
                {
                    return NotFound();
                }

                value.Id = id;

                var updated = _manager.Update(value);
                if (updated == true)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ValidationException vEx)
            {
                foreach (var err in vEx.ValidationMessages)
                {
                    ModelState.AddModelError(err.Key, err.Value);
                }

                // the client can consume the validation messages this way
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/members/5
        public IHttpActionResult Delete(int id)
        {
            var result = _manager.Delete(id);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}