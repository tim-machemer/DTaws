
using System.Collections.Generic;

namespace DealerTrackJsonTranslator.Code;

public class Translator
{

    private readonly string _json ;

    public Translator(string json)
    {
        _json = json;
    }

    public string GetEventTransactionId()
    {
        //var x = JsonConvert.DeserializeObject(_json);
        //Root deserializedObject = JsonConvert.DeserializeObject<Root>(_json);

        //return deserializedObject.detail.eventDetailHref;
        return "";
    }

    public class Root
    {
        public string version { get; set; }
        public string id { get; set; }
        public string detail_type { get; set; }
        public string source { get; set; }
        public string account { get; set; }
        public string time { get; set; }
        public string region { get; set; }
        public List<object> resources { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail
    {
        public string eventVersion { get; set; }
        public string eventId { get; set; }
        public string eventTime { get; set; }
        public string eventName { get; set; }
        public string eventDetailHref { get; set; }
        public string eventSource { get; set; }
        public string eventType { get; set; }
        public string eventTransactionId { get; set; }
        public EventKeyData eventKeyData { get; set; }
        public Payload payload { get; set; }
    }

    public class EventKeyData
    {
        public string creditAppId { get; set; }
        public string lenderId { get; set; }
        public string sourcePartnerId { get; set; }
        public string sourcePartnerDealerId { get; set; }
    }

    public class Payload
    {
        public string status { get; set; }
        public int approvedAmount { get; set; }
        public int approvedRate { get; set; }
        public int approvedTerm { get; set; }
        public string lenderId { get; set; }
        public string lenderName { get; set; }
        public string tier { get; set; }
        public string dealerMessage { get; set; }
        public string fromEmailAddress { get; set; }
        public List<Stipulation> stipulations { get; set; }
        public object lenderMoneyFactor { get; set; }
        public object customerMoneyFactor { get; set; }
        public object downPayment { get; set; }
        public int monthlyPayment { get; set; }
        public string financeMethod { get; set; }
        public object dealerDiscount { get; set; }
        public bool dealUpdate { get; set; }
        public object fundedAmount { get; set; }
        public string rateVariance { get; set; }
        public bool eContractEligible { get; set; }
        public string maxAmtToFinance { get; set; }
        public object resubmitReturnedEcontract { get; set; }
        public string lenderApplicationId { get; set; }
        public object message { get; set; }
        public string alternativeDealsGroupId { get; set; }
    }

    public class Stipulation
    {
        public bool shareWithConsumer { get; set; }
        public string comments { get; set; }
        public string text { get; set; }
        public List<AcceptedDoc> acceptedDocs { get; set; }
    }

    public class AcceptedDoc
    {
        public string customerRole { get; set; }
        public string documentType { get; set; }
    }
}