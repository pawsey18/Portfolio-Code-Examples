using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneAlpha.WebAPI.Controllers
{
    [RoutePrefix("api/reviews")]
    public class ReviewsController : ApiController
    {
        ReviewService service = new ReviewService();

        /// <summary>
        /// Get the supervisors department to be updated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list/{id}")]
        public IHttpActionResult GetSupervisorsDepartmentToBeUpdated(int id)
        {
            try
            {
                List<ReviewListDTO> employees = service.GetEmployeesDueForReviewForSupervisor(id);

                if (id != 0)
                    employees = employees.Where(a =>  a.HasQuarterReview == "" || a.HasQuarterReview == "False").ToList();

                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }

        /// <summary>
        /// Get employees reviews
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetEmployeePerformanceReviews(int id, int? reviewId = 0)
        {
            try
            {
                List<ShowEmployeePerformanceReviewsDTO> employees = service.ShowEmployeePerformanceReviews(id);

                if (id != 0)
                    employees = employees.ToList();

                if (reviewId != 0)
                    employees = employees.Where(r => r.ReviewId == reviewId).ToList();

                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }

        /// <summary>
        /// Add a review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("create")]
        public IHttpActionResult AddReview(Review review)
        {
            try
            {
                ReviewService newService = new ReviewService();
                if (review == null)
                    return BadRequest();
                review = newService.AddReview(review);

                if (review == null)
                {
                    return Content(HttpStatusCode.BadRequest, review);
                }
                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please " +
                   "contact the system administrator.");
            }
        }

    }
}
