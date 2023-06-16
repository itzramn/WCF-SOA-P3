using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service : IService
{
    //   EMPLOYEES   //
    public string GetEmployees()
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

    public string CreateEmployee(string name, string lastName, string curp, DateTime birthDate, string email, string assets)
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
                assets = assets
            };

            var json = JsonConvert.SerializeObject(newEmployee);

            var employee = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Employees", employee).Result;

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
    public string DeleteEmployee(int employeeId)
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
    public string RemoveAssetFromEmployee(int employeeId, int assetId)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var url = string.Format("https://p2-soa-api.azurewebsites.net/Employees/{0}/Assets/{1}", employeeId, assetId);
            var response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Activo se ha eliminado del empleado";
            }
            else
            {
                data = "El empleado o el activo no existen o no están asociados " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

    public string UpdateEmployee(int employeeId, string name, string lastName, string curp, DateTime birthDate, string email)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var updatedEmployee = new UpdateEmployeeRequest
            {
                name = name,
                lastName = lastName,
                curp = curp,
                birthDate = birthDate,
                email = email,
            };

            var json = JsonConvert.SerializeObject(updatedEmployee);

            var employee = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync(string.Format("https://p2-soa-api.azurewebsites.net/Employees/{0}", employeeId), employee).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Empleado actualizado exitosamente.";
            }
            else
            {
                return "Error al actualizar el empleado. Código de estado: " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
    public string AddAssetToEmployee(string asset)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var json = JsonConvert.SerializeObject(asset);

            var stringContentAsset = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Employees/Assets", stringContentAsset).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Se añadió activo exitosamente.";
            }
            else
            {
                return "Error al asignar activo. Código de estado: " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

    //   EMAIL   //
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

    //   AUTH   //
    public string ValidateEmployeeLogin(string email, string password)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var loginEntity = new LoginRequest
            {
                email = email,
                password = password
            };

            var json = JsonConvert.SerializeObject(loginEntity);

            var employee = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://p2-soa-api.azurewebsites.net/Auth", employee).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.StatusCode.ToString();
            }
            else
            {
                data = response.StatusCode.ToString();
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }

    //   ASSETS   //
    public string GetAssets(bool? status)
    {
        string data = "";
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            string url= "https://p2-soa-api.azurewebsites.net/Assets";

            if (status.HasValue)
            {
                url += "?status=" + status.Value;
            }

            var response = client.GetAsync(url).Result;

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
    public string ReleaseAsset(int assetId)
    {
        string data;
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var url = string.Format("https://p2-soa-api.azurewebsites.net/Assets/{0}/release", assetId);
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return "Se ha liberado correctamente el activo";
            }
            else
            {
                data = "No se ha podido liberar el activo " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            data = ex.Message;
        }
        return data;
    }
}
