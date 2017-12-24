using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DistanceCalculator;
using StationProvider;
using Stations;

namespace DistanceCalculatorApi
{
	[RoutePrefix("api/distanceCalculator")]
	public class DistanceCalculatorController : ApiController
	{
		private readonly IStationDistanceCalculator _distanceCalculator;
		private readonly IStationProvider _stationProvider;

		public DistanceCalculatorController(IStationDistanceCalculator distanceCalculator,
			 IStationProvider stationProvider)
		{
			if (distanceCalculator == null)
			{
				throw new ArgumentNullException(nameof(distanceCalculator));
			}
			if (stationProvider == null)
			{
				throw new ArgumentNullException(nameof(stationProvider));
			}

			_distanceCalculator = distanceCalculator;
			_stationProvider = stationProvider;
		}


		[HttpGet]
		[Route("findStations")]
		[ResponseType(typeof(IEnumerable<IStation>))]
		public HttpResponseMessage FindStations([FromUri]string namePattern)
		{
		    try
		    {
		        var res = _stationProvider.FindStations(namePattern);
		        return Request.CreateResponse(HttpStatusCode.OK, res);
            }
		    catch (Exception ex)
		    {
		        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can't get stations list");
		    }
            
		}


		[HttpGet]
		//[Route("getDistance/{stationNameFrom}/{stationNameTo}")]
		[Route("getDistance")]
		[ResponseType(typeof(double))]
		public HttpResponseMessage GetDistance([FromUri]string stationNameFrom, [FromUri]string stationNameTo)
		{
			IStation stationFrom, stationTo;

			try
			{
				stationFrom = _stationProvider.GetStation(stationNameFrom);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "'Station From' not found");
			}

			try
			{
				stationTo = _stationProvider.GetStation(stationNameTo);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "'Station To' not found");
			}

			try
			{
				var disct = _distanceCalculator.GetDistance(stationFrom, stationTo);

				return Request.CreateResponse(HttpStatusCode.OK, disct);
			}
			catch (Exception)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't calculate distance");
			}
		}

	}
}
