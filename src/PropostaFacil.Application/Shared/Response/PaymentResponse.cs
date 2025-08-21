using PropostaFacil.Domain.Enums;
using System.Text.Json.Serialization;

namespace PropostaFacil.Application.Shared.Response
{
    public record PaymentResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("customer")] string Customer,
        [property: JsonPropertyName("dateCreated")] DateTime DateCreated,
        [property: JsonPropertyName("dueDate")] DateTime DueDate,
        [property: JsonPropertyName("installment")] string Installment,
        [property: JsonPropertyName("subscription")] string Subscription,
        [property: JsonPropertyName("paymentLink")] string PaymentLink,
        [property: JsonPropertyName("value")] decimal Value,
        [property: JsonPropertyName("netValue")] decimal NetValue,
        [property: JsonPropertyName("billingType")][property: JsonConverter(typeof(JsonStringEnumConverter))] BillingTypeEnum BillingType,
        [property: JsonPropertyName("status")] string Status, [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("daysAfterDueDateToRegistrationCancellation")] int? DaysAfterDueDateToRegistrationCancellation,
        [property: JsonPropertyName("externalReference")] string ExternalReference,
        [property: JsonPropertyName("canBePaidAfterDueDate")] bool CanBePaidAfterDueDate,
        [property: JsonPropertyName("invoiceUrl")] string InvoiceUrl,
        [property: JsonPropertyName("bankSlipUrl")] string BankSlipUrl,
        [property: JsonPropertyName("invoiceNumber")] string InvoiceNumber,
        [property: JsonPropertyName("deleted")] bool Deleted);
}
