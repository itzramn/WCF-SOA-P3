using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service : IService
{

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

    public void CreateEmployee(string name, string lastName, string curp, DateTime birthDate, string email, int id, DateTime deliveryDate)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var assets = new
            {
                Id = id,
                DeliveryDate = deliveryDate
            };

            var employeeData = new
            {
                Name = name,
                LastName = lastName,
                Curp = curp,
                BirthDate = birthDate,
                Email = email,
                EntryDate = DateTime.Now,
                Assets = new[] { assets }

            };

            var newEmployee = new StringContent(JsonConvert.SerializeObject(employeeData), Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Employees", newEmployee).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Empleado creado exitosamente.");
            }
			else
			{
                Console.WriteLine("Error al crear el empleado. Código de estado: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
    }
    public string DeleteEmployeeId(int employeeId)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.DeleteAsync("https://p2-soa-api.azurewebsites.net/Employees/" +  employeeId).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Employee deleted successfully.";
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
}
