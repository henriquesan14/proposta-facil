using PropostaFacil.Shared.Messaging.Events;

namespace PropostaFacil.Infra.Emails.Builders
{
    public static class ProposalEmailBuilder
    {
        public static string BuildProposal(string ProposalNumber, string ClientName, DateTime ValidUntil, IEnumerable<ProposalItemIntegrationEvent> Items, decimal TotalAmount)
        {
            var itemsRows = string.Join("", Items.Select(i =>
                $"<tr>" +
                $"<td>{i.Name}</td>" +
                $"<td>{i.Description}</td>" +
                $"<td>{i.Quantity}</td>" +
                $"<td>{i.UnitPrice:C}</td>" +
                $"<td>{i.TotalPrice:C}</td>" +
                $"</tr>"
            ));

            return $@"
            <html>
                <body>
                    <h2>Proposta #{ProposalNumber}</h2>
                    <p>Cliente: {ClientName}</p>
                    <p>Validade: {ValidUntil:dd/MM/yyyy}</p>
                    <table border='1' cellpadding='5' cellspacing='0'>
                        <thead>
                            <tr>
                                <th>Item</th>
                                <th>Descrição</th>
                                <th>Quantidade</th>
                                <th>Preço Unitário</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {itemsRows}
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan='4'><strong>Total Geral</strong></td>
                                <td><strong>{TotalAmount:C}</strong></td>
                            </tr>
                        </tfoot>
                    </table>
                    <p>Obrigado por escolher nossa empresa!</p>
                </body>
            </html>
            ";
        }
    }
}
