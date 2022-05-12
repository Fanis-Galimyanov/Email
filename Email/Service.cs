using System.Net;
using System.Net.Mail;

namespace Email
{
    public class Service
    {
        private readonly ILogger<Service> logger;

        public Service(ILogger<Service> logger)
        {
            this.logger = logger;
        }
        
        public void SendEmail()
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("admin@itpark.ru","ITPark");
                message.To.Add("fanisga@yandex.ru");
                message.To.Add("fanisga@mail.ru");
                message.To.Add("fanisga@rambler.ru");
                message.Subject = "Привет из ITPark";

                var plainView = AlternateView.CreateAlternateViewFromString("Текст. Просто текст", null, "text/plain");
                var htmlView = AlternateView.CreateAlternateViewFromString(
                    "Ракета" +
                    "<img src = cid:roket>" +
                    "Корабль" +
                   "<img src = cid:ship>",
                    null, "text/html");

                LinkedResource roket = new LinkedResource("wwwroot/soyuz.jpg");
                LinkedResource ship = new LinkedResource("wwwroot/ship.jpg");

                roket.ContentId = "roket";
                ship.ContentId = "ship";

                htmlView.LinkedResources.Add(roket);
                htmlView.LinkedResources.Add(ship);

                message.AlternateViews.Add(plainView);
                message.AlternateViews.Add(htmlView);

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("soportmedimist@gmail.com","kazan_tatarstan");
                    client.Port =   587;
                    client.EnableSsl = true;
                    
                    client.Send(message);
                    logger.LogInformation("Сообщение отправлено успешно!");
                }

            }
            catch(Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }
      
    }
}
