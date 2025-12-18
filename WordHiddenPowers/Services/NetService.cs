// Ignore Spelling: uri

using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


namespace WordHiddenPowers.Services
{
	internal static class NetService
	{
		public static bool CheckHostByPing(string hostNameOrAddress, int timeout = 1000)
		{
			Task<bool> task = CheckHostByPingAsync(hostNameOrAddress: hostNameOrAddress, timeout: timeout);
			task.Wait();
			return task.Result;
		}

		public static async Task<bool> CheckHostByPingAsync(string hostNameOrAddress, int timeout = 1000)
		{
			Ping ping = new Ping();
			try
			{
				PingReply reply = await ping.SendPingAsync(hostNameOrAddress, timeout);
				return reply.Status == IPStatus.Success;
			}
			catch (PingException)
			{
				return false;
			}
		}

		public static bool CheckHostByHttp(Uri uri, double timeout = 10)
		{
			Task<bool> task = CheckHostByHttpAsync(uri: uri, timeout: timeout);
			task.Wait();
			return task.Result;			
		}

		public static async Task<bool> CheckHostByHttpAsync(Uri uri, double timeout = 10)
		{
			HttpClient client = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(timeout)
			};
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				return response.StatusCode == System.Net.HttpStatusCode.OK;
			}
			catch (HttpRequestException)
			{
				return false;
			}
			catch (TaskCanceledException)
			{
				return false;
			}
		}
	}
}
