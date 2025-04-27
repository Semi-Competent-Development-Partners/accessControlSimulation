using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serverSide {
    internal class ServerThread {
        // underscore can be used to indicate private variables
        private Socket _socket;
        private int _value;
        private Thread _thread;
        private DbHandler _dbHandler;

        public ServerThread(Socket socket, int value) {
            this._socket = socket;
            this._value = value;
            this._thread = new Thread(Run);
            this._dbHandler = new DbHandler(); // Initialize the database handler

            //this._thread.IsBackground = true; // Set the thread as a background thread

            this._thread.Start(); // Start the thread
        }

        private void Run() {
            // This is the main loop of the thread.
            while (true) {
                // Accept a connection from a client.
                //Socket client = _socket.Accept();

                try {
                    // Await for the client to send a message.
                    var buffer = new byte[1024];
                    var received = _socket.Receive(buffer);
                    var message = Encoding.UTF8.GetString(buffer, 0, received);

                    if (message == "stop") {
                        // Stop the thread if the message is "stop".
                        Console.WriteLine($"Socket server thread {_value} stopping.");
                        _socket.Shutdown(SocketShutdown.Both);
                        _socket.Close();
                        break;
                    }

                    // TODO: Handle the message received from the client.
                    if (!HandleMessage(message))
                        continue;

                    Console.WriteLine($"server-thread-{_value}: \"{message}\"");
                }
                catch (Exception e) {
                    // Handle exceptions (e.g., client disconnected)
                    Console.WriteLine($"Socket server thread {_value} exception: {e.Message}");
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                    break;
                }
            }
        }

        private bool HandleMessage(string message) {
            bool msgIsValid = ValidateMessage(message);
            if (!msgIsValid) {
                Console.WriteLine($"Socket server thread {_value} sent invalid message: \"{message}\"");
                return false;
            }

            // Send the data to the db handler
            if (this._dbHandler.AddEntry(message) == 1)
                Console.WriteLine($"Socket server thread {_value} added entry to db: \"{message}\"");
            else
                Console.WriteLine($"Socket server thread {_value} failed to add entry to db: \"{message}\"");

            return true;
        }

        //TODO: create a message utility class which the client and server can share instead of duplicating the message validation code
        private static bool ValidateMessage(string message) {
            string[] validMsgs = { "in", "out" };

            if (message.Split(' ').Length != 2 | !validMsgs.Contains(message.Split(' ')[0]))
                return false;

            // out into 'discard' variable (underscore)
            return int.TryParse(message.Split(' ')[1], out int _);
        }
    }
}
