using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GpsNote.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<T> GetAsync<T>(string requestUrl, Dictionary<string, string> additioalHeaders = null)
        {
            using var response = await MakeRequestAsync(requestUrl, HttpMethod.Get, null, additioalHeaders).ConfigureAwait(false);

            ThrowIfNotSuccess(response);

            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> PutAsync<T>(string requestUrl, object requestBody, Dictionary<string, string> additioalHeaders = null)
        {
            using var response = await MakeRequestAsync(requestUrl, HttpMethod.Put, requestBody, additioalHeaders).ConfigureAwait(false);

            ThrowIfNotSuccess(response);

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> DeleteAsync<T>(string requestUrl, Dictionary<string, string> additioalHeaders = null)
        {
            using var response = await MakeRequestAsync(requestUrl, HttpMethod.Delete, null, additioalHeaders).ConfigureAwait(false);

            ThrowIfNotSuccess(response);

            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> DeleteAsync<T>(string requestUrl, object requestBody, Dictionary<string, string> additioalHeaders = null)
        {
            using var response = await MakeRequestAsync(requestUrl, HttpMethod.Delete, requestBody, additioalHeaders).ConfigureAwait(false);

            ThrowIfNotSuccess(response);

            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> PostAsync<T>(string requestUrl, object requestBody, Dictionary<string, string> additioalHeaders = null)
        {
            using var response = await MakeRequestAsync(requestUrl, HttpMethod.Post, requestBody, additioalHeaders).ConfigureAwait(false);

            ThrowIfNotSuccess(response);

            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(data);
        }

        private static void ThrowIfNotSuccess(HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal string BuildParametersString(Dictionary<string, string> parameters)
        {
            if (parameters is null || parameters.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder("?");
            bool needAddDivider = false;

            foreach (var item in parameters)
            {
                if (needAddDivider)
                {
                    sb.Append('&');
                }

                var encodedKey = WebUtility.UrlEncode(item.Key);
                var encodedVal = WebUtility.UrlEncode(item.Value);

                sb.Append($"{encodedKey}={encodedVal}");

                needAddDivider = true;
            }

            return sb.ToString();
        }

        private async Task<HttpResponseMessage> MakeRequestAsync(string requestUrl, HttpMethod method, object requestBody = null, Dictionary<string, string> additioalHeaders = null)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(method, requestUrl);

            if (requestBody != null)
            {
                var json = JsonConvert.SerializeObject(requestBody);

                if (requestBody is IEnumerable<KeyValuePair<string, string>> body)
                {
                    request.Content = new FormUrlEncodedContent(body);
                }
                else
                {
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            if (additioalHeaders != null)
            {
                foreach (var header in additioalHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return await client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
