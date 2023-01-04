namespace RentACarSample.Areas.Admin.Models
{
    public class CloseModalPartialViewModel
    {
        public string ModalId { get; set; }
        public bool ShowToastr { get; set; }

        /// <summary>
        /// Toastr Types : "success", "error", "warning", "info"
        /// </summary>
        public string ToastrType { get; set; }
        public string ToastrTitle { get; set; }
        public string ToastrMessage { get; set; }
    }
}
