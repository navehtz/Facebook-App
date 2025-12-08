using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookWinFormsApp.logic.dummyData
{
    public class DummyPost
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<string> Likes { get; set; }
        public List<DummyComment> Comments { get; set; }
    }
}
