<Query Kind="Program">
  <Connection>
    <ID>97e144d2-b6cd-49a5-ad38-a673b1b21ba7</ID>
    <Persist>true</Persist>
    <Server>KONSULE-SG2EPLI</Server>
    <Database>MyFastDB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="Hulk\Hulk\bin\Debug\Hulk.dll">C:\ndcoslo\Hulk\Hulk\bin\Debug\Hulk.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.OwinSelfHost</NuGetReference>
  <Namespace>Hulk</Namespace>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web.Http</Namespace>
</Query>

async void Main()
{
	// Starting Server
	string baseAddress = "http://localhost:8090/";
	using (var app = WebApp.Start<Startup>(url: baseAddress))
	{
		"Server running".Dump();
		Console.ReadLine();
	}
}
	
public class FastController : ApiController
{
   [Route("")]
   public HttpResponseMessage GetResult()
   {
   	   var fs = new FileStream(@"C:\ndcoslo\SwissProt.xml", FileMode.Open,FileAccess.Read);
       var response = new HttpResponseMessage(HttpStatusCode.OK)
       {
           Content = new StreamContent(fs)
       };
       response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
       return response;
   }
}

public class Startup 
{ 
   public void Configuration(IAppBuilder appBuilder) 
   { 
       HttpConfiguration config = new HttpConfiguration();
       config.MapHttpAttributeRoutes();
	   appBuilder.Use<AllCaps>();
       appBuilder.UseWebApi(config); 
   } 
}