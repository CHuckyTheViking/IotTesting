using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=WIN20-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=VpiWQr5A3vMxsO2IaH5/rwcw49EPCqEHJhPZFOWZtlw=");

        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();

            InvokeMethod("DeviceApp", "SetTelemetryInterval", "10").GetAwaiter();
            Console.ReadKey();
        }

        static async Task InvokeMethod(string deviceId, string methodName, string payload)
        {
            var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson(payload);

            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());

        }




    }
}
