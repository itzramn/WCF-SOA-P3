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
    //   EMPLOYEES   //
    public string GetAllEmployees()
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
                return result;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
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

    public string CreateEmployee(string name, string lastName, string curp, DateTime birthDate, string email, int id, DateTime deliveryDate)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var newEmployee = new CreateEmployeeRequest
            {
                name = name,
                lastName = lastName,
                curp = curp,
                birthDate = birthDate,
                email = email,
                entryDate = DateTime.Now,
                assets = new List<AssetAssigment>
                {
                    new AssetAssigment
                    {
                        id = id,
                        deliveryDate = deliveryDate
                    }
                }
            };

            var json = JsonConvert.SerializeObject(newEmployee);

            var employee = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://localhost:7154/Employees", employee).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Empleado creado exitosamente.";
            }
			else
			{
                return "Error al crear el empleado. Código de estado: " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
    public string DeleteEmployeeId(int employeeId)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.DeleteAsync("https://p2-soa-api.azurewebsites.net/Employees/" +  employeeId).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Se ha eliminado el empleado";
            }
            else
            {
                data = "No se ha podido eliminar al empleado " + response.StatusCode;
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

            if (response.IsSuccessStatusCode)
            {
                return "Acceso correcto";
            }
            else
            {
                data = "Acceso incorrecto";
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

    //   ASSETS   //
    public string GetAllAssets()
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.GetAsync("https://p2-soa-api.azurewebsites.net/Assets").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
    public string CreateAsset(string name, string description)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var newAsset = new CreateAssetRequest
            {
                name = name,
                description = description
            };

            var json = JsonConvert.SerializeObject(newAsset);

            var asset = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Assets", asset).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Se ha creado correctamente el activo";
            }
            else
            {
                data = "No se ha podido crear el activo " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

    public string DeleteAsset(int assetId)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.DeleteAsync("https://p2-soa-api.azurewebsites.net/Assets/" + assetId).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Se ha eliminado el activo";
            }
            else
            {
                data = "No se ha podido eliminar el activo " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
}
