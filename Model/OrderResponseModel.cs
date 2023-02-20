using easyGroceries_e_commerce_api.DataModel;

namespace easyGroceries_e_commerce_api.Model
{
    public class OrderResponseModel
    {
         public FormattedOrderModel? Order { get; set; }
        public CustomerDataModel? Customer { get; set; }
        public ReceiptDataModel? Receipt { get; set; }
        public ShipmentDataModel? Shipment { get; set; }
    }
}