using ShopDbAccess.DAL;
using ShopDbAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopDbAccess.Controllers.API
{
    public class MerchandiseAPIController : ApiController
    {
        private IMerchandiseRepository merchandiseRepository = null;

        public MerchandiseAPIController()
        {
            this.merchandiseRepository = new MerchandiseRepository(new ShopContext());
        }
        public MerchandiseAPIController(IMerchandiseRepository repository)
        {
            this.merchandiseRepository = repository;
        }

        /// <summary>
        /// Get all merchandise
        /// </summary>
        /// <returns>IEnumerable of all merchandise</returns>
        [ResponseType(typeof(IEnumerable<Merchandise>))]
        public IHttpActionResult GetAllMerchandise()
        {
            return Ok(merchandiseRepository.GetAllMerchandise());
        }

        /// <summary>
        /// Get single or none merchandise by ID or article
        /// </summary>
        /// <param name="id">optional merchandise id</param>
        /// <param name="article">optional merchandise article</param>
        /// <returns>type of Merchandise</returns>
        [ResponseType(typeof(Merchandise))]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public IHttpActionResult GetMerchandise(int? id, string? article)
        {
            if (id is null)
            {
                if (string.IsNullOrEmpty(article))
                {
                    throw new ArgumentException("No id or article provided", nameof(article));
                }
                Merchandise merchandise1 = merchandiseRepository.GetMerchandiseByArticle(article);
                if (merchandise1 == null)
                {
                    return NotFound();
                }
                return Ok(merchandise1);
            }
            int notNullID = (Int32)id;
            Merchandise merchandise2 = merchandiseRepository.GetMerchandiseByID(notNullID);
            return Ok(merchandise2);
        }

        /// <summary>
        /// Update merchandise
        /// </summary>
        /// <param name="id">Merchandise ID to update</param>
        /// <param name="merchandise">Merchandise body to update with</param>
        /// <returns>type of void</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMerchandise(int id, Merchandise merchandise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != merchandise.ID)
            {
                return BadRequest();
            }
            try
            {
                merchandiseRepository.UpdateMerchandise(merchandise);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (merchandiseRepository.GetMerchandiseByID(id) != null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Create merchandise
        /// </summary>
        /// <param name="merchandise"> Merchandise object to create</param>
        /// <returns>type of Merchandise</returns>
        [ResponseType(typeof(Merchandise))]
        public IHttpActionResult PostMerchandise(Merchandise merchandise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            merchandiseRepository.CreateMerchandise(merchandise);
            return CreatedAtRoute("DefaultApi", new { id = merchandise.ID }, merchandise);
        }

        /// <summary>
        /// Delete merchandise by ID or article
        /// </summary>
        /// <param name="id">merchandise ID to delete</param>
        /// <param name="article">merchandise article to delete</param>
        /// <returns>type of void</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteMerchandise(int? id, string? article)
        {

            if (id is null)
            {
                if (string.IsNullOrEmpty(article))
                {
                    throw new ArgumentException("No id or article provided", nameof(article));
                }
                merchandiseRepository.DeleteMerchandiseByArticle(article);
                return StatusCode(HttpStatusCode.NoContent);
            }
            int notNullID = (Int32)id;
            merchandiseRepository.DeleteMerchandiseByID(notNullID);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}