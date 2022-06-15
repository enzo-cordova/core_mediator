using Genzai.WebCore.Integration.Test.Mock.Http;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Genzai.WebCore.Integration.Test.Common
{
    public struct Endpoint
    {
        private readonly string? url = null;

        public Endpoint(string url)
        {
            this.url = url;
        }

        public new string ToString() { return this.url ?? ""; }

        public string Url => this.ToString();
    }

    public class BaseCrudFeature : BaseFeature
    {
        public BaseCrudFeature(ITestOutputHelper output) : base(output)
        {
        }

        public static void CheckDeleteList(IList<HttpStatusCode> deleteCodes)
        {
            foreach (HttpStatusCode deleteCode in deleteCodes)
            {
                Assert.Equal(HttpStatusCode.OK, deleteCode);
            }
        }

        public static IDictionary<string, object?> convertDatatableToDictionary(Table dataTable)
        {
            return ConvertDataTableWithNameSuffixToDictionary(dataTable);
        }

        /// <summary>
        /// Converts a Jerkin Data Table into a JSON parseable Dictionary, adding
        /// to the value of the indicated fields (columns) the given suffix.
        ///
        /// If any of field or suffix are a null, empty or only-whitespaces-filled
        /// string, no suffix will be added.
        ///
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="suffix"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IDictionary<string, object?> ConvertDataTableWithNameSuffixToDictionary(Table dataTable, string suffix, string[] fields)
        {
            dynamic flexible = new ExpandoObject();
            IDictionary<string, object?> dictionary = (IDictionary<string, object?>)flexible;
            bool modifySequentialField =
                fields.Length > 0
                && !string.IsNullOrEmpty(suffix);
            foreach (TableRow row in dataTable.Rows)
            {
                string value = row[1];
                if (modifySequentialField)
                {
                    foreach (string f in fields!)
                    {
                        if (f != null && f.Trim().Length > 0 && row[0].Equals(f))
                        {
                            value = value + suffix;
                            break;
                        }
                    }
                }
                dictionary.Add(row[0], parseDataInput(value));
            }

            return dictionary;
        }

        public static IDictionary<string, object?> ConvertDataTableWithNameSuffixToDictionary(Table dataTable, string suffix = "", string field = "")
        {
            string[]? fieldArray = string.IsNullOrEmpty(field) ? System.Array.Empty<string>() : new string[] { field };
            return ConvertDataTableWithNameSuffixToDictionary(dataTable, suffix, fieldArray);
        }

        /// <summary>
        ///
        /// Converts a Gherkin "value" cell into a JSON-parseable objet.
        ///
        /// If a "value" is a list, it must be surrounded by square
        /// brackets [] an its elements separated by comma.
        ///
        /// </summary>
        /// <param name="rowData"> gherkin data input.</param>
        /// <returns>JSON-parseable objet representing the Gherkin (value) cell.</returns>
        private static object? parseDataInput(string? rowData)
        {
            Regex reMapp = new Regex("{(.*)}");
            Regex reList = new Regex(@"\[(.*)\]");
            if (rowData is not null && reMapp.IsMatch(rowData))
            {
                Match match = reMapp.Match(rowData);
                string expresion = match.Groups[1].Value;
                string[] subs = expresion.Split(',');
                IDictionary<string, object> keyValuePairsInner = new Dictionary<string, object>();
                foreach (string sub in subs)
                {
                    string[] subSubs = sub.Split("#=#");
                    keyValuePairsInner.Add(subSubs[0], subSubs[1]);
                }
                return keyValuePairsInner;
            }

            if (rowData is not null && reList.IsMatch(rowData))
            {
                Match match = reList.Match(rowData);
                string expresion = match.Groups[1].Value;
                string[] subs = expresion.Split(',');
                List<string> list = new();
                foreach (string sub in subs)
                {
                    list.Add(sub.Trim());
                }
                return list;
            }
            return rowData;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Testing", "CA1822", Justification = "Gherkin requires this not to be static.")]
        public string ParseDataTableToJson(Table dataTable)
        {
            return JsonConvert.SerializeObject(convertDatatableToDictionary(dataTable));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Testing", "CA1822", Justification = "Gherkin requires this not to be static.")]
        public string ParseDataTableWithSuffixToJson(Table dataTable, string suffix, string field)
        {
            return JsonConvert.SerializeObject(ConvertDataTableWithNameSuffixToDictionary(dataTable, suffix, field));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Testing", "CA1822", Justification = "Gherkin requires this not to be static.")]
        public string ParseDataTableWithSuffixToJson(Table dataTable, string suffix, string[] fields)
        {
            return JsonConvert.SerializeObject(ConvertDataTableWithNameSuffixToDictionary(dataTable, suffix, fields));
        }

        public static HttpStatusCode GetAll(string endPoint)
        {
            System.Threading.Tasks.Task<HttpResponseMessage> response = GetClient().GetAsync(endPoint);
            response.Wait();
            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();
            return response.Result.StatusCode;
        }

        /// <summary>
        /// The HTTP request is always sent with default-defined user's token,
        /// not with scenario-define one.
        /// </summary>
        public ResponseBean<TObjectDto> Get<TObjectDto>(Endpoint endPoint)
        {
            HttpClient defaultHttpClient = GetClient();
            return this.Get<TObjectDto>(endPoint, defaultHttpClient);
        }

        /// <summary>
        /// The HTTP request is always sent with default-defined user's token,
        /// not with scenario-define one.
        /// </summary>
        public ResponseBean<TObject> Get<TObject>(Endpoint endPoint, string code)
        {
            HttpClient defaultHttpClient = GetClient();
            return this.Get<TObject>(endPoint, defaultHttpClient, code);
        }

        public ResponseBean<TObject> Get<TObject>(Endpoint endPoint, HttpClient client, string code)
        {
            return this.Get<TObject>(
                new Endpoint
                (
                    code == null ? endPoint.Url : endPoint.Url + "/" + code
                ),
                client
            );
        }

        public ResponseBean<TObjectDto> Get<TObjectDto>(Endpoint endPoint, HttpClient client)
        {
            ResponseBean<TObjectDto> responseBean = new ResponseBean<TObjectDto>();
            System.Threading.Tasks.Task<HttpResponseMessage> response = client.GetAsync(endPoint.Url);
            response.Wait();
            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();
            this._output.WriteLine(jsonString.Result);
            try
            {
                responseBean.ReturnObject = JsonConvert.DeserializeObject<TObjectDto>(jsonString.Result)!;
            }
            catch (JsonSerializationException)
            {
                responseBean.ReturnObject = default(TObjectDto);
            }
            responseBean.StatusCode = response.Result.StatusCode;
            return responseBean;
        }

        public ResponseBean<string> Patch(Endpoint endPoint, string serialized)
        {
            HttpClient rootHttpClient = GetClient();
            return this.Patch(endPoint, rootHttpClient, serialized);
        }

        public ResponseBean<string> Patch(Endpoint endPoint, HttpClient client, string serialized)
        {
            using StringContent httpContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            ResponseBean<string> responseBean = new ResponseBean<string>();

            System.Threading.Tasks.Task<HttpResponseMessage> response = client.PatchAsync(endPoint.Url, httpContent);
            response.Wait();

            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();

            this._output.WriteLine(jsonString.Result);

            responseBean.ReturnObject = jsonString.Result;
            responseBean.StatusCode = response.Result.StatusCode;

            return responseBean;
        }

        public ResponseBean<TResponse> Create<TResponse>(Endpoint endPoint, string serialized)
        {
            ResponseBean<TResponse> responseBean = new ResponseBean<TResponse>();
            using StringContent httpContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            System.Threading.Tasks.Task<HttpResponseMessage> response = GetClient().PostAsync(endPoint.Url, httpContent);
            response.Wait();
            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();
            this._output.WriteLine(jsonString.Result);
            responseBean.ReturnObject = JsonConvert.DeserializeObject<TResponse>(jsonString.Result)!;
            responseBean.StatusCode = response.Result.StatusCode;
            return responseBean;
        }


        public ResponseBean<string> Update(Endpoint endPoint, string serialized)
        {
            ResponseBean<string> responseBean = new ResponseBean<string>();
            using StringContent httpContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            System.Threading.Tasks.Task<HttpResponseMessage> response = GetClient().PutAsync(endPoint.Url, httpContent);
            response.Wait();
            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();
            this._output.WriteLine(jsonString.Result);
            responseBean.ReturnObject = jsonString.Result!;
            responseBean.StatusCode = response.Result.StatusCode;
            return responseBean;
        }

        public ResponseBean<TSearchResult> Search<TSearchResult>(Endpoint endPoint, IDictionary<string, object?> values)
        {
            return this.Search<TSearchResult>(endPoint, GetClient(), values);
        }

        public ResponseBean<TSearchResult> Search<TSearchResult>(Endpoint endPoint, HttpClient client, IDictionary<string, object?> values)
        {
            ResponseBean<TSearchResult> responseBean = new ResponseBean<TSearchResult>();

            System.Threading.Tasks.Task<HttpResponseMessage> response = client.GetAsync(endPoint.Url + "?" + GetURL(values));
            response.Wait();
            System.Threading.Tasks.Task<string> jsonString = response.Result.Content.ReadAsStringAsync();
            jsonString.Wait();
            this._output.WriteLine(jsonString.Result);
            responseBean.ReturnObject = JsonConvert.DeserializeObject<TSearchResult>(jsonString.Result)!;
            responseBean.StatusCode = response.Result.StatusCode;
            return responseBean;
        }

        public static HttpStatusCode Delete(Endpoint endPoint, long id)
        {
            string url1 = string.Format("{0}/{1}", endPoint.Url, id);

            System.Threading.Tasks.Task<HttpResponseMessage> response = GetClient().DeleteAsync(url1);
            response.Wait();

            return response.Result.StatusCode;
        }

        public static void ValidateResponse(object tdto, IDictionary<string, object?> expectedUpdateDtoDictionary)
        {
            IDictionary<string, object> tdtoDictionary = ToDictionary<object>(tdto);
            Assert.True(DictionaryEquals(expectedUpdateDtoDictionary, tdtoDictionary));
        }

        public static bool DictionaryEquals(IDictionary<string, object?> expectedDictionary, IDictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object?> item in expectedDictionary)
            {
                if (item.Value != null && !item.Key.Equals("CopyNull") &&
                    !dictionary[item.Key].Equals(item.Value) && !dictionary[item.Key].ToString()!.Equals(item.Value.ToString()))
                {
                    return false;
                }

            }
            return true;
        }

        public static IDictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            Dictionary<string, TValue> dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json)!;
            return dictionary;
        }

        protected static string GetURL(IDictionary<string, object?> values)
        {
            string url = string.Empty;
            foreach (KeyValuePair<string, object?> item in values)
            {
                url += item.Key + "=" + item.Value;
            }
            return url;
        }
    }
}
