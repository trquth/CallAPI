using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SSC.Core.Api.Models.SSC;
using SSC.StudyRecords.Common.Util;
using SSC.Core.Api.Models.Entity;
using Newtonsoft.Json.Linq;
namespace SSC.StudyRecords.Common
{
	public class WebApiHelper
	{

		/// <summary>
		/// Lấy Các đối tượng từ Core gọi qua API, lấy theo danh sách (List).
		/// </summary>
		/// <typeparam name="T">Class Name</typeparam>
		/// <param name="apiController">string - bao gồm tên Controller/tên hàm</param>
		/// <param name="request">tùy thuộc vào apiController mà truyền request cho phù hợp</param>
		/// <returns>List of T Object</returns>
		public static List<T> GetListCoreObject<T>(string apiController, TheSscRequest request)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			if (!apiController.StartsWith("/"))
				apiController = "/" + apiController;
			if (!apiController.EndsWith("/"))
				apiController = apiController + "/";

			var apiUrl = string.Format("{0}" + apiController, ConfigurationManager.AppSettings.Get("SSCCoreApiUrl"));
			HttpResponseMessage response = null;
			try
			{
				response = client.PostAsJsonAsync(apiUrl, request).Result;
			}
			catch (Exception ex)
			{
				//// Loi khong nhan duoc Response from Core
				return new List<T> { default(T) };
			}
			return CheckCoreResponseList<T>(response);
		}

		public static List<T> CheckCoreResponseList<T>(HttpResponseMessage httpResponse)
		{
			var coreResponse = httpResponse.Content.ReadAsAsync<TheSscResponse>().Result;
			return coreResponse == null
				? new List<T>()
				: (coreResponse.ResponseCode != TheSscResultCode.Success
					? new List<T>()
					 : coreResponse.GetResponseList<T>());
		}


		/// <summary>
		/// Lấy một đối tượng từ Core gọi qua API.
		/// </summary>
		/// <typeparam name="T">Class Name</typeparam>
		/// <param name="apiController">string - bao gồm tên Controller/tên hàm</param>
		/// <param name="request">tùy thuộc vào apiController mà truyền request cho phù hợp</param>
		/// <returns>T Object</returns>
		public static T GetCoreObject<T>(string apiController, TheSscRequest request)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			if (!apiController.StartsWith("/"))
				apiController = "/" + apiController;
			if (!apiController.EndsWith("/"))
				apiController = apiController + "/";

			var apiUrl = string.Format("{0}" + apiController, ConfigurationManager.AppSettings.Get("SSCCoreApiUrl"));
			HttpResponseMessage response = null;
			try
			{
				response = client.PostAsJsonAsync(apiUrl, request).Result;
			}
			catch (Exception ex)
			{
				//// Loi khong nhan duoc Response from Core
				return default(T);
			}
			return CheckCoreResponse<T>(response);
		}

		public static T CheckCoreResponse<T>(HttpResponseMessage httpResponse)
		{
			var coreResponse = httpResponse.Content.ReadAsAsync<TheSscResponse>().Result;
			if (coreResponse == null) return default(T);
			else if (coreResponse.ResponseCode != TheSscResultCode.Success) return default(T);
			else return coreResponse.GetResponse<T>(0);
		}


		public static TResult SscCoreRequest<TResult>(string apiController, TheSscRequest request)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				if (!apiController.StartsWith("/"))
					apiController = "/" + apiController;
				if (!apiController.EndsWith("/"))
					apiController = apiController + "/";

				// Get uri API:
				var api_uri = string.Format("{0}" + apiController, ConfigurationManager.AppSettings.Get("SSCCoreApiUrl"));
				var response = client.PostAsJsonAsync(api_uri, request).Result;
				if (response.IsSuccessStatusCode)
				{
					var coreResponse = response.Content.ReadAsAsync<TheSscResponse>().Result;
					return coreResponse.GetResponse<TResult>(0);
				}
			}
			catch (Exception ex)
			{
			}
			return default(TResult);
		}


		public static TheSscResponse SscCoreRequest(string apiController, TheSscRequest request)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				if (!apiController.StartsWith("/"))
					apiController = "/" + apiController;
				if (!apiController.EndsWith("/"))
					apiController = apiController + "/";

				// Get uri API:
				var api_uri = string.Format("{0}" + apiController, ConfigurationManager.AppSettings.Get("SSCCoreApiUrl"));
				var response = client.PostAsJsonAsync(api_uri, request).Result;
				if (response.IsSuccessStatusCode)
				{
					var coreResponse = response.Content.ReadAsAsync<TheSscResponse>().Result;
					return coreResponse;
				}
			}
			catch (Exception ex)
			{
			}
			return default(TheSscResponse);
		}
 
	}
}