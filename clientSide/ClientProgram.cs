using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO.Ports;


namespace clientSide {
    internal class ClientProgram {
        private static SerialPort? serialPort;
        private static Socket? socket;
        private static bool exitRequested = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0210:Convert to top-level statements", Justification = "<Pending>")]
        static void Main(string[] args) {
            // Register cleanup handlers
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true; // Prevent immediate process termination
                exitRequested = true;
                Cleanup();
            };
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Cleanup();

            try {
                serialPort = new SerialPort("COM3", 9600);
                serialPort.Open();

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Loopback, 9000);

                while (true) {
                    try {
                        socket.Connect(ipEndPoint);
                        break;
                    }
                    catch (SocketException) {
                        Thread.Sleep(1000);
                    }
                }

                Console.WriteLine($"Socket client connected to {ipEndPoint}");

                string message;
                do {
                    if (exitRequested) break;

                    message = serialPort.ReadLine()?.Trim();

                    //if (!ValidateMessage(message)) {
                    //    Console.WriteLine($"Invalid message from serial: {message}");
                    //    continue;
                    //}

                    Console.WriteLine($"Sending message: {message}");
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    socket.Send(buffer);

                } while (message != "stop" && !exitRequested);
            }
            finally {
                Cleanup();
            }
        }

        private static void Cleanup() {
            if (socket != null) {
                try { socket.Shutdown(SocketShutdown.Both); } catch { }
                try { socket.Close(); } catch { }
                socket = null;
            }
            if (serialPort != null && serialPort.IsOpen) {
                try { serialPort.Close(); } catch { }
                serialPort = null;
            }
        }

        private static bool ValidateMessage(string message) {
            string[] validMsgs = { "in", "out" };

            if (string.IsNullOrWhiteSpace(message))
                return false;

            var parts = message.Split(' ');
            if (parts.Length != 2 || !validMsgs.Contains(parts[0]))
                return false;

            return int.TryParse(parts[1], out int _);
        }
    }
}