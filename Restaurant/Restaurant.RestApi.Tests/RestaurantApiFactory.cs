using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Restaurant.RestApi.Tests
{
    public class RestaurantApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IReservationsRepository>();
                services.AddSingleton<IReservationsRepository>(new FakeDatabase());
            });
        }

        internal async Task<HttpResponseMessage> PostReservation(object reservation)
        {
            var client = this.CreateClient();

            string json = JsonSerializer.Serialize(reservation);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("reservations", content);
        }
    }
}
