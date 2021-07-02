namespace RookieOnlineAssetManagement.ViewModels.Report
{
    public class ReportReadModel
    {
        public string CategoryName { get; set; }
        public int TotalAssets { get; set; }
        public int AssignedAssets { get; set; }
        public int AvailableAssets { get; set; }
        public int NotAvailableAssets { get; set; }
        public int WaitingForRecyclingAssets { get; set; }
        public int RecycledAssets { get; set; }
    }
}
