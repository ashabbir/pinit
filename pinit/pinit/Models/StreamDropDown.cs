using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pinit.Models
{
    public class StreamDropDownModel
    {
        public List<Item> _stream;

        [Display(Name = "Select Stream")]
        public int StreamsId { get; set; }
        public int BoardId { get; set; }
        public bool HasStreams { get; set; }
        public string StreamName { get; set; }

        public IEnumerable<SelectListItem> Streams
        {
            get { return new SelectList(_stream, "Id", "Name"); }
        }

        public StreamDropDownModel()
        {
            _stream = new List<Item>();
            HasStreams = false;
        }


        public void FillMe(String username)
        {
            using (var db = new pinit.Data.PinitEntities())
            {

                var stm = db.FollowStreams.Where(s => s.UserName == username).ToList();
                foreach (var item in stm)
                {
                    _stream.Add(new Item()
                    {
                        Id = item.StreamId,
                        Name = item.StreamName
                    });
                    if (_stream.Count() > 0)
                    {
                        HasStreams = true;
                    }
                }
            }

        }

    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}