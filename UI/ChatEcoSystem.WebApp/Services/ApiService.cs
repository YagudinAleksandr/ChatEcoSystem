using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ChatEcoSystem.WebApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private HubConnection _hubConnection;

        public ApiService(IConfiguration configuration)
        {
            _baseUrl = configuration["ApiGateway:BaseUrl"];
            _httpClient = new HttpClient { BaseAddress = new System.Uri(_baseUrl) };
        }

        // Auth
        public async Task<HttpResponseMessage> LoginAsync(object loginDto)
        {
            return await _httpClient.PostAsJsonAsync("/auth/login", loginDto);
        }

        // Users
        public async Task<HttpResponseMessage> GetAllUsersAsync()
        {
            return await _httpClient.GetAsync("/users/getall");
        }

        // ChatRooms
        public async Task<HttpResponseMessage> GetChatRoomsAsync()
        {
            return await _httpClient.GetAsync("/chatrooms/getuserchatrooms?userId=demo");
        }

        // WebSocket/SignalR
        public async Task ConnectToChatHubAsync(Action<string, string> onMessageReceived)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_baseUrl}/chatHub")
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                onMessageReceived?.Invoke(user, message);
            });

            await _hubConnection.StartAsync();
        }

        public async Task SendMessageAsync(string user, string message)
        {
            if (_hubConnection != null)
                await _hubConnection.InvokeAsync("SendMessage", user, message);
        }

        public async Task DisconnectFromChatHubAsync()
        {
            if (_hubConnection != null)
                await _hubConnection.StopAsync();
        }
    }
} 