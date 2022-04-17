using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationsApi.Services
{
    public class ReservationService : IReservation
    {
        private ApiDbContext dbContext;
        public ReservationService()
        {
            dbContext = new ApiDbContext();
        }
        public async Task<List<Reservation>> GetReservations()
        {
            string connectionString = "Endpoint=sb://cartestdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=U2QZx6roloUKjWHhuxaQIqYsJkz9/vtS521/9b9peu4=";
            string queueName = "azureorderqueue";
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);
            if (receivedMessages == null)
            {
                return null;
            }

            foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
            {
                string body = receivedMessage.Body.ToString();
                var messageCreated = JsonConvert.DeserializeObject<Reservation>(body);
                await dbContext.Reservations.AddAsync(messageCreated);
                await dbContext.SaveChangesAsync();
                await receiver.CompleteMessageAsync(receivedMessage);
            }
            return await dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            var reservationResult = await dbContext.Reservations.FindAsync(id);
            if (reservationResult != null && reservationResult.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.live.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("scpandml@gmail.com", "Scp&ml100"),
                    EnableSsl = true,
                };
                smtpClient.Send("scpandml@gmail.com", reservationResult.Email, "Car Test Drive", "Your test drive is reserved");
                reservationResult.IsMailSent = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
