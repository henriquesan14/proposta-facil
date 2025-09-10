namespace PropostaFacil.Application.Payments.Email
{
    public static class PaymentEmailBuilder
    {
        public static string BuildHtml(string ClientName, decimal Amount, DateOnly PaidDate, string PlanName)
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
                    <p><strong>Data de pagamento:</strong> {PaidDate:dd/MM/yyyy}</p>

                    <p>Obrigado por confiar em nossos serviços!</p>
                    <div class='footer'>
                      <p>Este é um e-mail automático, por favor não responda.</p>
                    </div>
                  </div>
                </body>
                </html>";
        }
    }
}
