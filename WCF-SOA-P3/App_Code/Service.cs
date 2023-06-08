using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public string GetDataEmployeeId(int employeeId)
	{
		string data = "";
		try
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();

			var response = client.GetAsync("https://p2-soa-api.azurewebsites.net/Employees").Result;

			if (response.IsSuccessStatusCode)
			{
				var result = response.Content.ReadAsStringAsync().Result;
				List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(result);
				var employee = employees.FirstOrDefault(x => x.employeeId == employeeId);
				data = JsonConvert.SerializeObject(employee);
			}
		} catch (Exception ex)
		{
			data = ex.Message;
		}
		return data;
	}
}
