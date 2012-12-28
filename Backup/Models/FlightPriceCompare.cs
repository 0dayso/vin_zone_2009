using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightPriceCompare
    {
        #region �ֶ�
        /// <summary>
        /// CompareId
        /// </summary>
        private Int32 _Compareid;

        /// <summary>
        /// OtherPriceId
        /// </summary>
        private Int32? _Otherpriceid;

        /// <summary>
        /// ��ͼ۸�id
        /// </summary>
        private Int32? _LowestPriceId;

        /// <summary>
        /// �Զ���ʵ���
        /// </summary>
        private Int32? _Autoresult;

        /// <summary>
        /// ��ʵ���
        /// </summary>
        private Int32? _Handresult;

        /// <summary>
        /// ��ע
        /// </summary>
        private String _Memo;

        #endregion

        #region ����
        /// <summary>
        /// CompareId
        /// </summary>
        public Int32 Compareid
        {
            get { return _Compareid; }
            set { this._Compareid = value; }
        }

        /// <summary>
        /// OtherPriceId
        /// </summary>
        public Int32? Otherpriceid
        {
            get { return _Otherpriceid; }
            set { this._Otherpriceid = value; }
        }

        /// <summary>
        /// ��ͼ۸�id
        /// </summary>
        public Int32? LowestPriceId
        {
            get { return _LowestPriceId; }
            set { this._LowestPriceId = value; }
        }

        /// <summary>
        /// �Զ���ʵ���
        /// </summary>
        public Int32? Autoresult
        {
            get { return _Autoresult; }
            set { this._Autoresult = value; }
        }

        /// <summary>
        /// ��ʵ���
        /// </summary>
        public Int32? Handresult
        {
            get { return _Handresult; }
            set { this._Handresult = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public String Memo
        {
            get { return _Memo; }
            set { this._Memo = value; }
        }

        #endregion
    }
}
