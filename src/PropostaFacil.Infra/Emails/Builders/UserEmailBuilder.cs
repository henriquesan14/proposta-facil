namespace PropostaFacil.Infra.Emails.Builders;

public class UserEmailBuilder
{
    public static string BuildSendVerifyEmailAddress(string Name, string Email)
    {
        return $@"
            <!DOCTYPE html>
            <html>
              <head>
                <meta charset=""UTF-8"">
                <title>Verificação de E-mail</title>
              </head>
              <body style=""font-family: Arial, sans-serif; line-height:1.5; color:#333;"">
                <h2>Olá, {Name}!</h2>
                <p>Obrigado por se cadastrar no nosso sistema.</p>
                <p>Por favor, clique no botão abaixo para verificar seu e-mail:</p>
                <p>
                  <a href=""{{verificationLink}}"" style=""display:inline-block; padding:10px 20px; color:#fff; background-color:#6a0dad; text-decoration:none; border-radius:5px;"">
                    Verificar E-mail
                  </a>
                </p>
                <p>Se você não se cadastrou, ignore este e-mail.</p>
                <hr>
                <p style=""font-size:12px; color:#999;"">&copy; 2025 Seu Sistema</p>
              </body>
            </html>";
    }

    public static string BuildSendForgotPassword(string Name, string Email)
    {
        return $@"
            <!DOCTYPE html>
            <html>
              <head>
                <meta charset=""UTF-8"">
                <title>Redefinição de Senha</title>
              </head>
              <body style=""font-family: Arial, sans-serif; line-height:1.5; color:#333;"">
                <h2>Olá, {Name}!</h2>
                <p>Recebemos uma solicitação para redefinir sua senha.</p>
                <p>Clique no botão abaixo para criar uma nova senha:</p>
                <p>
                  <a href=""{{{{resetLink}}}}"" style=""display:inline-block; padding:10px 20px; color:#fff; background-color:#6a0dad; text-decoration:none; border-radius:5px;"">
                    Redefinir Senha
                  </a>
                </p>
                <p>Se você não solicitou, apenas ignore este e-mail.</p>
                <hr>
                <p style=""font-size:12px; color:#999;"">&copy; 2025 Seu Sistema</p>
              </body>
            </html>";
    }
}
