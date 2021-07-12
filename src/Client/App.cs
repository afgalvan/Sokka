using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.Extensions.Hosting;
using TcpClient = NetCoreServer.TcpClient;

namespace Client
{
    public class App : TcpClient, IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private          bool                     _stop;


        public App(SocketSetting config, IHostApplicationLifetime appLifetime) :
            base(config.Host, config.Port)
        {
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () =>
                await Task.Run(ConnectToServer, cancellationToken)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ConnectToServer()
        {
            // Connect the client
            Console.Write("Client connecting...");
            ConnectAsync();
            Console.WriteLine("Done!");

            Console.WriteLine(
                "Press Enter to stop the client or '!' to reconnect the client...");

            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Disconnect the client
                if (line == "!")
                {
                    Console.Write("Client disconnecting...");
                    DisconnectAsync();
                    Console.WriteLine("Done!");
                    continue;
                }

                // Send the entered text to the chat server
                SendAsync(line);
            }

            // Disconnect the client
            Console.Write("Client disconnecting...");
            DisconnectAndStop();
            Console.WriteLine("Done!");
        }


        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Console.WriteLine(
                $"Chat TCP client connected a new session with Id {Id}");
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine(
                $"Chat TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset,
            long size)
        {
            Console.WriteLine(
                Encoding.UTF8.GetString(buffer, (int) offset, (int) size));
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine(
                $"Chat TCP client caught an error with code {error}");
        }
    }
}
