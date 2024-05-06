﻿using ChatService.DataService;
using ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SharedDB _shared;

        public ChatHub(SharedDB shared) => _shared = shared;

        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.
                SendAsync("ReceiveMessage","admin",$"{conn.UserName} has joined");
        }
        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            _shared.connections[Context.ConnectionId] = conn;

            await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.UserName} has joined {conn.ChatRoom}");
        }
        public async Task SendMessage(string msg)
        {
            if(_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Groups(conn.ChatRoom).SendAsync("ReceiveSpecificMessage", conn.UserName, msg);
            }
            {
                string a = "asdasd";
            }
        }
    }
}
