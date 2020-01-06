using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CoreSignalR_ContadorVisitas.Hubs
{
    public class ContadorHub : Hub
    {
        private static int _visitantes = 0;

        // Los metodos ahora son asyncronos con lo cual tienen que tener async y await
        //Notificamos a todos los clientes registrados
        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref _visitantes);
            await Clients.Others.SendAsync("displayMessage", "Nuevo conexión " + Context.ConnectionId + " Total Visistantes: " + _visitantes);
            await Clients.Caller.SendAsync("displayMessage", "Hola, Bienvenido...!!!");
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Interlocked.Decrement(ref _visitantes);

            // Mandamos al cliente lo que recibe el servidor. Notificamos a todos los clientes registrados
            await Clients.All.SendAsync("displayMessage", Context.ConnectionId + " cerro la conexión." + " Total Visitantes: " + _visitantes);
        }

        // Metodo que los clientes pueden invocar para enviar mensajes
        public async Task BroadcastMessage(string message)
        {
            await Clients.All.SendAsync("displayMessage", Context.ConnectionId + ">>" + message);
        }
    }
}

