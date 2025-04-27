using System;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace clientSide {
    internal class ClientProgram {

        static void Main(string[] args) {
            // Create a new socket.
            Socket socket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
            // Connect to the server.
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Loopback, 9000);

            // keep trying to connect until the server is up
            while (true) {
                try {
                    socket.Connect(ipEndPoint);
                    break;
                }
                catch (SocketException) {
                    //Console.WriteLine($"Socket client failed to connect to {ipEndPoint}, retrying...");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine($"Socket client connected to {ipEndPoint}");
            // Send a message to the server.
            string message;
            do {

                do { Console.Write("> "); }
                while (!ValidateMessage(message = Console.ReadLine()));
                
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                socket.Send(buffer);
            } while (message != "stop");

            // Close the socket.
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private static bool ValidateMessage(string message) {
            string[] validMsgs = { "in", "out" };

            if (message.Split(' ').Length != 2 | !validMsgs.Contains(message.Split(' ')[0])) 
                return false;

            // out into 'discard' variable (underscore)
            return int.TryParse(message.Split(' ')[1], out int _);
        }
    }
}