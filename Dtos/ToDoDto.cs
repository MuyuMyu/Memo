﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Shared.Dtos
{
    /// <summary>
    /// 待办事项数据实体
    /// </summary>
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private DateTime createDate;
        private DateTime updateDate;
        private int status;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }


        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public DateTime CreateDate 
        {
            get { return createDate; }
            set {  createDate = value; OnPropertyChanged(); }
        }

        public DateTime UpdateDate 
        {
            get { return updateDate; }
            set {  updateDate = value; OnPropertyChanged(); }
        }
    }
}
