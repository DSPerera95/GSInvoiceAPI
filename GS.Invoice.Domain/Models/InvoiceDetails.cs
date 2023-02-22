using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Domain.Models
{
    public class InvoiceDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string InvoiceId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "totalamount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty(PropertyName = "invoiceitems")]
        public IList<InvoiceItem>? InvoiceItems { get; set; }
    }
}
