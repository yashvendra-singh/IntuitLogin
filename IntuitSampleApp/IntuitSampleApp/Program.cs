using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IntuitSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //using(var client = new HttpClient())
            //{

            //}
            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "client_id", "ABElQaCHPtBPMHsdhsPktgCQgCBITN1EElXsq25nygN7j-test" },
                { "client_secret", "XRudErK1sDcEtwrhSgWwiC905dc3Mp6WufiyqWLs" },
                { "grant_type", "client_credentials" }
            });

            var client = new HttpClient();
            var response = client.SendAsync(request);
            response.Result.EnsureSuccessStatusCode();

            var payload = JObject.Parse(response.Result.Content.ReadAsStringAsync().Result);
            var token = payload.Value<string>("access_token");

            var client2 = new HttpClient();


            var requestMessage = @"{  
                  'metadata' :{ 
                      'payerId' : 'p123',
                      'companyId' : 'c123'
                  },
                  'deliveryOptions': {
                      'mail': 'true'
                  },
                 'payer':{  
                      'metadata': {
                          'id': 'payer1'
                      },
                      'businessName':'Jil Hill',
                      'streetAddress':'2600 Garcia Ave',
                      'city':'Mountain View',
                      'state':'CA',
                      'postalCode':'94043',
                      'phone':'7152321458',
                      'email': 'jilhill@gmail.com',
                      'tin':'23-1345462'
                  },
                  'recipient':{  
                      'metadata': {
                          'id': 'recipient1'
                      },
                      'businessName':'John Doe',
                      'streetAddress':'2500 Garcia Ave',
                      'city':'Mountain View',
                      'state':'CA',
                      'postalCode':'94043',
                      'phone':'5132223457',
                      'email': 'johndoe@yahoo.com',
                      'tin':'22-3334424'
                  },
                  'boxValues':{  
                      'box1':'1000.00',
                      'box2':'2000.00',
                      'box3':'3000.00',
                      'box4':'4000.00',
                      'box5':'5000.00',
                      'box6':'6000.00',
                      'box7':'7000.00',
                      'box8':'8000.00',
                      'box9':'true',
                      'box10':'10000.00',
                      'box13':'13000.00',
                      'box14':'14000.00',
                      'box15a':'15000.00',
                      'box15b':'15500.00',
                      'box16':'16000.00',
                      'box17':'123'
                  }
              }";

            var jarray = JObject.Parse(requestMessage);

            var body = JsonConvert.SerializeObject(jarray);

            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var sampleRequest = new HttpRequestMessage(HttpMethod.Post, "https://formfly.api.intuit.com/v1/forms/form1099Miscs");
            sampleRequest.Content = new StringContent(body);
            var res = client2.SendAsync(sampleRequest);

            Console.WriteLine(res.Result.StatusCode);
            var payload1 = JObject.Parse(res.Result.Content.ReadAsStringAsync().Result);
            
            Console.WriteLine(res.Result.Content.ReadAsStringAsync().Result);
            Console.WriteLine(res.Result.RequestMessage);
        }
    }
}
