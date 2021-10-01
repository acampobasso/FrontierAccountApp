using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FrontierAccountApp
{
	public class ApiCaller
	{

		static ApiCallerInfo ApiCallerInfo;

		public ApiCaller(ApiCallerInfo apiCallerInfo)
		{
			ApiCallerInfo = new ApiCallerInfo
			{
				BaseAddress = apiCallerInfo.BaseAddress
			};
		}

		public async Task<String> CallApi()
		{
			HttpClient httpClient = new HttpClient() { BaseAddress = ApiCallerInfo.BaseAddress };
			HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress, HttpCompletionOption.ResponseContentRead);
			httpResponseMessage.EnsureSuccessStatusCode();
			return httpResponseMessage.Content.ReadAsStringAsync().Result;
		}
	}
}
