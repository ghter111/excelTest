using System;

namespace test
{
    public class tradeInfo
    {
        public string Type { get; set; }
        public string UserID { get; set; }
        public string Code { get; set; }
        public string ExchangeID { get; set; }
        public string OrderRef { get; set; }
        public string OrderSysID { get; set; }
        public string TradeID { get; set; }
        public string Direction { get; set; }
        public string OffsetFlag { get; set; }
        public string HedgeFlag { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public string TradeDate { get; set; }
        public string TradeTime { get; set; }    
        public string Remark { get; set; }      
    }
}
