namespace PropostaFacil.Infra.Emails.Builders
{
    public static class PaymentEmailBuilder
    {
        public static string BuildConfirmPayment(string ClientName, decimal Amount, DateOnly? PaymentDate, string PlanName)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='pt-BR'>
                <head>
                  <meta charset='UTF-8'>
                  <title>Confirmação de Pagamento</title>
                  <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f8f9fa;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background: #ffffff;
                        border-radius: 8px;
                        box-shadow: 0 2px 8px rgba(0,0,0,0.1);
                        padding: 24px;
                    }}
                    h2 {{
                        color: #2e7d32;
                    }}
                    p {{
                        font-size: 15px;
                        color: #333;
                    }}
                    .footer {{
                        margin-top: 20px;
                        font-size: 12px;
                        color: #888;
                    }}
                  </style>
                </head>
                <body>
                  <div class='container'>
                    <h2>Pagamento Confirmado ✅</h2>
                    <p>Olá <strong>{ClientName}</strong>,</p>
                    <p>Recebemos seu pagamento do plano <strong>{PlanName}</strong>.</p>
                    <p><strong>Valor:</strong> R$ {Amount:F2}</p>
                    <p><strong>Data de pagamento:</strong> {PaymentDate:dd/MM/yyyy}</p>

                    <p>Obrigado por confiar em nossos serviços!</p>
                    <div class='footer'>
                      <p>Este é um e-mail automático, por favor não responda.</p>
                    </div>
                  </div>
                </body>
                </html>";
        }

        public static string BuildPaymentCreated(string name, string paymentLink, decimal value, DateOnly dueDate)
        {
            return $@"
            <!DOCTYPE html>
            <html>
              <head>
                <meta charset=""UTF-8"">
                <title>Pagamento da sua assinatura</title>
              </head>
              <body style=""font-family: Arial, sans-serif; line-height:1.5; color:#333;"">
                <h2>Olá, {name}!</h2>
                <p>Foi gerada uma cobrança referente à sua assinatura.</p>
                <p><strong>Valor:</strong> R$ {value:F2}<br>
                   <strong>Vencimento:</strong> {dueDate:dd/MM/yyyy}</p>
                <p>Você pode acessar o boleto ou realizar o pagamento através do link abaixo:</p>
                <p>
                  <a href=""{paymentLink}"" style=""display:inline-block; padding:12px 24px; color:#fff; background-color:#28a745; text-decoration:none; border-radius:5px;"">
                    Pagar Agora
                  </a>
                </p>
                <p>Se você já realizou o pagamento, por favor desconsidere este e-mail.</p>
                <hr>
                <p style=""font-size:12px; color:#999;"">&copy; {DateTime.UtcNow.Year} Seu Sistema</p>
              </body>
            </html>";
        }

        public static string BuildPaymentOverdue(string name, string paymentLink, decimal value, DateOnly dueDate)
        {
            return $@"
            <!DOCTYPE html>
            <html>
              <head>
                <meta charset=""UTF-8"">
                <title>Fatura vencida da sua assinatura</title>
              </head>
              <body style=""font-family: Arial, sans-serif; line-height:1.5; color:#333;"">
                <h2>Olá, {name}!</h2>
                <p>Identificamos que sua fatura da assinatura está <strong>vencida</strong>.</p>
                <p><strong>Valor:</strong> R$ {value:F2}<br>
                   <strong>Data de vencimento:</strong> {dueDate:dd/MM/yyyy}</p>
                <p>Para evitar a suspensão da sua assinatura, regularize o pagamento através do link abaixo:</p>
                <p>
                  <a href=""{paymentLink}"" style=""display:inline-block; padding:12px 24px; color:#fff; background-color:#dc3545; text-decoration:none; border-radius:5px;"">
                    Pagar Agora
                  </a>
                </p>
                <p>Se você já realizou o pagamento, por favor desconsidere este aviso.</p>
                <hr>
                <p style=""font-size:12px; color:#999;"">&copy; {DateTime.Now.Year} Seu Sistema</p>
              </body>
            </html>";
        }
    }
}
