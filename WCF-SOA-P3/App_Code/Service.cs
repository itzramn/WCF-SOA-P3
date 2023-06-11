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

    public void CreateEmployee(CreateEmployeeRequest request)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var newEmployee = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://localhost:7154/Employees", newEmployee).Result;

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

    public string SendEmailReminders()
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.GetAsync("https://localhost:7154/Email").Result;

            if (response.IsSuccessStatusCode)
            {
                return "Emails Enviados";
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }

        return data;
    }

    public string ValidateEmployeeLogin(LoginRequest request)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            LoginRequest loginEntity = new LoginRequest
            {
                email = request.email,
                password = request.password
            };

            var json = JsonConvert.SerializeObject(loginEntity);

            var employee = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Auth", employee).Result;

            if(response.IsSuccessStatusCode)
            {
                return "Acceso correcto";
            } else
            {
                data = "Acceso incorrecto";
            }
        } catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

}
