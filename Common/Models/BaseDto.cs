using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.Common.Models
{
    // 定义一个基础 DTO 类，其他 DTO 类可以继承它
    public class BaseDto
    {
        // 私有字段用于存储 ID
        private int id;

        // 公共属性，用于获取和设置 ID
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        // 私有字段用于存储创建日期
        private DateTime createDate;

        // 公共属性，用于获取和设置创建日期
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        // 私有字段用于存储更新日期
        private DateTime updateDate;

        // 公共属性，用于获取和设置更新日期
        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
    }
}