namespace PropostaFacil.Infra.Emails.Builders
{
    public static class SubscriptionEmailBuilder
    { 
        public static string BuildConfirmSubscription(string customerName, string planName, decimal price, string paymentLink)
        {
            return $@"
            <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <h2>Olá, {customerName}!</h2>
                    <p>Sua assinatura foi criada com sucesso 🎉</p>

                    <h3>Detalhes da assinatura</h3>
                    <ul>
                        <li><strong>Plano:</strong> {planName}</li>
                        <li><strong>Valor:</strong> R$ {price:N2}</li>
                        <li><strong>Status:</strong> Pendente</li>
                    </ul>

                    <p>Para ativar seu plano, finalize o pagamento no link abaixo:</p>

                    <p>
                        <a href='{paymentLink}' target='_blank' 
                           style='display:inline-block; padding:10px 20px; background:#007bff; 
                                  color:white; text-decoration:none; border-radius:5px;'>
                           Pagar agora
                        </a>
                    </p>

                    <br />
                    <p>Se tiver dúvidas, entre em contato com nosso suporte.</p>
                    <p>Obrigado por escolher nossos serviços!</p>
                </body>
            </html>";
        }
    }
}
