namespace PropostaFacil.Infra.Emails.Builders;

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

    public static string BuildSubscriptionExpired(string customerName, string paymentLink, decimal value, DateOnly dueDate)
    {
        return $@"
        <html>
            <body style='font-family: Arial, sans-serif; color: #333;'>
                <h2>Olá, {customerName}!</h2>
                <p>Sua assinatura expirou 😞</p>

                <h3>Detalhes da fatura</h3>
                <ul>
                    <li><strong>Valor:</strong> R$ {value:N2}</li>
                    <li><strong>Vencimento:</strong> {dueDate:dd/MM/yyyy}</li>
                    <li><strong>Status:</strong> Expirada</li>
                </ul>

                <p>Mas não se preocupe! Você pode reativar sua assinatura realizando o pagamento através do link abaixo:</p>

                <p>
                    <a href='{paymentLink}' target='_blank' 
                       style='display:inline-block; padding:10px 20px; background:#dc3545; 
                              color:white; text-decoration:none; border-radius:5px;'>
                       Reativar assinatura
                    </a>
                </p>

                <br />
                <p>Após o pagamento, sua assinatura será reativada automaticamente.</p>
                <p>Se já realizou o pagamento, por favor desconsidere este e-mail.</p>
                <p>Obrigado por continuar conosco!</p>
            </body>
        </html>";
    }
}
