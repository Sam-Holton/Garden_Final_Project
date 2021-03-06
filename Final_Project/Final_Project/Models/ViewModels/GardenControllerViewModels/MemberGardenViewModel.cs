using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.ViewModels.GardenControllerViewModels
{
    public class MemberGardenViewModel
    {
        public string common_name { get; set; }
        public string scientific_name { get; set; }
        public string plantDate { get; set; }
        public string harvestDate { get; set; }
        public string image_url { get; set; }
        public int Id { get; set; }
        public IEnumerable<MemberGardenViewModel> garden { get; set; }
    }
}
