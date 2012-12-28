using System;
using System.Collections.Generic;
using System.Text;

namespace FeliControls
{
    /// <summary>
    /// 分页控件事件参数类
    /// </summary>
    public class PageChangedEventArgs:System.EventArgs
    {
        public PageChangedEventArgs()
        { }
        /// <summary>
        /// 当前页索引
        /// </summary>
        private int intCurrentPageIndex;
        public new int CurrentPageIndex
        {
            get { return intCurrentPageIndex; }
            set { intCurrentPageIndex = value; }
        }
        /// <summary>
        /// 数据总页数
        /// </summary>
        private int intPageCount;
        public new int PageCount
        {
            get { return intPageCount; }
            set { intPageCount = value; }
        }
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        private int intPageSize;
        public new int PageSize
        {
            get { return intPageSize; }
            set { intPageSize = value; }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        private int intRecordCount;
        public new int RecordCount
        {
            get { return intRecordCount; }
            set { intRecordCount = value; }
        }
    }
}
