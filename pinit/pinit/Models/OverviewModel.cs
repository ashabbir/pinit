using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinit.Models
{
    public class OverviewModel
    {
        public List<pinit.Data.Board> boards { get; set; }
        public string username { get; set; }


        public OverviewModel(string _username)
        {
            SetUserName(_username);
            LoadBoards();
        }


        public void SetUserName(string _username) 
        {
            username = _username;
        }
        public void LoadBoards() 
        {
            boards = new List<Data.Board>();
            using (var db = new pinit.Data.PinitEntities())
            {
                boards = db.Boards.Where(b => b.BoardOwner == username).ToList();
                
            }
        }

    }
}