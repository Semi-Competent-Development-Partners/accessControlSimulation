using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace serverSide {
    public class ServerProgram {
        public const int TCP_PORT = 9000;

        private static void Main(string[] args) {
            DbHandler.OpenConnection(); // Open the static database connection

            int clientCount = 0;
            // Create a new socket.
            Socket socket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);

            // Bind the socket to the local endpoint.
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, TCP_PORT);
            socket.Bind(ipEndPoint);

            // Start listening for incoming connections.
            socket.Listen(10);
            Console.WriteLine($"Socket server listening on {ipEndPoint}");

            // on ctrl +c, stop the server
            Console.CancelKeyPress += (sender, e) => {
                Console.WriteLine("Stopping server...");
                socket.Close();
                // Close the static database connection when the server stops
                DbHandler.CloseConnection();
                Environment.Exit(0);
            };

            while (true) {
                // Accept a connection from a client.
                Socket client = socket.Accept();
                Console.WriteLine($"Socket server accepted connection from {client.RemoteEndPoint}");

                // Create a new thread to handle the client. (Thread starts in the constructor)
                ServerThread serverThread = new ServerThread(client, clientCount++);
                
            }
        }
    }
}