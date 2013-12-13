using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pinit.Models
{
    /// <summary>
    /// fill up dropdowns and show user existing boards
    /// </summary>
    public class BoardDropDown
    {
        public List<Item> _board;

        [Display(Name = "Select Stream")]
        public int BoardId { get; set; }
        public string BoardName { get; set; }
        public bool HasBoard { get; set; }
        public int PinId { get; set; }

        public IEnumerable<SelectListItem> Boards
        {
            get { return new SelectList(_board, "Id", "Name"); }
        }

        public BoardDropDown()
        {
            _board = new List<Item>();
        }


        public void FillMe(String username)
        {
            using (var db = new pinit.Data.PinitEntities())
            {

                var brd = db.Boards.Where(s => s.BoardOwner == username).ToList();
                foreach (var item in brd)
                {
                    _board.Add(new Item()
                    {
                        Id = item.BoardId,
                        Name = item.BoardName
                    });
                    if (_board.Count() > 0)
                    {
                        HasBoard = true;
                    }
                    else 
                    {
                        HasBoard = false;
                    }
                }
            }

        }

    }

}