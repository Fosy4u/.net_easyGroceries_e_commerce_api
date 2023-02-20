using easyGroceries_e_commerce_api.DataModel;

namespace easyGroceries_e_commerce_api.Model
{
    public class PaymentResponseModel
    {
        public FormattedOrderModel? Order { get; set; }
        public ReceiptDataModel? Receipt { get; set; }
        public ShipmentDataModel? Shipment { get; set; }
    }
}