using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace InStories.Core.Common
{
    public class PingHandler : DelegatingHandler
    {
        private readonly string HOST;
        private readonly int PORT; // Изменили тип на int

        public PingHandler(string host, string port)
        {
            HOST = host;
            PORT = int.Parse(port); // Преобразуем строку в int
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!(await IsPingable(HOST, PORT, cancellationToken)))
            {
                throw new HttpRequestException($"Сервер {HOST} недоступен.");
            }

            // Если сервер доступен, продолжаем выполнение запроса
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<bool> IsPingable(string host, int port, CancellationToken cancellationToken) // Изменили тип порта на int
        {
            using (var client = new TcpClient())
            {
                try
                {
                    var task = client.ConnectAsync(host, port);
                    var result = await Task.WhenAny(task, Task.Delay(500, cancellationToken)); // 0,5 секунды
                    return result == task && client.Connected; // Проверяем, подключен ли клиент
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
