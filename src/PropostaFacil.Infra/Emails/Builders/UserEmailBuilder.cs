namespace PropostaFacil.Infra.Emails.Builders;

public class UserEmailBuilder
{
    public static string BuildSendVerifyEmailAddress(string Name, string VerificationLink)
    {
        return $@"
            <!DOCTYPE html>
            <html>
              <head>
                <meta charset=""UTF-8"">
                <title>Ative sua conta</title>
              </head>
              <body style=""font-family: Arial, sans-serif; line-height:1.5; color:#333;"">
                <h2>Olá, {Name}!</h2>
                <p>Bem-vindo ao nosso sistema.</p>
                <p>Para ativar sua conta e criar sua senha, clique no botão abaixo:</p>
                <p>
                  <a href=""{VerificationLink}"" style=""display:inline-block; padding:10px 20px; color:#fff; background-color:#6a0dad; text-decoration:none; border-radius:5px;"">
                    Ativar Conta e Criar Senha
                  </a>
                </p>
                <p>Se você não se cadastrou, ignore este e-mail.</p>
                <hr>
                <p style=""font-size:12px; color:#999;"">&copy; 2025 Seu Sistema</p>
              </body>
            </html>";
    }

    public static string BuildSendForgotPassword(string Name, string ResetPasswordLink)
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
                  <a href=""{ResetPasswordLink}"" style=""display:inline-block; padding:10px 20px; color:#fff; background-color:#6a0dad; text-decoration:none; border-radius:5px;"">
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
